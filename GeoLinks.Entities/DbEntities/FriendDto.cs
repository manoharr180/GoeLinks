using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeoLinks.Entities.DbEntities
{
    [Table("FriendsList_Tbl")]
    
    public class FriendDto
    {
        [Key]
        public int FrndListId { get; set; }
        public int ProfileId { get; set; }
        public int FriendProfileId { get; set; }
        public bool IsProfileBlocked { get; set; }
        public bool IsFriend { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
