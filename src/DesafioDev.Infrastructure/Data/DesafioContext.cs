using DesafioDev.Domain;
using Microsoft.EntityFrameworkCore;

namespace DesafioDev.Infrastructure.Data
{
    public class DesafioContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        public DesafioContext(DbContextOptions<DesafioContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(x =>
            {
                x.HasKey(p => p.Codigo);

                x.Property(p => p.Codigo).IsRequired();
                x.Property(p => p.Descricao).IsRequired();
            });
        }
    }
}
