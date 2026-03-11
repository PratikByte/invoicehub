using InvoiceAPI.Dto;
using MediatR;
using InvoiceAPI.Repository;


namespace InvoiceAPI.CQRS.Command
{
    public class UpdateItemCommand : IRequest<ItemDto>
    {
        public UpdateItemRequestDto ItemDto { get; }

        public UpdateItemCommand(UpdateItemRequestDto itemDto)
        {
            ItemDto = itemDto;
        }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ItemDto>
{
    private readonly IItemRepository _ItemRepository;

    public UpdateItemCommandHandler(IItemRepository repo)
    {
        _ItemRepository = repo;
    }

    public async Task<ItemDto> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
       // 1️⃣ Fetch existing item
        var existing = await _ItemRepository.GetItemByIdAsync(request.ItemDto.ItemId, cancellationToken);
        if (existing == null) throw new Exception("Item not found");

        // 2️⃣ Apply partial updates only if values are provided
        if (!string.IsNullOrEmpty(request.ItemDto.ItemName))
            existing.ItemName = request.ItemDto.ItemName;

        if (request.ItemDto.ItemQuantity.HasValue)
            existing.ItemQuantity = request.ItemDto.ItemQuantity.Value;

        if (request.ItemDto.ItemPrice.HasValue)
            existing.ItemPrice = request.ItemDto.ItemPrice.Value;

        // 3️⃣ Save changes
        var updated = await _ItemRepository.UpdateItemAsync(existing, cancellationToken);

        // 4️⃣ Map to response DTO
        return new ItemDto
        {
            ItemId = updated.ItemId,
            ItemName = updated.ItemName,
            ItemQuantity = updated.ItemQuantity,
            ItemPrice = updated.ItemPrice
        };
    }
}
}
