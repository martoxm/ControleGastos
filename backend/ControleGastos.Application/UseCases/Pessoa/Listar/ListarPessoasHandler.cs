using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.UseCases.Pessoa.Listar;

/// <summary>Caso de uso responsável por retornar todas as pessoas cadastradas no sistema.</summary>

public class ListarPessoasHandler(IPessoaRepository pessoaRepository) : IListarPessoasHandler
{
    private readonly IPessoaRepository _pessoaRepository = pessoaRepository;

    public async Task<IEnumerable<ListarPessoasResponse>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var pessoas = await _pessoaRepository.ListarTodasAsync(cancellationToken);

        // Mapeia cada entidade de domínio
        return pessoas.Select(p => new ListarPessoasResponse
        {
            Id = p.Id,
            Nome = p.Nome,
            Idade = p.Idade
        });
    }
}