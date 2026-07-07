namespace ControleGastos.Application.UseCases.Pessoa.Listar;

///  <summary>Dados retornados para cada pessoa na listagem geral.
/// Expõe apenas as informações públicas necessárias para exibição. </summary>
public class ListarPessoasResponse
{
    public Guid Id { get; set; }
    public string? Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}