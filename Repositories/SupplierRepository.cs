using InvoiceAPI.Common.Pagination;
using InvoiceAPI.Data;
using InvoiceAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.Repository
{

    public class SupplierRepository : ISupplierRepository
    {
        private readonly InvoiceDbContext _context;
        private readonly ILogger<SupplierRepository> _logger;

        public SupplierRepository(InvoiceDbContext context, ILogger<SupplierRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Supplier> AddSupplierAsync(Supplier supplier)
        {
            try
            {
                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync();
                return supplier;

            }catch(Exception ex)
            {
                _logger.LogError(ex,"error adding Supplier");
                throw;
            }
        }

        public async Task<bool> DeleteSupplierAsync(int id,CancellationToken cancellationToken)
        {
            try
            {
                var supplier=await _context.Suppliers.FirstOrDefaultAsync(s=>s.SupplierId==id,cancellationToken);
                if(supplier==null)
                return false;
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync(cancellationToken);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error deleting Supplier");
                throw;
            }
        }

        public async Task<PagedResult<Supplier>> GetAllSupplierAsync(PaginationParams paginationParams,CancellationToken cancellationToken)
        {
            try
            {
                var query = _context.Suppliers.AsQueryable();
                return await query.ToPagedResultAsync(paginationParams, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error getting Supplier");
                throw;
            }
        }
       

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            try
            {
                var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == id);
                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error getting Supplier by id={id}");
                throw;
            }
        }

        public async Task<Supplier?>UpdateSupplierAsync(int id,Supplier updatedSupplier, CancellationToken cancellationToken)
        {
            try
            {
                var existingSupplier = await _context.Suppliers.FirstOrDefaultAsync(s=>s.SupplierId==id,cancellationToken);
                if(existingSupplier == null)
                    return null;
                existingSupplier.Name = updatedSupplier.Name;
                existingSupplier.Email = updatedSupplier.Email;
                existingSupplier.MobileNo = updatedSupplier.MobileNo;
                existingSupplier.GstNo = updatedSupplier.GstNo;
                existingSupplier.Address = updatedSupplier.Address;

                await _context.SaveChangesAsync(cancellationToken);
                return existingSupplier;
            }
            catch
            {   
                _logger.LogError("error updating Supplier");
                throw;
            }
        }

        public async Task<Supplier?> UpdateSupplierAsync(Supplier supplier, CancellationToken cancellationToken)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error updating Supplier");
                throw;
            }
        }
        public async Task <Supplier>GetByNumberAsync(string number,CancellationToken cancellationToken)
        {
            try
            {
                var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.MobileNo == number);
                return supplier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error getting Supplier by number={number}");
                throw;
            }
        }
    }
}
