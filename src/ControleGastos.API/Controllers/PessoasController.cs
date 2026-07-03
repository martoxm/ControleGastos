using ControleGastos.Application.DTOs;
using ControleGastos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController(PessoaAppService pessoaAppService) : ControllerBase
    {
        private readonly PessoaAppService _pessoaAppService = pessoaAppService;

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PessoaCadastroDto dto, CancellationToken cancellationToken)
        {
            var resultado = await _pessoaAppService.CriarAsync(dto, cancellationToken);

            return Created($"{Request.Path}/{resultado.Id}", resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {
            var pessoas = await _pessoaAppService.ListarTodasAsync(cancellationToken);
            return Ok(pessoas);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
        {
            await _pessoaAppService.DeletarAsync(id, cancellationToken);
            return NoContent();
        }

        // ROTA EXIGIDA: Consulta de totais consolidados
        [HttpGet("totais")]
        public async Task<IActionResult> ObterTotais(CancellationToken cancellationToken)
        {
            var relatorio = await _pessoaAppService.ObterConsultaDeTotaisAsync(cancellationToken);
            return Ok(relatorio);
        }
    }
}