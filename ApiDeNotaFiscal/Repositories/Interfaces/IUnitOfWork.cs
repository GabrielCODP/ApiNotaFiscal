namespace ApiDeNotaFiscal.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        INotaFiscalRepository NotaFiscalRepository { get; }
        IEmpresaRepository EmpresaRepository { get; }
        IClienteRepository ClienteRepository { get; }

        Task CommitAsync();
    }
}
