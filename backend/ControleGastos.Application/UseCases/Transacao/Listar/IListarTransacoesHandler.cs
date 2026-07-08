namespace ControleGastos.Application.UseCases.Transacao.Listar;

/// <summary>Contrato do caso de uso responsável por retornar todas as transações registradas./// </summary>

public interface IListarTransacoesHandler
{
    Task<IEnumerable<ListarTransacoesResponse>> ExecuteAsync(CancellationToken cancellationToken = default);
}