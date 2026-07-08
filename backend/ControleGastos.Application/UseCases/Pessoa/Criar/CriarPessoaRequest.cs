namespace ControleGastos.Application.UseCases.Pessoa.Criar;

/// <summary>Dados de entrada necessários para cadastrar uma nova pessoa no sistema.</summary>

public class CriarPessoaRequest
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}