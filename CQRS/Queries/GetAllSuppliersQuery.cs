using InvoiceAPI.Common.Pagination;
using InvoiceAPI.DTOs;
using InvoiceAPI.Mapping;
using InvoiceAPI.Repository;
using MediatR;

namespace InvoiceAPI.CQRS.Query
{
    public class GetAllSuppliersQuery : IRequest<PagedResult<SupplierReadDto>>
    {
        public PaginationParams PaginationParams { get; set; }
        public GetAllSuppliersQuery(PaginationParams paginationParams)
        {
            PaginationParams = paginationParams;
        }


    }
    public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, PagedResult<SupplierReadDto>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILogger<GetAllSuppliersQueryHandler> _logger;

        public GetAllSuppliersQueryHandler(ISupplierRepository supplierRepository, ILogger<GetAllSuppliersQueryHandler> logger)
        {
            _supplierRepository = supplierRepository;
            _logger = logger;
        }

        public async Task<PagedResult<SupplierReadDto>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var pagedSuppliers = await _supplierRepository.GetAllSupplierAsync(request.PaginationParams,cancellationToken);
                //manual mapping
                var supplierDtos = pagedSuppliers.Items.Select(s => s.ToReadDto()).ToList();

                var result = new PagedResult<SupplierReadDto>(
                    supplierDtos,
                    pagedSuppliers.TotalCount,
                    pagedSuppliers.PageNumber,
                    pagedSuppliers.PageSize
                    );
                _logger.LogInformation("Fetched {Count} suppliers successfully.", supplierDtos.Count);
                return result;
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching suppliers");
                throw;
            }
        }
    }
}
