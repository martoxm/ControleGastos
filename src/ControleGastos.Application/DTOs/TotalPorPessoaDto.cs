namespace ControleGastos.Application.DTOs
{
    /// <summary>
    /// DTO de visualização responsável por expor o balanço financeiro de cada indivíduo.
    /// </summary>
    public class TotalPorPessoaDto
    {
        public Guid PessoaId { get; set; }
        public string? Nome { get; set; } 
        public int Idade { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }

        // Regra de negócio: saldo calculado de forma limpa na própria propriedade
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }
}