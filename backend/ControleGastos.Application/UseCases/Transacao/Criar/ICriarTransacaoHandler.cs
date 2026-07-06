namespace ControleGastos.Application.UseCases.Transacao.Criar;

/// <summary>
/// Contrato do caso de uso responsável por registrar uma nova transação.
/// Seguindo o DIP do SOLID, o controller depende desta abstração e não da implementação concreta.
/// </summary>
public interface ICriarTransacaoHandler
{
    Task<CriarTransacaoResponse> ExecuteAsync(CriarTransacaoRequest request, CancellationToken cancellationToken = default);
}