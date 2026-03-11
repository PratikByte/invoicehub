using Microsoft.EntityFrameworkCore;    

namespace InvoiceAPI.Common.Pagination
{
    public static class PaginationExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            PaginationParams paginationParams,
            CancellationToken cancellationToken = default)
        {
            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>(items, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }

}
