using InvoiceAPI.Common.Pagination;
using InvoiceAPI.Dto;
using InvoiceAPI.Repository;
using MediatR;

namespace InvoiceAPI.CQRS.Query
{
    public class GetAllItemsQuery : IRequest<PagedResult<ItemDto>>
    {
        public PaginationParams PaginationParams { get; set; }
        public GetAllItemsQuery(PaginationParams paginationParams)
        {
            PaginationParams = paginationParams;
        }

    }

    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, PagedResult<ItemDto>>
    {
        private readonly IItemRepository _ItemRepository;

        public GetAllItemsQueryHandler(IItemRepository repo)
        {
            _ItemRepository = repo;
        }

        public async Task<PagedResult<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var Pageditems = await _ItemRepository.GetAllItemsAsync(request.PaginationParams, cancellationToken);

            // Manual mapping
            var dtoItems = Pageditems.Items.Select(i => new ItemDto
            {
                ItemId = i.ItemId,
                ItemName = i.ItemName,
                ItemQuantity = i.ItemQuantity,
                ItemPrice = i.ItemPrice
            }).ToList();    
            // Return as PagedResult<ItemDto> with same metadata
            return new PagedResult<ItemDto>(
                dtoItems,
                Pageditems.TotalCount,
                Pageditems.PageNumber,
                Pageditems.PageSize);
        }

    }
}
