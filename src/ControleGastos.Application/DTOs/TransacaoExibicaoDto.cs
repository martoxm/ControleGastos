namespace ControleGastos.Application.DTOs
{
    /// <summary>
    /// DTO focado no retorno visual de dados de transações.
    /// Converte o Enum interno para string amigável para o Frontend.
    /// </summary>
    public class TransacaoExibicaoDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public Guid PessoaId { get; set; }
    }
}