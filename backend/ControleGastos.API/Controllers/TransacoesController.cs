using ControleGastos.Application.DTOs;
using ControleGastos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] TransacaoCadastroDto dto, CancellationToken cancellationToken)
        {
            var resultado = await _transacaoAppService.CriarAsync(dto, cancellationToken);
            return Created($"{Request.Path}/{resultado.Id}", resultado);
        }

        /// <summary>
        /// Lista todas as transações cadastradas.
        /// </summary>
        /// <returns>Retorna a lista de transações.</returns>
        [HttpGet]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {
            var transacoes = await _transacaoAppService.ListarTodasAsync(cancellationToken);
            return Ok(transacoes);
        }
    }
}