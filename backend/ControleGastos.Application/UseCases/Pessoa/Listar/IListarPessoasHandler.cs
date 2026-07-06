namespace ControleGastos.Application.UseCases.Pessoa.Listar;

/// <summary>
/// Contrato do caso de uso responsável por retornar todas as pessoas cadastradas.
/// Seguindo o DIP do SOLID, o controller depende desta abstração e não da implementação concreta.
/// </summary>
public interface IListarPessoasHandler
{
    Task<IEnumerable<ListarPessoasResponse>> ExecuteAsync(CancellationToken cancellationToken = default);
}