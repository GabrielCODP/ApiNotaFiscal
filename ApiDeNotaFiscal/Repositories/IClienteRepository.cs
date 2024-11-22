using ApiDeNotaFiscal.Models;

namespace ApiDeNotaFiscal.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetClientesAsync();
        Task<Cliente> GetClienteAsync(int id);
        Task<Cliente> CreateAsync(Cliente cliente);
        Task<Cliente> UpdateAsync(Cliente cliente);
        Task<Cliente> DeleteAsync(int id);
    }
}
