namespace ControleGastos.Application.UseCases.Pessoa.Criar;

/// <summary>Contrato do caso de uso responsável por cadastrar uma nova pessoa.</summary>

public interface ICriarPessoaHandler
{
    Task<CriarPessoaResponse> ExecuteAsync(CriarPessoaRequest request, CancellationToken cancellationToken = default);
}