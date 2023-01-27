using CRUDintermediario.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDintermediario.Service.Sqlite
{
    public class CrudEstoqueContext : DbContext
    {
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }

        public CrudEstoqueContext(DbContextOptions<CrudEstoqueContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaModel>().ToTable("Categoria");
            modelBuilder.Entity<ProdutoModel>().ToTable("Produto");
        }
    }
}