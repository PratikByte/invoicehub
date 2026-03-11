using InvoiceAPI.Entity;

namespace InvoiceAPI.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task AddInvoiceAsync(Invoice invoice);
        Task<List<Invoice>> GetAllInvoiceAsync(CancellationToken cancellationToken);
        Task<Invoice?> GetInvoiceByIdAsync(int id);
        //Task<bool> DecreaseItemStockAsync(int itemId, int quantity, CancellationToken cancellationToken);

    }
}