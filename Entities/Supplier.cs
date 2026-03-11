namespace InvoiceAPI.Entity
{
    public class Supplier
    {   
        public int SupplierId { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string MobileNo { get; set; } 
        public string GstNo { get; set; }= string.Empty;
        public string Address { get; set; } 
    }
}
