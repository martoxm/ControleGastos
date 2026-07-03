using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Repositories
{
    public class TransacaoRepository(AppDbContext context) : ITransacaoRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AdicionarAsync(Transacao transacao)
        {
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transacao>> ListarTodasAsync()
        {
            return await _context.Transacoes.ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> ObterPorPessoaIdAsync(Guid pessoaId)
        {
            return await _context.Transacoes
                                 .Where(t => t.PessoaId == pessoaId)
                                 .ToListAsync();
        }

        public async Task DeletarTransacoesDeUmaPessoaAsync(Guid pessoaId)
        {
            // Regra do teste: ao apagar pessoa, remove transações. O cascade cuida disso,
            // mas ter esse método garante segurança caso façamos operações em lote.
            var transacoes = _context.Transacoes.Where(t => t.PessoaId == pessoaId);
            _context.Transacoes.RemoveRange(transacoes);
            await _context.SaveChangesAsync();
        }
    }
}