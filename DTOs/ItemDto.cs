namespace InvoiceAPI.Dto
{
    public class ItemDto
    {
        public int? ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int ItemQuantity { get; set; }
        public int ItemPrice { get; set; }
    }


public class ItemBaseDto
{
    public string ItemName { get; set; } = string.Empty;
    public int ItemQuantity { get; set; }
    public int ItemPrice { get; set; }
}

public class CreateItemRequestDto : ItemBaseDto
{
    // no extra fields
}

public class UpdateItemRequestDto
{
    public int ItemId { get; set; } // Required to identify the item

    public string? ItemName { get; set; } // optional
    public int? ItemQuantity { get; set; } // optional
    public int? ItemPrice { get; set; } // optional
}

}
