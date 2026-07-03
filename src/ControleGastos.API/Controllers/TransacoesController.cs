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
        public async Task<IActionResult> Criar([FromBody] TransacaoCadastroDto dto)
        {
            try
            {
                var resultado = await _transacaoAppService.CriarAsync(dto);
                return Ok(resultado);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                // Captura as validações de domínio (ex: menor de idade tentando colocar receita)
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var transacoes = await _transacaoAppService.ListarTodasAsync();
            return Ok(transacoes);
        }
    }
}