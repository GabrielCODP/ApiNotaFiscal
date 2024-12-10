using ApiDeNotaFiscal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiDeNotaFiscal.Context
{
    //public class AppDbContext : DbContext
    //{
    //    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    //    {
    //    }

    //    public DbSet<Empresa>? Empresas { get; set; }
    //    public DbSet<Cliente>? Clientes { get; set; }
    //    public DbSet<NotaFiscal>? NotasFiscais { get; set; }

    //}


    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Empresa>? Empresas { get; set; }
        public DbSet<Cliente>? Clientes { get; set; }
        public DbSet<NotaFiscal>? NotasFiscais { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
