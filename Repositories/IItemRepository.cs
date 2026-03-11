using InvoiceAPI.Common.Pagination;
using InvoiceAPI.Entity;

namespace InvoiceAPI.Repository
{
    public interface IItemRepository
    {
        Task<Item> AddItemAsync(Item item);

        Task<PagedResult<Item>> GetAllItemsAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
        //Task<List<Item>> GetAllItemsAsync(CancellationToken cancellationToken);

        // ✅ Read (single item by id)
        Task<Item?> GetItemByIdAsync(int itemId, CancellationToken cancellationToken);

        // ✅ Update
        Task<Item> UpdateItemAsync(Item item, CancellationToken cancellationToken);
       Task<bool> DeleteItemAsync(int item, CancellationToken cancellationToken);
        Task<bool> DecreaseItemStockAsync(int itemId, int quantity, CancellationToken cancellationToken);


    }
}
