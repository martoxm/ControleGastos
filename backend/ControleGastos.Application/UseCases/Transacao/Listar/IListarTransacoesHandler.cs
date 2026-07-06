namespace ControleGastos.Application.UseCases.Transacao.Listar;

/// <summary>
/// Contrato do caso de uso responsável por retornar todas as transações registradas.
/// Seguindo o DIP do SOLID, o controller depende desta abstração e não da implementação concreta.
/// </summary>
public interface IListarTransacoesHandler
{
    Task<IEnumerable<ListarTransacoesResponse>> ExecuteAsync(CancellationToken cancellationToken = default);
}