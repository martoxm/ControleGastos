using ControleGastos.Application.DTOs;

namespace ControleGastos.Application.Interfaces;

/// <summary>
/// Contrato da camada de aplicação para os casos de uso de Transacao.
/// Permite inversão de dependência (DIP do SOLID) e facilita testes unitários.
/// </summary>
public interface ITransacaoAppService
{
    Task<TransacaoExibicaoDto> CriarAsync(TransacaoCadastroDto dto, CancellationToken cancellationToken = default);
    Task<IEnumerable<TransacaoExibicaoDto>> ListarTodasAsync(CancellationToken cancellationToken = default);
}