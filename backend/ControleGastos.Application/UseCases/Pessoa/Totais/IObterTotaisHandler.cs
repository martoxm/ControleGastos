namespace ControleGastos.Application.UseCases.Pessoa.Totais;

/// <summary>
/// Contrato do caso de uso responsável por gerar o relatório financeiro consolidado.
/// Seguindo o DIP do SOLID, o controller depende desta abstração e não da implementação concreta.
/// </summary>
public interface IObterTotaisHandler
{
    Task<ObterTotaisResponse> ExecuteAsync(CancellationToken cancellationToken = default);
}