using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.DTOs
{
    /// <summary>
    /// DTO focado na entrada de dados para registrar movimentações financeiras.
    /// </summary>
    public class TransacaoCadastroDto
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public Guid PessoaId { get; set; }
    }
}