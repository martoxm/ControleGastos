namespace ControleGastos.Application.UseCases.Pessoa.Deletar;

/// <summary>Contrato do caso de uso responsável por remover uma pessoa do sistema.</summary>

public interface IDeletarPessoaHandler
{
    Task ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}