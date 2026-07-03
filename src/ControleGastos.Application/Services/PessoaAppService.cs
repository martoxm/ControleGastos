using ControleGastos.Application.DTOs;
using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Enums;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Services
{
    public class PessoaAppService(IPessoaRepository pessoaRepository, ITransacaoRepository transacaoRepository)
    {
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
        private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;

        public async Task<PessoaExibicaoDto> CriarAsync(PessoaCadastroDto dto)
        {
            var novaPessoa = new Pessoa(dto.Nome, dto.Idade);
            await _pessoaRepository.AdicionarAsync(novaPessoa);

            return new PessoaExibicaoDto
            {
                Id = novaPessoa.Id,
                Nome = novaPessoa.Nome,
                Idade = novaPessoa.Idade
            };
        }

        public async Task DeletarAsync(Guid id)
        {
            // REGRA DE NEGÓCIO EXIGIDA: Deletar pessoa apaga todas as transações vinculadas
            await _transacaoRepository.DeletarTransacoesDeUmaPessoaAsync(id);
            await _pessoaRepository.DeletarAsync(id);
        }

        public async Task<IEnumerable<PessoaExibicaoDto>> ListarTodasAsync()
        {
            var pessoas = await _pessoaRepository.ListarTodasAsync();
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
        public async Task<RelatorioFinanceiroGeralDto> ObterConsultaDeTotaisAsync()
        {
            var pessoas = await _pessoaRepository.ListarTodasAsync();
            var transacoes = await _transacaoRepository.ListarTodasAsync();

            var listaTotaisPessoas = new List<TotalPorPessoaDto>();

            foreach (var pessoa in pessoas)
            {
                var transacoesDaPessoa = transacoes.Where(t => t.PessoaId == pessoa.Id).ToList();

                var totalReceitas = transacoesDaPessoa.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
                var totalDespesas = transacoesDaPessoa.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);

                listaTotaisPessoas.Add(new TotalPorPessoaDto
                {
                    PessoaId = pessoa.Id,
                    Nome = pessoa.Nome,
                    Idade = pessoa.Idade,
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas
                });
            }

            // REGRA DE NEGÓCIO: Calcular o total geral acumulado de receitas, despesas e saldo líquido
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