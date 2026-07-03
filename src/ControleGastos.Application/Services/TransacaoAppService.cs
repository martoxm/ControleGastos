using ControleGastos.Application.DTOs;
using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Application.Services
{
    public class TransacaoAppService(ITransacaoRepository transacaoRepository, IPessoaRepository pessoaRepository)
    {
        private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;

        public async Task<TransacaoExibicaoDto> CriarAsync(TransacaoCadastroDto dto)
        {
            // REGRA DE NEGÓCIO EXIGIDA: O identificador da pessoa informada precisa existir no cadastro
            var pessoa = await _pessoaRepository.ObterPorIdAsync(dto.PessoaId);
            if (pessoa != null)
            {
                // O construtor interno da classe Transacao validará automaticamente se a pessoa é menor de idade
                // e se está tentando cadastrar uma Receita indevidamente, gerando erro de domínio se violado.
                var novaTransacao = new Transacao(dto.Descricao, dto.Valor, dto.Tipo, pessoa);

                await _transacaoRepository.AdicionarAsync(novaTransacao);

                return new TransacaoExibicaoDto
                {
                    Id = novaTransacao.Id,
                    Descricao = novaTransacao.Descricao,
                    Valor = novaTransacao.Valor,
                    Tipo = novaTransacao.Tipo.ToString(),
                    PessoaId = novaTransacao.PessoaId
                };
            }

            throw new ArgumentException("A pessoa informada para a transação não foi localizada no sistema.");
        }

        public async Task<IEnumerable<TransacaoExibicaoDto>> ListarTodasAsync()
        {
            var transacoes = await _transacaoRepository.ListarTodasAsync();
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