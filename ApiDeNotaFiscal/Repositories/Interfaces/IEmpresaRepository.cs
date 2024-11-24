using ApiDeNotaFiscal.Models;

namespace ApiDeNotaFiscal.Repositories.Interfaces
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        Task<IEnumerable<Empresa>> GetAllNotasFiscaisDaEmpresa();
    }
}
