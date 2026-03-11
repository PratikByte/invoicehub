using InvoiceAPI.Data;
using InvoiceAPI.Entity;
using InvoiceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDbContext _context;
        public InvoiceRepository(InvoiceDbContext context) => _context = context;

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Invoice>> GetAllInvoiceAsync(CancellationToken cancellationToken)
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .ToListAsync();
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(int id)
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
        
        //public async Task<bool> DecreaseItemStockAsync(int itemId, int quantity, CancellationToken cancellationToken)
        //{
        
        //        var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId, cancellationToken);

        //        if (item == null)
        //            return false; // Item not found

        //        // Validate quantity
        //        if (quantity <= 0)
        //            throw new ArgumentException("Quantity must be greater than 0.");

        //        if (item.ItemQuantity < quantity)
        //            return false; // Not enough stock

        //        // Reduce stock
        //        item.ItemQuantity -= quantity;

        //        // Save changes
        //        await _context.SaveChangesAsync(cancellationToken);

        //        return true;
            
        //}

    }
}
