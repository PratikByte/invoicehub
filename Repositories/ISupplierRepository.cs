using InvoiceAPI.Common.Pagination;
using InvoiceAPI.Entity;

namespace InvoiceAPI.Repository
{
    public interface ISupplierRepository
    {
        Task <Supplier>AddSupplierAsync(Supplier supplier);

        Task<PagedResult<Supplier>> GetAllSupplierAsync(PaginationParams paginationParams,CancellationToken cancellationToken);
        Task<bool> DeleteSupplierAsync(int id, CancellationToken cancellationToken);
        Task<Supplier> GetSupplierByIdAsync(int id );

        Task<Supplier?> UpdateSupplierAsync(Supplier updatedSupplier, CancellationToken cancellationToken);
        Task<Supplier?> GetByNumberAsync(string supplierNumber,CancellationToken cancellationToken);
    }
}
