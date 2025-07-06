using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLinks.Entities.DbEntities;

[Table("orders")]
public class OrderDto
{
    [Key]
    [Column("orderid")]
    public int OrderId { get; set; }

    [Column("orderdate")]
    public DateTime OrderDate { get; set; }

    [Column("userid")]
    public string UserId { get; set; } = null!;

    [Column("storeid")]
    public string StoreId { get; set; } = null!;

    [Column("itemid")]
    public string ItemId { get; set; } = null!;

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("totalprice")]
    public decimal TotalPrice { get; set; }

    [Column("orderstatus")]
    public string OrderStatus { get; set; } = null!;

    [Column("createddatetime")]
    public DateTime CreatedDatetime { get; set; }
}
