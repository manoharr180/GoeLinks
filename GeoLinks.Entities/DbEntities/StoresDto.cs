using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeoLinks.Entities.DbEntities;
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
    [Column("createddatetime")]
    public DateTime CreatedDatetime { get; set; }
    [Column("modifieddatetime")]
    public DateTime ModifieDatetime { get; set; }
    [Column("isactive")]
    public bool IsActive { get; set; }

    public List<StoreItemDetailsDto> StoreItemDetails { get; set; }
}