using ControleGastos.Application.DTOs;
using ControleGastos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PessoasController(IPessoaAppService pessoaAppService) : ControllerBase
    {
        private readonly IPessoaAppService _pessoaAppService = pessoaAppService;

        /// <summary>
        /// Cadastra uma nova pessoa.
        /// </summary>
        /// <param name="dto">Dados da pessoa a ser cadastrada.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Retorna a pessoa criada.</returns>
        /// <response code="201">Pessoa criada com sucesso.</response>
        /// <response code="400">Dados inválidos para cadastro. Pode retornar um ou mais campos com erro.</response>
        [HttpPost]
        [ProducesResponseType(typeof(PessoaExibicaoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] PessoaCadastroDto dto, CancellationToken cancellationToken)
        {
            var resultado = await _pessoaAppService.CriarAsync(dto, cancellationToken);

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
        [ProducesResponseType(typeof(IEnumerable<PessoaExibicaoDto>), StatusCodes.Status200OK)]
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
        /// <returns>Retorna a mensagem de exclusão.</returns>
        /// <remarks>
        /// Ao excluir uma pessoa, todas as transações vinculadas a ela também são removidas.
        /// </remarks>
        /// <response code="200">Pessoa removida com sucesso.</response>
        /// <response code="404">Pessoa não encontrada.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
        {
            var removido = await _pessoaAppService.DeletarAsync(id, cancellationToken);

            if (!removido)
                return NotFound(new { mensagem = "Pessoa não encontrada." });

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
        [ProducesResponseType(typeof(RelatorioFinanceiroGeralDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTotais(CancellationToken cancellationToken)
        {
            var relatorio = await _pessoaAppService.ObterConsultaDeTotaisAsync(cancellationToken);
            return Ok(relatorio);
        }
    }
}