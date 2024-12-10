using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Pagination;
using ApiDeNotaFiscal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

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

        public async Task<PagedList<Empresa>> GetEmpresasPaginacao(EmpresasParameters empresasParameters)
        {
            var empresas = await _context.Set<Empresa>().OrderBy(e => e.EmpresaId).ToListAsync();
            var empresasOrdenadas = PagedList<Empresa>.TOPagedList(empresas.AsQueryable(), empresasParameters.PageNumber, empresasParameters.PageSize); 

            return empresasOrdenadas;
        }
    }
}
