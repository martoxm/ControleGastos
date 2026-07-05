using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório responsável pela persistência da entidade Transacao.
    /// Implementa operações de cadastro, consulta e remoção por pessoa.
    /// </summary>
    public class TransacaoRepository(AppDbContext context) : ITransacaoRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AdicionarAsync(Transacao transacao, CancellationToken cancellationToken = default)
        {
            await _context.Transacoes.AddAsync(transacao, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Transacao>> ListarTodasAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Transacoes.ToListAsync(cancellationToken);
        }

        public async Task DeletarTransacoesDeUmaPessoaAsync(Guid pessoaId, CancellationToken cancellationToken = default)
        {
            // Regra do teste: ao apagar uma pessoa, suas transações também devem ser removidas.
            // O cascade já cobre esse comportamento no banco, mas este método mantém uma proteção
            // adicional caso a remoção seja executada por outro fluxo específico da aplicação.
            var transacoes = await _context.Transacoes
                                           .Where(t => t.PessoaId == pessoaId)
                                           .ToListAsync(cancellationToken);

            if (transacoes.Count > 0)
            {
                _context.Transacoes.RemoveRange(transacoes);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}