using ControleGastos.Domain.Entities;

namespace ControleGastos.Domain.Interfaces
{
    /// <summary>Contrato de repositório para a entidade Pessoa.</summary>

    public interface IPessoaRepository
    {
        Task AdicionarAsync(Pessoa pessoa, CancellationToken cancellationToken = default);

        Task DeletarAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Pessoa?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Pessoa>> ListarTodasAsync(CancellationToken cancellationToken = default);
    }
}