using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.UseCases.Pessoa.Listar;

/// <summary>
/// Caso de uso responsável por retornar todas as pessoas cadastradas no sistema.
/// Mapeia as entidades de domínio para o response de exibição sem expor detalhes internos.
/// </summary>
public class ListarPessoasHandler(IPessoaRepository pessoaRepository) : IListarPessoasHandler
{
    private readonly IPessoaRepository _pessoaRepository = pessoaRepository;

    public async Task<IEnumerable<ListarPessoasResponse>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var pessoas = await _pessoaRepository.ListarTodasAsync(cancellationToken);

        // Mapeia cada entidade de domínio,
        // evitando expor a entidade diretamente para a camada de apresentação.
        return pessoas.Select(p => new ListarPessoasResponse
        {
            Id = p.Id,
            Nome = p.Nome,
            Idade = p.Idade
        });
    }
}