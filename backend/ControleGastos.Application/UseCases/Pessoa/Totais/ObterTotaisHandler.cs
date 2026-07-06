using ControleGastos.Domain.Enums;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.UseCases.Pessoa.Totais;

/// <summary>
/// Caso de uso responsável por gerar o relatório financeiro geral do sistema.
/// Calcula os totais individuais por pessoa e consolida o resultado geral.
/// </summary>
public class ObterTotaisHandler(IPessoaRepository pessoaRepository, ITransacaoRepository transacaoRepository) : IObterTotaisHandler
{
    private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
    private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;

    public async Task<ObterTotaisResponse> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var pessoas = await _pessoaRepository.ListarTodasAsync(cancellationToken);
        var transacoes = await _transacaoRepository.ListarTodasAsync(cancellationToken);

        var listaTotais = pessoas
            .Select(p => CalcularTotalPorPessoa(p, transacoes))
            .ToList();

        return MontarRelatorio(listaTotais);
    }

    /// <summary>
    /// Calcula receitas, despesas e saldo de uma pessoa específica
    /// filtrando apenas as transações que pertencem a ela.
    /// </summary>
    private static TotalPorPessoaResponse CalcularTotalPorPessoa(
        Domain.Entities.Pessoa pessoa,
        IEnumerable<Domain.Entities.Transacao> transacoes)
    {
        var transacoesDaPessoa = transacoes.Where(t => t.PessoaId == pessoa.Id);

        return new TotalPorPessoaResponse
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
    /// Consolida os totais individuais no relatório geral do sistema,
    /// somando receitas, despesas e saldo líquido de todas as pessoas.
    /// </summary>
    private static ObterTotaisResponse MontarRelatorio(List<TotalPorPessoaResponse> totais)
    {
        return new ObterTotaisResponse
        {
            Pessoas = totais,
            TotalGeralReceitas = totais.Sum(p => p.TotalReceitas),
            TotalGeralDespesas = totais.Sum(p => p.TotalDespesas),
            SaldoLiquidoGeral = totais.Sum(p => p.Saldo)
        };
    }
}