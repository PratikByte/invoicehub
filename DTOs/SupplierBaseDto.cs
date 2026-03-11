namespace InvoiceAPI.DTOs
{
	//   Base class containing common fields shared across DTOs
	public abstract class SupplierBaseDto
	{
		public string Name { get; set; } = string.Empty;     // Supplier name
		public string Email { get; set; } = string.Empty;    // Supplier email
		public string MobileNo { get; set; }     = string.Empty;              // Supplier mobile number
        public string GstNo { get; set; } = string.Empty;    // GST Number
		public string Address { get; set; } = string.Empty;  // Supplier address
	}

	//   DTO for creating a supplier (POST)
	public class SupplierCreateDto : SupplierBaseDto
	{
		// No SupplierId here, because DB will auto-generate it
	}

    //   DTO for  reading supplier details (GET)
    public class SupplierReadDto : SupplierBaseDto
	{
		public int SupplierId { get; set; } // Needed to identify which supplier to update
	}

	//   DTO for update
	public class SupplierUpdateDto 
	{
        public string? Name { get; set; }     // Supplier name
        public string? Email { get; set; }    // Supplier email
        public string? MobileNo { get; set; }    // Supplier mobile number
        public string? GstNo { get; set; }     // GST Number
        public string? Address { get; set; }   // Supplier address
    } 

	//   DTO for deleting a supplier (DELETE)
	public class SupplierDeleteDto
	{
		public int SupplierId { get; set; } // Only ID is required for deletion
	}
}
