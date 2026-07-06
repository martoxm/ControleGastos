using ControleGastos.Domain.Exceptions;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.UseCases.Transacao.Criar;

/// <summary>
/// Caso de uso responsável por orquestrar o registro de uma nova transação.
/// Valida a existência da pessoa vinculada antes de criar a entidade de domínio.
/// </summary>
public class CriarTransacaoHandler(ITransacaoRepository transacaoRepository, IPessoaRepository pessoaRepository) : ICriarTransacaoHandler
{
    private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository = pessoaRepository;

    public async Task<CriarTransacaoResponse> ExecuteAsync(CriarTransacaoRequest request, CancellationToken cancellationToken = default)
    {
        // REGRA DE NEGÓCIO EXIGIDA:
        // O identificador da pessoa informada precisa existir no cadastro antes da criação da transação.
        var pessoa = await _pessoaRepository.ObterPorIdAsync(request.PessoaId, cancellationToken)
            ?? throw new RegraDeNegocioException("A pessoa informada para a transação não foi localizada no sistema.");

        // O construtor da entidade Transacao valida internamente:
        // - descrição obrigatória
        // - valor maior que zero
        // - tipo válido
        // - bloqueio de Receita para menores de idade
        var novaTransacao = new Domain.Entities.Transacao(request.Descricao, request.Valor, request.Tipo, pessoa);

        await _transacaoRepository.AdicionarAsync(novaTransacao, cancellationToken);

        return new CriarTransacaoResponse
        {
            Id = novaTransacao.Id,
            Descricao = novaTransacao.Descricao,
            Valor = novaTransacao.Valor,
            Tipo = novaTransacao.Tipo.ToString(),
            PessoaId = novaTransacao.PessoaId
        };
    }
}