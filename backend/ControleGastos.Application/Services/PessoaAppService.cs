using ControleGastos.Application.DTOs;
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

        public async Task DeletarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // REGRA DE NEGÓCIO EXIGIDA:
            // Ao deletar uma pessoa, todas as transações vinculadas a ela também devem ser apagadas.
            // O banco já está configurado com exclusão em cascata, mas manter este fluxo explícito
            // deixa a regra mais visível na camada de aplicação e facilita a explicação do projeto.
            await _transacaoRepository.DeletarTransacoesDeUmaPessoaAsync(id, cancellationToken);
            await _pessoaRepository.DeletarAsync(id, cancellationToken);
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

            var listaTotaisPessoas = new List<TotalPorPessoaDto>();

            foreach (var pessoa in pessoas)
            {
                var transacoesDaPessoa = transacoes.Where(t => t.PessoaId == pessoa.Id).ToList();

                var totalReceitas = transacoesDaPessoa
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor);

                var totalDespesas = transacoesDaPessoa
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor);

                listaTotaisPessoas.Add(new TotalPorPessoaDto
                {
                    PessoaId = pessoa.Id,
                    Nome = pessoa.Nome,
                    Idade = pessoa.Idade,
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas
                });
            }

            // REGRA DE NEGÓCIO:
            // Calcular o total geral acumulado de receitas, despesas e saldo líquido.
            var totalGeralReceitas = listaTotaisPessoas.Sum(p => p.TotalReceitas);
            var totalGeralDespesas = listaTotaisPessoas.Sum(p => p.TotalDespesas);
            var saldoLiquidoGeral = totalGeralReceitas - totalGeralDespesas;

            return new RelatorioFinanceiroGeralDto
            {
                Pessoas = listaTotaisPessoas,
                TotalGeralReceitas = totalGeralReceitas,
                TotalGeralDespesas = totalGeralDespesas,
                SaldoLiquidoGeral = saldoLiquidoGeral
            };
        }
    }
}