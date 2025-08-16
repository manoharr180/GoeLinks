using System;

public class CartItemDetailsDto
{
    public string StoreId { get; set; }
    public int ItemId { get; set; }
    public int CartItemId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public string Size { get; set; }
    public string Material { get; set; }
}