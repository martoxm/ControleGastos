using ControleGastos.Domain.Exceptions;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.UseCases.Pessoa.Deletar;

/// <summary>Caso de uso responsável por remover uma pessoa e todas as suas transações vinculadas.</summary>

public class DeletarPessoaHandler(IPessoaRepository pessoaRepository, ITransacaoRepository transacaoRepository) : IDeletarPessoaHandler
{
    private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
    private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;

    public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _ = await _pessoaRepository.ObterPorIdAsync(id, cancellationToken) ?? throw new NotFoundException("Pessoa não encontrada.");

        // O banco já está configurado com exclusão em cascata, mas manter este fluxo explícito
        // deixa a regra mais visível na camada de aplicação e facilita a explicação do projeto.
        await _transacaoRepository.DeletarTransacoesDeUmaPessoaAsync(id, cancellationToken);
        await _pessoaRepository.DeletarAsync(id, cancellationToken);
    }
}