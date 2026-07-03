using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Repositories
{
    public class PessoaRepository(AppDbContext context) : IPessoaRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AdicionarAsync(Pessoa pessoa)
        {
            await _context.Pessoas.AddAsync(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Guid id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoas.Remove(pessoa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Pessoa?> ObterPorIdAsync(Guid id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        public async Task<IEnumerable<Pessoa>> ListarTodasAsync()
        {
            return await _context.Pessoas.ToListAsync();
        }
    }
}