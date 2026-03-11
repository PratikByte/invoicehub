using System.Collections.Generic;

namespace InvoiceAPI.Dto
{
    // ✅ Request DTO for creating an Invoice
    public class InvoiceRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Reuse your existing item DTOs here!
        public List<ItemDto> Items { get; set; } = new();  
    }

    // ✅ Response DTO for viewing Invoice details
    public class InvoiceResponseDto
    {
        public int InvoiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }

         public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // Reuse the ItemDto you already have
        public List<ItemDto> Items { get; set; } = new();
    }
}
