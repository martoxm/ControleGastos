namespace ControleGastos.Application.UseCases.Pessoa.Totais;

/// <summary>Totais financeiros calculados individualmente para cada pessoa cadastrada.
/// O Saldo é calculado automaticamente como propriedade derivada.</summary>
public class TotalPorPessoaResponse
{
    public Guid PessoaId { get; set; }
    public string? Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }

    // Saldo calculado automaticamente, sem necessidade de ser informado pelo caller.
    public decimal Saldo => TotalReceitas - TotalDespesas;
}

/// <summary>
/// Relatório financeiro geral consolidando os totais de todas as pessoas do sistema.
/// </summary>
public class ObterTotaisResponse
{
    public IEnumerable<TotalPorPessoaResponse> Pessoas { get; set; } = [];
    public decimal TotalGeralReceitas { get; set; }
    public decimal TotalGeralDespesas { get; set; }
    public decimal SaldoLiquidoGeral { get; set; }
}