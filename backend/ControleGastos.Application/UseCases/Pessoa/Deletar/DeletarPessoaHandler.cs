using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.UseCases.Pessoa.Deletar;

/// <summary>
/// Caso de uso responsável por remover uma pessoa e todas as suas transações vinculadas.
/// Garante a consistência dos dados antes de acionar o repositório.
/// </summary>
public class DeletarPessoaHandler(IPessoaRepository pessoaRepository, ITransacaoRepository transacaoRepository) : IDeletarPessoaHandler
{
    private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
    private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(id, cancellationToken);

        if (pessoa is null)
            return false;

        // REGRA DE NEGÓCIO EXIGIDA:
        // Ao deletar uma pessoa, todas as transações vinculadas a ela também devem ser apagadas.
        // O banco já está configurado com exclusão em cascata, mas manter este fluxo explícito
        // deixa a regra mais visível na camada de aplicação e facilita a explicação do projeto.
        await _transacaoRepository.DeletarTransacoesDeUmaPessoaAsync(id, cancellationToken);
        await _pessoaRepository.DeletarAsync(id, cancellationToken);

        return true;
    }
}