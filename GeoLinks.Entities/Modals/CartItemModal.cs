using System;

public class CartItemModal
{
    public int CartItemId { get; set; }
    public int UserId { get; set; }
    public string StoreId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; } = 1;
    public System.DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public bool IsItemAvailable { get; set; } = true;
}