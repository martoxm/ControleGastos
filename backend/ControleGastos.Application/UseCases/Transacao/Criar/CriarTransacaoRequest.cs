using ControleGastos.Domain.Enums;

namespace ControleGastos.Application.UseCases.Transacao.Criar;

/// <summary>Dados de entrada necessários para registrar uma nova transação no sistema.</summary>

public class CriarTransacaoRequest
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public Guid PessoaId { get; set; }
}