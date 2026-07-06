using ControleGastos.Application.DTOs;
using ControleGastos.Application.Interfaces;
using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Exceptions;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Services
{
    /// <summary>
    /// Serviço de aplicação responsável por orquestrar os casos de uso relacionados à entidade Transacao.
    /// Centraliza validações de aplicação e mantém os controllers mais enxutos.
    /// </summary>
    public class TransacaoAppService(ITransacaoRepository transacaoRepository, IPessoaRepository pessoaRepository)
    : ITransacaoAppService
    {
        private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;

        public async Task<TransacaoExibicaoDto> CriarAsync(TransacaoCadastroDto dto, CancellationToken cancellationToken = default)
        {
            // REGRA DE NEGÓCIO EXIGIDA:
            // O identificador da pessoa informada precisa existir no cadastro antes da criação da transação.
            var pessoa = await _pessoaRepository.ObterPorIdAsync(dto.PessoaId, cancellationToken) ?? throw new RegraDeNegocioException("A pessoa informada para a transação não foi localizada no sistema.");
            // O construtor da entidade Transacao valida internamente:
            // - descrição obrigatória
            // - valor maior que zero
            // - tipo válido
            // - bloqueio de Receita para menores de idade
            var novaTransacao = new Transacao(dto.Descricao, dto.Valor, dto.Tipo, pessoa);

            await _transacaoRepository.AdicionarAsync(novaTransacao, cancellationToken);

            return new TransacaoExibicaoDto
            {
                Id = novaTransacao.Id,
                Descricao = novaTransacao.Descricao,
                Valor = novaTransacao.Valor,
                Tipo = novaTransacao.Tipo.ToString(),
                PessoaId = novaTransacao.PessoaId
            };
        }

        public async Task<IEnumerable<TransacaoExibicaoDto>> ListarTodasAsync(CancellationToken cancellationToken = default)
        {
            var transacoes = await _transacaoRepository.ListarTodasAsync(cancellationToken);

            return transacoes.Select(t => new TransacaoExibicaoDto
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Tipo = t.Tipo.ToString(),
                PessoaId = t.PessoaId
            });
        }
    }
}