using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLinks.Entities.DbEntities
{
    [Table("cartitem")]
    public class CartItemDto
    {
        public CartItemDto()
        {
            CreatedOn = DateTime.UtcNow;
            IsItemAvailable = true;
            IsActive = true;
        }

        [Key]
        [Column("cartitemid")]
        public int CartItemId { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("storeid")]
        public string StoreId { get; set; }

        [Column("itemid")]
        public int ItemId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("createdon")]
        public DateTime CreatedOn { get; set; }

        [Column("isitemavailable")]
        public bool IsItemAvailable { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }
    }
}