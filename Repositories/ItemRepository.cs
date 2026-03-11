
using InvoiceAPI.Common.Pagination;
using InvoiceAPI.Data;
using InvoiceAPI.Entity;
using InvoiceAPI.Repository;
using Microsoft.EntityFrameworkCore;

public class ItemRepository : IItemRepository
{
    private readonly InvoiceDbContext _context;
    private readonly ILogger<ItemRepository> _logger;

    public ItemRepository(InvoiceDbContext context, ILogger<ItemRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    // ✅ Decrease item stock
    public async Task<bool> DecreaseItemStockAsync(int itemId, int quantity, CancellationToken cancellationToken)
    {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId, cancellationToken);
        if (item == null || item.ItemQuantity < quantity)
            return false;

        item.ItemQuantity = item.ItemQuantity - quantity;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Item> AddItemAsync(Item item)
    {
        try
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding Item");
            throw;
        }
    }

    public async Task<PagedResult<Item>> GetAllItemsAsync(PaginationParams paginationParams,CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Getting Item By Pagination..");
            var query = _context.Items.AsQueryable();
            return await query.ToPagedResultAsync(paginationParams, cancellationToken);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting Item");
            throw;
        }
    }

    public async Task<List<Item>> GetAllItemsAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Items.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all items.");
            throw;
        }
    }

    // ✅ Get single item by Id
    public async Task<Item?> GetItemByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Items.FirstOrDefaultAsync(i => i.ItemId == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving Item with Id={id}");
            throw;
        }
    }

    // ✅ Update item (assumes item is already tracked or attached)
    public async Task<Item> UpdateItemAsync(Item item, CancellationToken cancellationToken)
    {
        try
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating Item with Id={item.ItemId}");
            throw;
        }
    }

    // ✅ Delete item by Id
    public async Task<bool> DeleteItemAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == id, cancellationToken);
            if (item == null) return false; // nothing to delete

            _context.Items.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting Item with Id={id}");
            throw;
        }
    }

    
}