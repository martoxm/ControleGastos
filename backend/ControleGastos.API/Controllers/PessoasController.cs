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
public class PessoasController(
    ICriarPessoaHandler criarHandler,
    IListarPessoasHandler listarHandler,
    IDeletarPessoaHandler deletarHandler,
    IObterTotaisHandler totaisHandler) : ControllerBase
{
    private readonly ICriarPessoaHandler _criarHandler = criarHandler;
    private readonly IListarPessoasHandler _listarHandler = listarHandler;
    private readonly IDeletarPessoaHandler _deletarHandler = deletarHandler;
    private readonly IObterTotaisHandler _totaisHandler = totaisHandler;

    /// <summary>
    /// Cadastra uma nova pessoa.
    /// </summary>
    /// <param name="request">Dados da pessoa a ser cadastrada.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Retorna a pessoa criada.</returns>
    /// <response code="201">Pessoa criada com sucesso.</response>
    /// <response code="400">Dados inválidos para cadastro. Pode retornar um ou mais campos com erro.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CriarPessoaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CriarPessoaRequest request, CancellationToken cancellationToken)
    {
        var resultado = await _criarHandler.ExecuteAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(Listar),
            new { id = resultado.Id },
            resultado);
    }

    /// <summary>
    /// Lista todas as pessoas cadastradas.
    /// </summary>
    /// <returns>Retorna a lista de pessoas.</returns>
    /// <response code="200">Lista de pessoas retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ListarPessoasResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var pessoas = await _listarHandler.ExecuteAsync(cancellationToken);
        return Ok(pessoas);
    }

    /// <summary>
    /// Remove uma pessoa cadastrada.
    /// </summary>
    /// <param name="id">Identificador da pessoa.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Retorna a mensagem de exclusão.</returns>
    /// <remarks>
    /// Ao excluir uma pessoa, todas as transações vinculadas a ela também são removidas.
    /// </remarks>
    /// <response code="200">Pessoa removida com sucesso.</response>
    /// <response code="404">Pessoa não encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
    {
        await _deletarHandler.ExecuteAsync(id, cancellationToken);
        return Ok(new { mensagem = "Pessoa removida com sucesso." });
    }

    /// <summary>
    /// Consulta os totais por pessoa e o total geral.
    /// </summary>
    /// <returns>
    /// Retorna receitas, despesas e saldo de cada pessoa, além do consolidado geral.
    /// </returns>
    /// <response code="200">Relatório retornado com sucesso.</response>
    [HttpGet("totais")]
    [ProducesResponseType(typeof(ObterTotaisResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTotais(CancellationToken cancellationToken)
    {
        var relatorio = await _totaisHandler.ExecuteAsync(cancellationToken);
        return Ok(relatorio);
    }
}