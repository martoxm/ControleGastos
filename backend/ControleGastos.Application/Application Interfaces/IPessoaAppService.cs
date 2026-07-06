using ControleGastos.Application.DTOs;

namespace ControleGastos.Application.Intefaces;

/// <summary>
/// Contrato da camada de aplicação para os casos de uso de Pessoa.
/// Permite inversão de dependência (DIP do SOLID) e facilita testes unitários.
/// </summary>
public interface IPessoaAppService
{
    Task<PessoaExibicaoDto> CriarAsync(PessoaCadastroDto dto, CancellationToken cancellationToken = default);

    Task<bool> DeletarAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<PessoaExibicaoDto>> ListarTodasAsync(CancellationToken cancellationToken = default);

    Task<RelatorioFinanceiroGeralDto> ObterConsultaDeTotaisAsync(CancellationToken cancellationToken = default);
}