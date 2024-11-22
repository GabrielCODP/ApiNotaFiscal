using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDeNotaFiscal.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {

        private readonly AppDbContext _context;

        public EmpresaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetEmpresasAsync()
        {
            var empresas = await _context.Empresas.ToListAsync();

            return empresas;
        }

        public async Task<Empresa> GetEmpresaAsync(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            return empresa;
        }

        public async Task<Empresa> CreateAsync(Empresa empresa)
        {

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return empresa;
        }

        public async Task<Empresa> UpdateAsync(Empresa empresa)
        {

            _context.Entry(empresa).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return empresa;
        }

        public async Task<Empresa> DeleteAsync(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();

            return empresa;
        }

    }
}
