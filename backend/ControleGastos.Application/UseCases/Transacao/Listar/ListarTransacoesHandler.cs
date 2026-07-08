using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.UseCases.Transacao.Listar;

/// <summary>Caso de uso responsável por retornar todas as transações registradas no sistema.</summary>

public class ListarTransacoesHandler(ITransacaoRepository transacaoRepository) : IListarTransacoesHandler
{
    private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;

    public async Task<IEnumerable<ListarTransacoesResponse>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var transacoes = await _transacaoRepository.ListarTodasAsync(cancellationToken);

        // Mapeia cada entidade de domínio para o ListarTransacoesResponse
        return transacoes.Select(t => new ListarTransacoesResponse
        {
            Id = t.Id,
            Descricao = t.Descricao,
            Valor = t.Valor,
            Tipo = t.Tipo.ToString(),
            PessoaId = t.PessoaId
        });
    }
}