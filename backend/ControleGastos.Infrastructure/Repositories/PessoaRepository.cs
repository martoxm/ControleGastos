using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Repositories
{
    /// <summary>Repositório responsável pela persistência da entidade Pessoa.
    /// Implementa operações básicas de cadastro, consulta e remoção.</summary>
    public class PessoaRepository(AppDbContext context) : IPessoaRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AdicionarAsync(Pessoa pessoa, CancellationToken cancellationToken = default)
        {
            await _context.Pessoas.AddAsync(pessoa, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var pessoa = await _context.Pessoas.FindAsync([id], cancellationToken);

            if (pessoa != null)
            {
                _context.Pessoas.Remove(pessoa);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<Pessoa?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Pessoas.FindAsync([id], cancellationToken);
        }

        public async Task<IEnumerable<Pessoa>> ListarTodasAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Pessoas.ToListAsync(cancellationToken);
        }
    }
}