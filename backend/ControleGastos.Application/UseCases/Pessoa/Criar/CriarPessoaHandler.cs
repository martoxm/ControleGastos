using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.UseCases.Pessoa.Criar;

/// <summary>
/// Caso de uso responsável por orquestrar o cadastro de uma nova pessoa.
/// Recebe o request validado, cria a entidade de domínio e persiste via repositório.
/// </summary>
public class CriarPessoaHandler(IPessoaRepository pessoaRepository) : ICriarPessoaHandler
{
    private readonly IPessoaRepository _pessoaRepository = pessoaRepository;

    public async Task<CriarPessoaResponse> ExecuteAsync(CriarPessoaRequest request, CancellationToken cancellationToken = default)
    {
        // A entidade de domínio é criada aqui, mantendo a lógica de negócio encapsulada no construtor da entidade.
        var novaPessoa = new Domain.Entities.Pessoa(request.Nome, (int)request.Idade!);
        await _pessoaRepository.AdicionarAsync(novaPessoa, cancellationToken);

        return new CriarPessoaResponse
        {
            Id = novaPessoa.Id,
            Nome = novaPessoa.Nome,
            Idade = novaPessoa.Idade
        };
    }
}