using ControleGastos.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Application.DTOs
{
    /// <summary>
    /// DTO focado na entrada de dados para registrar movimentações financeiras.
    /// </summary>
    public class TransacaoCadastroDto
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "A descrição deve ter entre 2 e 200 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Range(0.01, 999999999.99, ErrorMessage = "O valor deve ser maior que zero.")]
        [DefaultValue(0)]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O tipo da transação é obrigatório.")]
        public TipoTransacao Tipo { get; set; }

        [Required(ErrorMessage = "O identificador da pessoa é obrigatório.")]
        public Guid PessoaId { get; set; }
    }
}