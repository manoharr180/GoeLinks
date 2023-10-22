using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.Entities.DbEntities
{
    public class FriendDetailDto
    {
        public int FriendProfileId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserName { get; set; }
        public string BloodGroup { get; set; }
        public DateTime FrndAddedOn { get; set; }
    }
}
