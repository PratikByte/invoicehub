using InvoiceAPI.Dto;
using InvoiceAPI.Mapping;
using InvoiceAPI.Repositories.Interfaces;
using MediatR;

namespace InvoiceAPI.CQRS.Queries
{
    // -----------------------------
    // Query: Get all invoices
    // -----------------------------
    public class GetInvoicesAll : IRequest<List<InvoiceResponseDto>>
    {
    }

    // -----------------------------
    // Handler for GetInvoicesAll
    // -----------------------------
    public class GetInvoicesAllHandler : IRequestHandler<GetInvoicesAll, List<InvoiceResponseDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ILogger<GetInvoicesAllHandler> _logger;

        // Inject dependencies (repository + logger)
        public GetInvoicesAllHandler(IInvoiceRepository invoiceRepository, ILogger<GetInvoicesAllHandler> logger)
        {
            _invoiceRepository = invoiceRepository;
            _logger = logger;
        }

        // -----------------------------
        // Handle() → fetch all invoices
        // -----------------------------
        public async Task<List<InvoiceResponseDto>> Handle(GetInvoicesAll request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching all invoices...");

                // 1 Fetch all invoice entities from the repository
                var invoices = await _invoiceRepository.GetAllInvoiceAsync(cancellationToken);

                // 2 Map entities → DTOs using your InvoiceMapper
                var invoiceDtos = invoices
                    .Select(InvoiceMapper.ToDetailDto) // Detail view for all
                    .ToList();

                _logger.LogInformation("Fetched {Count} invoices successfully.", invoiceDtos.Count);

                return invoiceDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all invoices.");
                
                throw;
            }
        }
    }
}
