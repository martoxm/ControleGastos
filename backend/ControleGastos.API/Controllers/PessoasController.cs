using ControleGastos.Application.Responses;
using ControleGastos.Application.UseCases.Pessoa.Criar;
using ControleGastos.Application.UseCases.Pessoa.Deletar;
using ControleGastos.Application.UseCases.Pessoa.Listar;
using ControleGastos.Application.UseCases.Pessoa.Totais;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PessoasController : ControllerBase
{
    /// <summary>Cadastra uma nova pessoa.</summary>
    /// <response code="201">Pessoa criada com sucesso.</response>
    /// <response code="400">Dados inválidos para cadastro.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CriarPessoaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar(
        [FromServices] ICriarPessoaHandler criarHandler,
        [FromBody] CriarPessoaRequest request,
        CancellationToken cancellationToken)
    {
        var resultado = await criarHandler.ExecuteAsync(request, cancellationToken);

        return Created(string.Empty, resultado);
    }

    /// <summary>Lista todas as pessoas cadastradas.</summary>
    /// <response code="200">Lista de pessoas retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ListarPessoasResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(
        [FromServices] IListarPessoasHandler listarHandler,
        CancellationToken cancellationToken)
    {
        var pessoas = await listarHandler.ExecuteAsync(cancellationToken);

        return Ok(pessoas);
    }

    /// <summary>Remove uma pessoa cadastrada.</summary>
    /// <response code="204">Pessoa removida com sucesso.</response>
    /// <response code="404">Pessoa não encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(
        [FromServices] IDeletarPessoaHandler deletarHandler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await deletarHandler.ExecuteAsync(id, cancellationToken);

        return NoContent();
    }

    /// <summary> Consulta os totais por pessoa e o total geral.</summary>
    /// <response code="200">Relatório retornado com sucesso.</response>
    [HttpGet("totais")]
    [ProducesResponseType(typeof(ObterTotaisResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTotais(
        [FromServices] IObterTotaisHandler totaisHandler,
        CancellationToken cancellationToken)
    {
        var relatorio = await totaisHandler.ExecuteAsync(cancellationToken);

        return Ok(relatorio);
    }
}