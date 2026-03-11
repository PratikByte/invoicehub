using InvoiceAPI.DTOs;
using InvoiceAPI.Repository;
using MediatR;
using InvoiceAPI.Mapping;

namespace InvoiceAPI.CQRS.Command
{
    public class CreateSupplierCommand : IRequest<SupplierReadDto>
    {   
        public SupplierCreateDto SupplierDto { get; }
        public CreateSupplierCommand(SupplierCreateDto dto)
        {
            SupplierDto = dto;
        }
    }

    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, SupplierReadDto>
    {
        public readonly ISupplierRepository _supplierRepository;
        public readonly ILogger<CreateSupplierCommandHandler> _logger;
        public CreateSupplierCommandHandler(ISupplierRepository supplierRepository, ILogger<CreateSupplierCommandHandler> logger)
        {
            _supplierRepository = supplierRepository;
            _logger = logger;
        }

        public async Task<SupplierReadDto> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var MappedEntity = SupplierMapper.ToEntity(request.SupplierDto);
                var createdSupplier = await _supplierRepository.AddSupplierAsync(MappedEntity);
                return createdSupplier.ToReadDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating supplier");
                throw;
            }
        }
    }

}
