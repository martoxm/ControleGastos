namespace ControleGastos.Application.UseCases.Pessoa.Totais;

/// <summary>Contrato do caso de uso responsável por gerar o relatório financeiro consolidado.</summary>

public interface IObterTotaisHandler
{
    Task<ObterTotaisResponse> ExecuteAsync(CancellationToken cancellationToken = default);
}