using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControleGastos.Application.DTOs;
using ControleGastos.Application.Services;

namespace ControleGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController(PessoaAppService pessoaAppService) : ControllerBase
    {
        private readonly PessoaAppService _pessoaAppService = pessoaAppService;

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PessoaCadastroDto dto)
        {
            try
            {
                var resultado = await _pessoaAppService.CriarAsync(dto);
                return CreatedAtAction(nameof(Listar), new { id = resultado.Id }, resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var pessoas = await _pessoaAppService.ListarTodasAsync();
            return Ok(pessoas);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _pessoaAppService.DeletarAsync(id);
            return NoContent();
        }

        // ROTA EXIGIDA: Consulta de Totais consolidados
        [HttpGet("totais")]
        public async Task<IActionResult> ObterTotais()
        {
            var relatorio = await _pessoaAppService.ObterConsultaDeTotaisAsync();
            return Ok(relatorio);
        }
    }
}