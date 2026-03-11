using InvoiceAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.Data
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options) { }

        // -----------------------------
        // DbSets (Tables)
        // -----------------------------
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }//customer table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // -----------------------------
            // 1️⃣ Invoice → InvoiceItems (One-to-Many)
            // -----------------------------
            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Items)
                .WithOne(it => it.Invoice)
                .HasForeignKey(it => it.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade); // If invoice deleted → delete its invoice items

            // -----------------------------
            // 2️⃣ InvoiceItem → Item (Optional Many-to-One)
            // -----------------------------
            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Item)
                .WithMany() // no navigation back from Item to InvoiceItems
                .HasForeignKey(ii => ii.ItemId)
                .OnDelete(DeleteBehavior.SetNull); // if Item deleted, InvoiceItem keeps data

            base.OnModelCreating(modelBuilder);
        }
    }
}
