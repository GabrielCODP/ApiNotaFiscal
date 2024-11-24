using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiDeNotaFiscal.Repositories
{
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Empresa>> GetAllNotasFiscaisDaEmpresa()
        {
            return await _context.Set<Empresa>().AsNoTracking().Include(n => n.NotasFiscais).ToListAsync();
        }
    }
}
