using InvoiceAPI.Repository;
using MediatR;

namespace InvoiceAPI.CQRS.Command
{
    public class DeleteSupplierCommand : IRequest<bool>
    {
        public int SupplierId { get; }
        public DeleteSupplierCommand(int supplierId)
        {
            SupplierId = supplierId;
        }

    }
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, bool>
    {
        private readonly ISupplierRepository _ISupplierRepository;
        private readonly ILogger<DeleteSupplierCommandHandler> _logger;

        public DeleteSupplierCommandHandler(ISupplierRepository iSupplierRepository, ILogger<DeleteSupplierCommandHandler> logger)
        {
            _ISupplierRepository = iSupplierRepository;
            _logger = logger;
        }

        public Task<bool> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleted = _ISupplierRepository.DeleteSupplierAsync(request.SupplierId, cancellationToken);
                return deleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting supplier");
                throw;
            }
        }
    }
}
