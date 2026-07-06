using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Application.UseCases.Pessoa.Criar;

/// <summary>
/// Dados de entrada necessários para cadastrar uma nova pessoa no sistema.
/// Utilizado como parâmetro do endpoint POST /pessoas.
/// </summary>
public class CriarPessoaRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A idade é obrigatória.")]
    [Range(0, 150, ErrorMessage = "A idade deve estar entre 0 e 150 anos.")]
    public int Idade { get; set; }
}