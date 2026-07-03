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

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] TransacaoCadastroDto dto, CancellationToken cancellationToken)
        {
            var resultado = await _transacaoAppService.CriarAsync(dto, cancellationToken);
            return Created($"{Request.Path}/{resultado.Id}", resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {
            var transacoes = await _transacaoAppService.ListarTodasAsync(cancellationToken);
            return Ok(transacoes);
        }
    }
}