using InvoiceAPI.Repository;
using MediatR;

namespace InvoiceAPI.CQRS.Command
{
    // ✅ Command only needs the Id of the item to delete
    public class DeleteItemCommand : IRequest<bool>
    {
        public int ItemId { get; }

        public DeleteItemCommand(int itemId)
        {
            ItemId = itemId;
        }
    }

    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, bool>
    {
        private readonly IItemRepository _IItemRepository;
        private readonly ILogger<DeleteItemCommandHandler> _logger;

        public DeleteItemCommandHandler(IItemRepository repo, ILogger<DeleteItemCommandHandler> logger)
        {
            _IItemRepository = repo;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleted = await _IItemRepository.DeleteItemAsync(request.ItemId, cancellationToken);
                return deleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting item Id={request.ItemId}");
                throw;
            }
        }
    }
}
