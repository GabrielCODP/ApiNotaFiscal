using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDeNotaFiscal.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return clientes;
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == id);
            return cliente;
        }

        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return cliente;

        }

        public async Task<Cliente> UpdateAsync(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return cliente;
        }

        public async Task<Cliente> DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

    }
}
