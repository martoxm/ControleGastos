namespace ControleGastos.Application.UseCases.Pessoa.Deletar;

/// <summary>Contrato do caso de uso responsável por remover uma pessoa do sistema.
/// Lança NotFoundException se a pessoa não for encontrada. </summary>
public interface IDeletarPessoaHandler
{
    Task ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}