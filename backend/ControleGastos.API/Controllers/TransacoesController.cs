using ControleGastos.Application.Responses;
using ControleGastos.Application.UseCases.Transacao.Criar;
using ControleGastos.Application.UseCases.Transacao.Listar;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TransacoesController : ControllerBase
{
    /// <summary>Registra uma nova transação vinculada a uma pessoa.</summary>
    /// <response code="201">Transação criada com sucesso.</response>
    /// <response code="400">Dados inválidos ou regra de negócio violada.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CriarTransacaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar(
        [FromServices] ICriarTransacaoHandler criarHandler,
        [FromBody] CriarTransacaoRequest request,
        CancellationToken cancellationToken)
    {
        var resultado = await criarHandler.ExecuteAsync(request, cancellationToken);

        return Created(string.Empty, resultado);
    }

    /// <summary>Lista todas as transações registradas.</summary>
    /// <returns>Retorna a lista de transações.</returns>
    /// <response code="200">Lista de transações retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ListarTransacoesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(
        [FromServices] IListarTransacoesHandler listarHandler,
        CancellationToken cancellationToken)
    {
        var transacoes = await listarHandler.ExecuteAsync(cancellationToken);

        return Ok(transacoes);
    }
}