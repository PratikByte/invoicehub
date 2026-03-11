// CQRS/Handlers/CreateInvoiceHandler.cs
using InvoiceAPI.CQRS.Command;
using InvoiceAPI.Dto;
using InvoiceAPI.Entity;
using InvoiceAPI.Mapping;
using InvoiceAPI.Repositories.Interfaces;
using InvoiceAPI.Repository;
using MediatR;

namespace InvoiceAPI.CQRS.Command
{
    public class CreateInvoiceCommand : IRequest<InvoiceResponseDto> // Return response after creation
    {
        public InvoiceRequestDto InvoiceDto { get; }

        // Constructor injection for DTO
        public CreateInvoiceCommand(InvoiceRequestDto invoiceDto)
        {
            InvoiceDto = invoiceDto;
        }
    }

   
        public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, InvoiceResponseDto>
        {
            private readonly IInvoiceRepository _invoiceRepository;
            private readonly IItemRepository _itemRepository;
            private readonly ILogger<CreateInvoiceHandler> _logger;

            public CreateInvoiceHandler(
                IInvoiceRepository invoiceRepository,
                IItemRepository itemRepository,
                ILogger<CreateInvoiceHandler> logger)
            {
                _invoiceRepository = invoiceRepository;
                _itemRepository = itemRepository;
                _logger = logger;
            }

            public async Task<InvoiceResponseDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Creating invoice for customer: {Customer}", request.InvoiceDto.Name);

                    // 1️ Map DTO to Invoice entity
                    var invoice = InvoiceMapper.FromRequestDto(request.InvoiceDto);

                    // 2️ Process all items
                    foreach (var invoiceItem in invoice.Items)
                    {
                    if (!invoiceItem.ItemId.HasValue)
                        throw new Exception("Invoice item must have a valid ItemId.");
                    var dbItem = await _itemRepository.GetItemByIdAsync(invoiceItem.ItemId.Value, cancellationToken);
                        if (dbItem == null)
                        {
                            throw new Exception($"Item with ID {invoiceItem.ItemId} not found.");
                        }

                        if (invoiceItem.Quantity > dbItem.ItemQuantity)
                        {
                            throw new Exception($"Not enough stock for item {dbItem.ItemName}. Available: {dbItem.ItemQuantity}");
                        }

                        // Reduce stock in real Items table
                        dbItem.ItemQuantity -= invoiceItem.Quantity;
                        await _itemRepository.UpdateItemAsync(dbItem, cancellationToken);

                        // Update snapshot in invoice item
                        invoiceItem.Price = dbItem.ItemPrice;
                        invoiceItem.SubTotal = invoiceItem.Quantity * dbItem.ItemPrice;

                        _logger.LogInformation("Stock updated for item {ItemName}, Remaining: {Qty}", dbItem.ItemName, dbItem.ItemQuantity);
                    }

                    // 3️ Calculate total after all items
                    invoice.TotalAmount = invoice.Items.Sum(i => i.SubTotal);

                    // 4️ Save invoice
                    await _invoiceRepository.AddInvoiceAsync(invoice);
                    _logger.LogInformation("Invoice created successfully (ID: {Id})", invoice.Id);

                    // 5️ Return DTO
                    return InvoiceMapper.ToDetailDto(invoice);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating invoice");
                    throw;
                }
            }
        }

    }

