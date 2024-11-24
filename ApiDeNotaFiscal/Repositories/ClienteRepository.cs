using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiDeNotaFiscal.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cliente>> GetAllNotasFiscaisAsync()
        {
            return await _context.Set<Cliente>().AsNoTracking().Include(n => n.NotasFiscais).ToListAsync();
        }
    }
}
