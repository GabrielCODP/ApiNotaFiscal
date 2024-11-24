using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Repositories.Interfaces;

namespace ApiDeNotaFiscal.Repositories
{
    public class NotaFiscalRepository : Repository<NotaFiscal>, INotaFiscalRepository
    {
        public NotaFiscalRepository(AppDbContext context) : base(context)
        {
        }
    }
}
