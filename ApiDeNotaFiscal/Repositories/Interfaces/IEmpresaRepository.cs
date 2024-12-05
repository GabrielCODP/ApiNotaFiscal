using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Pagination;
using System.Numerics;

namespace ApiDeNotaFiscal.Repositories.Interfaces
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        Task<IEnumerable<Empresa>> GetAllNotasFiscaisDaEmpresa();
        //Task<IEnumerable<Empresa>> GetEmpresasPaginacao(EmpresasParameters empresasParameters);
        Task<PagedList<Empresa>> GetEmpresasPaginacao(EmpresasParameters empresasParameters);
    }
}
