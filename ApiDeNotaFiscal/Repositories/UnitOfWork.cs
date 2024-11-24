using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Repositories.Interfaces;

namespace ApiDeNotaFiscal.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private INotaFiscalRepository? _notaFiscalRepo;

        private IEmpresaRepository? _empresaRepo;

        private IClienteRepository? _clienteRepo;

        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public INotaFiscalRepository NotaFiscalRepository
        {
            get
            {
                return _notaFiscalRepo = _notaFiscalRepo ?? new NotaFiscalRepository(_context);
            }
        }

        public IEmpresaRepository EmpresaRepository
        {
            get
            {
                return _empresaRepo = _empresaRepo ?? new EmpresaRepository(_context);
            }
        }

        public IClienteRepository ClienteRepository
        {
            get
            {
                return _clienteRepo = _clienteRepo ?? new ClienteRepository(_context);
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Dipose()
        {
           await _context.DisposeAsync();
        }
    }
}
