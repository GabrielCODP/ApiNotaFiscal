using ApiDeNotaFiscal.Models;

namespace ApiDeNotaFiscal.Repositories.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> GetAllNotasFiscaisAsync();
    }
}
