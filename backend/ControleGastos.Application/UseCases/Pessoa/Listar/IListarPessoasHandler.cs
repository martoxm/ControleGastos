namespace ControleGastos.Application.UseCases.Pessoa.Listar;

/// <summary>Contrato do caso de uso responsável por retornar todas as pessoas cadastradas. </summary>

public interface IListarPessoasHandler
{
    Task<IEnumerable<ListarPessoasResponse>> ExecuteAsync(CancellationToken cancellationToken = default);
}