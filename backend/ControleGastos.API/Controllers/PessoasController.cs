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

        /// <summary>
        /// Cadastra uma nova pessoa.
        /// </summary>
        /// <param name="dto">Dados da pessoa a ser cadastrada.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna a pessoa criada.</returns>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PessoaCadastroDto dto, CancellationToken cancellationToken)
        {
            var resultado = await _pessoaAppService.CriarAsync(dto, cancellationToken);

            return Created($"{Request.Path}/{resultado.Id}", resultado);
        }

        /// <summary>
        /// Lista todas as pessoas cadastradas.
        /// </summary>
        /// <returns>Retorna a lista de pessoas.</returns>
        [HttpGet]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {
            var pessoas = await _pessoaAppService.ListarTodasAsync(cancellationToken);
            return Ok(pessoas);
        }

        /// <summary>
        /// Remove uma pessoa cadastrada.
        /// </summary>
        /// <param name="id">Identificador da pessoa.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna 204 quando a exclusão for realizada com sucesso.</returns>
        /// <remarks>
        /// Ao excluir uma pessoa, todas as transações vinculadas a ela também são removidas.
        /// </remarks>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
        {
            await _pessoaAppService.DeletarAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Consulta os totais por pessoa e o total geral.
        /// </summary>
        /// <returns>
        /// Retorna receitas, despesas e saldo de cada pessoa, além do consolidado geral.
        /// </returns>
        [HttpGet("totais")]
        public async Task<IActionResult> ObterTotais(CancellationToken cancellationToken)
        {
            var relatorio = await _pessoaAppService.ObterConsultaDeTotaisAsync(cancellationToken);
            return Ok(relatorio);
        }
    }
}