using ControleGastos.Application.Responses;
using ControleGastos.Application.UseCases.Transacao.Criar;
using ControleGastos.Application.UseCases.Transacao.Listar;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TransacoesController(
    ICriarTransacaoHandler criarHandler,
    IListarTransacoesHandler listarHandler) : ControllerBase
{
    private readonly ICriarTransacaoHandler _criarHandler = criarHandler;
    private readonly IListarTransacoesHandler _listarHandler = listarHandler;

    /// <summary>
    /// Registra uma nova transação vinculada a uma pessoa.
    /// </summary>
    /// <param name="request">Dados da transação a ser registrada.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Retorna a transação criada.</returns>
    /// <response code="201">Transação criada com sucesso.</response>
    /// <response code="400">Dados inválidos ou regra de negócio violada.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CriarTransacaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CriarTransacaoRequest request, CancellationToken cancellationToken)
    {
        var resultado = await _criarHandler.ExecuteAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(Listar),
            new { id = resultado.Id },
            resultado);
    }

    /// <summary>
    /// Lista todas as transações registradas.
    /// </summary>
    /// <returns>Retorna a lista de transações.</returns>
    /// <response code="200">Lista de transações retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ListarTransacoesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var transacoes = await _listarHandler.ExecuteAsync(cancellationToken);
        return Ok(transacoes);
    }
}