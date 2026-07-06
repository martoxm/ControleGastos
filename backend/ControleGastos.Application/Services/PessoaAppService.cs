using ControleGastos.Application.DTOs;
using ControleGastos.Application.Intefaces;
using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Enums;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Services
{
    /// <summary>
    /// Serviço de aplicação responsável por orquestrar os casos de uso relacionados à entidade Pessoa.
    /// Mantém controllers mais finos e centraliza a lógica de aplicação.
    /// </summary>
    public class PessoaAppService(IPessoaRepository pessoaRepository, ITransacaoRepository transacaoRepository)
    : IPessoaAppService
    {
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
        private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;

        public async Task<PessoaExibicaoDto> CriarAsync(PessoaCadastroDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Os dados da pessoa são obrigatórios.");

            var novaPessoa = new Pessoa(dto.Nome, dto.Idade);
            await _pessoaRepository.AdicionarAsync(novaPessoa, cancellationToken);

            return new PessoaExibicaoDto
            {
                Id = novaPessoa.Id,
                Nome = novaPessoa.Nome,
                Idade = novaPessoa.Idade
            };
        }

        public async Task<bool> DeletarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var pessoa = await _pessoaRepository.ObterPorIdAsync(id, cancellationToken);

            if (pessoa is null)
                return false;

            // REGRA DE NEGÓCIO EXIGIDA:
            // Ao deletar uma pessoa, todas as transações vinculadas a ela também devem ser apagadas.
            // O banco já está configurado com exclusão em cascata, mas manter este fluxo explícito
            // deixa a regra mais visível na camada de aplicação e facilita a explicação do projeto.
            await _transacaoRepository.DeletarTransacoesDeUmaPessoaAsync(id, cancellationToken);
            await _pessoaRepository.DeletarAsync(id, cancellationToken);

            return true;
        }

        public async Task<IEnumerable<PessoaExibicaoDto>> ListarTodasAsync(CancellationToken cancellationToken = default)
        {
            var pessoas = await _pessoaRepository.ListarTodasAsync(cancellationToken);

            return pessoas.Select(p => new PessoaExibicaoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Idade = p.Idade
            });
        }

        /// <summary>
        /// Gera o relatório completo calculando os totais individuais e o consolidado geral do sistema.
        /// </summary>
        public async Task<RelatorioFinanceiroGeralDto> ObterConsultaDeTotaisAsync(CancellationToken cancellationToken = default)
        {
            var pessoas = await _pessoaRepository.ListarTodasAsync(cancellationToken);
            var transacoes = await _transacaoRepository.ListarTodasAsync(cancellationToken);

            var listaTotaisPessoas = pessoas
                .Select(p => CalcularTotalPorPessoa(p, transacoes))
                .ToList();

            return MontarRelatorioGeral(listaTotaisPessoas);
        }

        /// <summary>
        /// Calcula receitas, despesas e saldo de uma pessoa específica.
        /// </summary>
        private static TotalPorPessoaDto CalcularTotalPorPessoa(Pessoa pessoa, IEnumerable<Transacao> transacoes)
        {
            var transacoesDaPessoa = transacoes.Where(t => t.PessoaId == pessoa.Id);

            return new TotalPorPessoaDto
            {
                PessoaId = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade,
                TotalReceitas = transacoesDaPessoa
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),
                TotalDespesas = transacoesDaPessoa
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            };
        }

        /// <summary>
        /// Consolida os totais individuais no relatório geral do sistema.
        /// </summary>
        private static RelatorioFinanceiroGeralDto MontarRelatorioGeral(List<TotalPorPessoaDto> totaisPessoas)
        {
            return new RelatorioFinanceiroGeralDto
            {
                Pessoas = totaisPessoas,
                TotalGeralReceitas = totaisPessoas.Sum(p => p.TotalReceitas),
                TotalGeralDespesas = totaisPessoas.Sum(p => p.TotalDespesas),
                SaldoLiquidoGeral = totaisPessoas.Sum(p => p.Saldo)
            };
        }
    }
}