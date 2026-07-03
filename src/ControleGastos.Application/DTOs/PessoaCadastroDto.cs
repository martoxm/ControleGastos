using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Application.DTOs
{
    /// <summary>
    /// DTO focado exclusivamente na entrada de dados para o cadastro de uma nova pessoa.
    /// </summary>
    public class PessoaCadastroDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Range(0, 150, ErrorMessage = "A idade deve estar entre 0 e 150 anos.")]
        public int Idade { get; set; }
    }
}