namespace ControleGastos.Application.UseCases.Pessoa.Criar;

/// <summary>
/// Dados retornados após o cadastro bem-sucedido de uma pessoa.
/// Expõe apenas as informações necessárias para o cliente da API.
/// </summary>
public class CriarPessoaResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}