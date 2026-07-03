namespace ControleGastos.Application.DTOs
{
    /// <summary>
    /// DTO focado no retorno de dados de transações para exibição.
    /// Converte o enum interno para uma string mais amigável ao frontend.
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