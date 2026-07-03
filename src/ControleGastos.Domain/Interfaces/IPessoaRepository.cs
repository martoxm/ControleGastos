using ControleGastos.Domain.Entities;

namespace ControleGastos.Domain.Interfaces
{
    /// <summary>
    /// Contrato de repositório para a entidade Pessoa.
    /// Define as operações básicas exigidas pelas regras de negócio.
    /// </summary>
    public interface IPessoaRepository
    {
        Task AdicionarAsync(Pessoa pessoa);

        Task DeletarAsync(Guid id);

        Task<Pessoa?> ObterPorIdAsync(Guid id);

        Task<IEnumerable<Pessoa>> ListarTodasAsync();
    }
}