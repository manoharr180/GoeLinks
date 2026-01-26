using System;

public class CartItemModal
{
    public int CartItemId { get; set; }
    public int UserId { get; set; }
    public string StoreId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; } = 1;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public bool IsItemAvailable { get; set; } = true;
    public bool IsActive { get; set; } = true;
    // Additional properties from storeitems
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public string Size { get; set; }
    public string Material { get; set; }
}