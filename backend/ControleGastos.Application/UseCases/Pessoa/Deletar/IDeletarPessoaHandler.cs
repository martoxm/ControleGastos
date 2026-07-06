namespace ControleGastos.Application.UseCases.Pessoa.Deletar;

/// <summary>
/// Contrato do caso de uso responsável por remover uma pessoa do sistema.
/// Retorna false se a pessoa não for encontrada, true em caso de sucesso.
/// </summary>
public interface IDeletarPessoaHandler
{
    Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}