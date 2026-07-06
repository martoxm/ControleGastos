namespace ControleGastos.Application.UseCases.Pessoa.Criar;

/// <summary>
/// Contrato do caso de uso responsável por cadastrar uma nova pessoa.
/// Seguindo o DIP do SOLID, o controller depende desta abstração e não da implementação concreta.
/// </summary>
public interface ICriarPessoaHandler
{
    Task<CriarPessoaResponse> ExecuteAsync(CriarPessoaRequest request, CancellationToken cancellationToken = default);
}