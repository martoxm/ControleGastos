namespace ControleGastos.Application.UseCases.Transacao.Listar;

/// <summary>Dados retornados para cada transação na listagem geral.</summary>
public class ListarTransacoesResponse
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }

    // Tipo retornado como string para facilitar a leitura no cliente (ex: "Receita" ou "Despesa").
    public string Tipo { get; set; } = string.Empty;

    public Guid PessoaId { get; set; }
}