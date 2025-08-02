public class CartItemModal
{
    public int CartItemId { get; set; }
    public int UserId { get; set; }
    public string StoreId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public System.DateTime CreatedOn { get; set; }
    public bool IsItemAvailable { get; set; }
}