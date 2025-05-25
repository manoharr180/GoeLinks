using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//                         ValidateIssuer = true,

public class StoreDto
{
    [Key]
    [Column("storeid")]
    public string StoreId { get; set; }

    [Column("storename")]
    public string StoreName { get; set; }

    [Column("storelogo")]
    public string StoreLogo { get; set; }

    public List<StoreItemDetailsDto> StoreItemDetails { get; set; }
}

[Table("storeitemdetails")]
public class StoreItemDetailsDto
{
    [Key]
    [Column("itemid")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ItemId { get; set; }

    [Column("storeid")]
    public string StoreId { get; set; }

    [ForeignKey("StoreId")]
    public StoreDto Store { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("price")]
    public string Price { get; set; }

    [Column("category")]
    public string Category { get; set; }

    [Column("brand")]
    public string Brand { get; set; }

    [Column("color")]
    public string Color { get; set; }

    [Column("size")]
    public string Size { get; set; }

    [Column("material")]
    public string Material { get; set; }

    [Column("quntity")]
    public string Quntity { get; set; }

    [Column("url")]
    public string Url { get; set; }

    [Column("imageurl")]
    public string ImageUrl { get; set; }
}