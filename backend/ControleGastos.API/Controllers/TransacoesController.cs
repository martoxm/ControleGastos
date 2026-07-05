using ControleGastos.Application.DTOs;
using ControleGastos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TransacoesController(TransacaoAppService transacaoAppService) : ControllerBase
    {
        private readonly TransacaoAppService _transacaoAppService = transacaoAppService;

        /// <summary>
        /// Cadastra uma nova transação.
        /// </summary>
        /// <param name="dto">Dados da transação a ser cadastrada.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna a transação criada.</returns>
        /// <remarks>
        /// A pessoa informada deve existir.
        /// Pessoas menores de idade podem cadastrar apenas despesas.
        /// </remarks>
        /// <response code="201">Transação criada com sucesso.</response>
        /// <response code="400">Dados inválidos para cadastro. Pode retornar um ou mais campos com erro.</response>
        [HttpPost]
        [ProducesResponseType(typeof(TransacaoExibicaoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] TransacaoCadastroDto dto, CancellationToken cancellationToken)
        {
            var resultado = await _transacaoAppService.CriarAsync(dto, cancellationToken);

            return CreatedAtAction(
                nameof(Listar),
                new { id = resultado.Id },
                resultado);
        }

        /// <summary>
        /// Lista todas as transações cadastradas.
        /// </summary>
        /// <returns>Retorna a lista de transações.</returns>
        /// <response code="200">Lista de transações retornada com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransacaoExibicaoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {
            var transacoes = await _transacaoAppService.ListarTodasAsync(cancellationToken);
            return Ok(transacoes);
        }
    }
}