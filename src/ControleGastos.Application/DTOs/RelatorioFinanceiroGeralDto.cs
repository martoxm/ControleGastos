namespace ControleGastos.Application.DTOs
{
    /// <summary>
    /// DTO mestre usado para responder à tela de "Consulta de Totais" contendo a lista e o rodapé geral.
    /// </summary>
    public class RelatorioFinanceiroGeralDto
    {
        public IEnumerable<TotalPorPessoaDto> Pessoas { get; set; } = new List<TotalPorPessoaDto>();
        public decimal TotalGeralReceitas { get; set; }
        public decimal TotalGeralDespesas { get; set; }
        public decimal SaldoLiquidoGeral { get; set; }
    }
}