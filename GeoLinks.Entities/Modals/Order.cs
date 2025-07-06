using System;

namespace GeoLinks.Entities.Modals;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string UserId { get; set; } = null!;
    public string StoreId { get; set; } = null!;
    public string ItemId { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string OrderStatus { get; set; } = null!;
    public DateTime CreatedDatetime { get; set; }
}