using System;
using System.ComponentModel.DataAnnotations.Schema;
//Snapshot of Items for an Invoice
namespace InvoiceAPI.Entity
{
    // -----------------------------
    // InvoiceItem Entity
    // Stores a snapshot of Item details at billing time
    // -----------------------------
    public class InvoiceItem
    {
        public int Id { get; set; }

        // Item details captured at invoice creation
        public int? ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int Price { get; set; }       // Price at purchase time
        public int SubTotal { get; set; }    // = Quantity * Price

        // -----------------------------
        // Relationship → Belongs to one Invoice
        // -----------------------------
        public int InvoiceId { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }

        // -----------------------------
        // Optional reference to master Item (for traceability only)
        // -----------------------------
        //public int? ItemId { get; set; } // Nullable in case item deleted later
        //[ForeignKey("ItemId")]
        public Item? Item { get; set; }

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
