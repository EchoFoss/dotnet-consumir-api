using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace WebApi.Infra.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>()
            .Property(p => p.Nome)
            .HasMaxLength(80);
        modelBuilder.Entity<Produto>()
            .Property(p => p.Preco)
            .HasPrecision(10, 2);
        modelBuilder.Entity<Produto>()
            .HasData(
                new Produto(1, "Caderno", 7.99M, 50),
                new Produto(2, "Lápis", 1.99M, 100),
                new Produto(3, "Borracha", 0.75M, 20)
            );
    }
}