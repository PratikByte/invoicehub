
using AutoMapper;
using InvoiceAPI.Dto;
using InvoiceAPI.Entity;
using InvoiceAPI.Repository;
using MediatR;

namespace InvoiceAPI.CQRS.Command
{

    public class AddItemCommand : IRequest<ItemDto>
    {
        public CreateItemRequestDto ItemDto { get; }

        public AddItemCommand(CreateItemRequestDto itemDto)
        {
            ItemDto = itemDto;
        }
    }

    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, ItemDto>
{
    private readonly IItemRepository _IItemRepository;

    public AddItemCommandHandler(IItemRepository repo)
    {
        _IItemRepository = repo;
    }

    public async Task<ItemDto> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new Item
        {
            ItemName = request.ItemDto.ItemName,
            ItemQuantity = request.ItemDto.ItemQuantity,
            ItemPrice = request.ItemDto.ItemPrice
        };

        var created = await _IItemRepository.AddItemAsync(entity);

        return new ItemDto
        {
            ItemId = created.ItemId,
            ItemName = created.ItemName,
            ItemQuantity = created.ItemQuantity,
            ItemPrice = created.ItemPrice
        };
    }
}
}
