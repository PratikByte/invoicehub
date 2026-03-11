using InvoiceAPI.Dto;
using InvoiceAPI.Entity;
using System.Linq;

namespace InvoiceAPI.Mapping
{
    public class InvoiceMapper
    {
        // -----------------------------
        // Map Invoice entity -> InvoiceResponseDto (detailed view)
        // -----------------------------
        public static InvoiceResponseDto ToDetailDto(Invoice invoice)
        {
            return new InvoiceResponseDto
            {
                InvoiceId = invoice.Id,
                Name = invoice.Name,
                Email = invoice.Email,
                TotalAmount = invoice.TotalAmount,
                CreatedAt = invoice.CreatedAt,
                UpdatedAt = invoice.UpdatedAt,

                // Map all invoice items safely
                Items = invoice.Items?.Select(i => new ItemDto
                {
                    // i.ItemId is int? now, so DTO property must also be int?
                    ItemId = i.ItemId,
                    ItemName = i.ItemName,
                    ItemQuantity = i.Quantity,
                    ItemPrice = i.Price,
                    // SubTotal = i.SubTotal (optional, add if needed)
                }).ToList() ?? new List<ItemDto>()
            };
        }

        // -----------------------------
        // Summary DTO (lightweight)
        // -----------------------------
        public static InvoiceResponseDto ToSummaryDto(Invoice invoice)
        {
            return new InvoiceResponseDto
            {
                InvoiceId = invoice.Id,
                Name = invoice.Name,
                Email = invoice.Email,
                TotalAmount = invoice.TotalAmount,
                CreatedAt = invoice.CreatedAt,
                UpdatedAt = invoice.UpdatedAt,
                Items = new List<ItemDto>() // summary: no item details
            };
        }

        // -----------------------------
        // Map InvoiceRequestDto -> Invoice entity (for creation)
        // -----------------------------
        public static Invoice FromRequestDto(InvoiceRequestDto dto)
        {
            return new Invoice
            {
                Name = dto.Name,
                Email = dto.Email,
                Items = dto.Items.Select(i => new InvoiceItem
                {
                    // ItemId is nullable now, so accept int? from DTO
                    ItemId = i.ItemId,
                    Quantity = i.ItemQuantity,
                    ItemName = i.ItemName, // snapshot, filled from DB later
                    Price = 0,            // filled from DB
                    SubTotal = 0           // computed later
                }).ToList()
            };
        }
    }
}
