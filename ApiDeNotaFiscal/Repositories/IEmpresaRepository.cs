using ApiDeNotaFiscal.Models;

namespace ApiDeNotaFiscal.Repositories
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> GetEmpresasAsync();
        Task<Empresa> GetEmpresaAsync(int id);
        Task<Empresa> CreateAsync(Empresa empresa);
        Task<Empresa> UpdateAsync(Empresa empresa);
        Task<Empresa> DeleteAsync(int id);
    }
}
