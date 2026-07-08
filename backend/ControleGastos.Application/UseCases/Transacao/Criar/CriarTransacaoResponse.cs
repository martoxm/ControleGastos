namespace ControleGastos.Application.UseCases.Transacao.Criar;

/// <summary>Dados retornados após o registro bem-sucedido de uma transação.</summary>

public class CriarTransacaoResponse
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }

    // Tipo retornado como string para facilitar a leitura no cliente (ex: "Receita" ou "Despesa").
    public string Tipo { get; set; } = string.Empty;

    public Guid PessoaId { get; set; }
}