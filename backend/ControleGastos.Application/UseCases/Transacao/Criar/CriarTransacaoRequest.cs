using ControleGastos.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Application.UseCases.Transacao.Criar;

/// <summary>Dados de entrada necessários para registrar uma nova transação no sistema.
/// Utilizado como parâmetro do endpoint POST /transacoes.</summary>
public class CriarTransacaoRequest
{
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 e 200 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    [DefaultValue(0)]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O tipo da transação é obrigatório.")]
    [EnumDataType(typeof(TipoTransacao), ErrorMessage = "Tipo de transação inválido. Use 0 para Receita ou 1 para Despesa.")]
    public TipoTransacao Tipo { get; set; }

    [Required(ErrorMessage = "O identificador da pessoa é obrigatório.")]
    public Guid PessoaId { get; set; }
}