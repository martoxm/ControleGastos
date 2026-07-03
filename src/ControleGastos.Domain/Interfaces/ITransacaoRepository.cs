using ControleGastos.Domain.Entities;

namespace ControleGastos.Domain.Interfaces
{
    /// <summary>
    /// Contrato de repositório para a entidade Transacao.
    /// Focado em criação, listagem e remoção em cascata.
    /// </summary>
    public interface ITransacaoRepository
    {
        Task AdicionarAsync(Transacao transacao);

        Task<IEnumerable<Transacao>> ListarTodasAsync();

        Task<IEnumerable<Transacao>> ObterPorPessoaIdAsync(Guid pessoaId);

        Task DeletarTransacoesDeUmaPessoaAsync(Guid pessoaId);
    }
}