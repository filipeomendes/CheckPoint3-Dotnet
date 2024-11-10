using Checkpoint3.Models;
using Microsoft.EntityFrameworkCore;

namespace checkpoint3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Livro> Livros { get; set; }

        public DbSet<Biblioteca> Bibliotecas { get; set; }
    }
}
