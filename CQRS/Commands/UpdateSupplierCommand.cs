using InvoiceAPI.DTOs;
using InvoiceAPI.Mapping;
using InvoiceAPI.Repository;
using MediatR;

namespace InvoiceAPI.CQRS.Command
{
    public class UpdateSupplierCommand: IRequest<SupplierReadDto>
    {
        public UpdateSupplierCommand(int supplierId, SupplierUpdateDto supplierDto)
        {
            SupplierId = supplierId;
            SupplierDto = supplierDto;
        }

        public int SupplierId { get; set; }
        public SupplierUpdateDto SupplierDto { get; }

        
    }
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, SupplierReadDto>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILogger<UpdateSupplierCommandHandler> _logger;

        public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository, ILogger<UpdateSupplierCommandHandler> logger)
        {
            _supplierRepository = supplierRepository;
            _logger = logger;
        }

        public async Task<SupplierReadDto> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            try
            {   var existingSupplier= await _supplierRepository.GetSupplierByIdAsync(request.SupplierId);
                if (existingSupplier == null) {
                    _logger.LogWarning("Supplier with ID {SupplierId} not found for update.", request.SupplierId);
                    throw new Exception($"Supplier with ID {request.SupplierId} not found.");
                }
                var dto = request.SupplierDto;

               
                if(dto.Name!=null)existingSupplier.Name = dto.Name;
                if(dto.Email!=null)existingSupplier.Email=dto.Email;
                if(dto.MobileNo!=null)existingSupplier.MobileNo = dto.MobileNo;
                if(dto.GstNo!=null)existingSupplier.GstNo=dto.GstNo;
                if (dto.Address!=null)existingSupplier.Address = dto.Address;

                var updatedSupplier = await _supplierRepository.UpdateSupplierAsync( existingSupplier, cancellationToken);
                return updatedSupplier.ToReadDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating supplier");
                  throw;
            }
        }
    }
}
   