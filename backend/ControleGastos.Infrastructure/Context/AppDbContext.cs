using ControleGastos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Context
{
    /// <summary>
    /// Contexto do Banco de Dados configurado para mapear as entidades para tabelas SQLite.
    /// </summary>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento da entidade Pessoa
            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.ToTable("Pessoas");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(e => e.Idade)
                      .IsRequired();
            });

            // Mapeamento da entidade Transacao
            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.ToTable("Transacoes");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Descricao)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Valor)
                      .HasPrecision(18, 2)
                      .HasConversion<double>()
                      .IsRequired(); // No SQLite, o suporte a decimal pode variar; por isso a conversão ajuda a evitar problemas de persistência neste teste.

                entity.Property(e => e.Tipo)
                      .IsRequired();

                // Configurando o relacionamento: uma Transação pertence a uma Pessoa.
                // Se a pessoa for removida, suas transações também serão excluídas.
                entity.HasOne<Pessoa>()
                      .WithMany()
                      .HasForeignKey(e => e.PessoaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}