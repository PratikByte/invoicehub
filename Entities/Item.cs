using System;

namespace InvoiceAPI.Entity
{
    // -----------------------------
    // Item Entity (Inventory Table)
    // -----------------------------
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int ItemPrice { get; set; }      // Current live price
        public int ItemQuantity { get; set; }   // Current   quantity

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
