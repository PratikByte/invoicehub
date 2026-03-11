using System;
using System.Collections.Generic;
//Costomerbill
namespace InvoiceAPI.Entity
{
    // -----------------------------
    // Invoice Entity
    // -----------------------------
    public class Invoice
    {
        public int Id { get; set; }

        // Customer info
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // -----------------------------
        // Navigation property → One Invoice has many InvoiceItems
        // -----------------------------
        public List<InvoiceItem> Items { get; set; } = new();

        // Calculated total for this invoice
        public int TotalAmount { get; set; }

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
