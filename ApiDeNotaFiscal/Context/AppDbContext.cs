using ApiDeNotaFiscal.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDeNotaFiscal.Context
{

   
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Empresa>? Empresas { get; set; }
        public DbSet<Cliente>? Clientes { get; set; }
        public DbSet<NotaFiscal>? NotasFiscais { get; set; }
      
    }
}
