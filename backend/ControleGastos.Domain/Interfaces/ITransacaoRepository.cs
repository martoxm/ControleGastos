using ControleGastos.Domain.Entities;

namespace ControleGastos.Domain.Interfaces
{
    /// <summary>Contrato de repositório para a entidade Transacao.
    /// Focado em criação, listagem e remoção em cascata.</summary>
    public interface ITransacaoRepository
    {
        Task AdicionarAsync(Transacao transacao, CancellationToken cancellationToken = default);

        Task<IEnumerable<Transacao>> ListarTodasAsync(CancellationToken cancellationToken = default);

        Task DeletarTransacoesDeUmaPessoaAsync(Guid pessoaId, CancellationToken cancellationToken = default);
    }
}