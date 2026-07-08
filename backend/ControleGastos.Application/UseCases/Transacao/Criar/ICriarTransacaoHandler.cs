namespace ControleGastos.Application.UseCases.Transacao.Criar;

/// <summary>Contrato do caso de uso responsável por registrar uma nova transação.</summary>

public interface ICriarTransacaoHandler
{
    Task<CriarTransacaoResponse> ExecuteAsync(CriarTransacaoRequest request, CancellationToken cancellationToken = default);
}