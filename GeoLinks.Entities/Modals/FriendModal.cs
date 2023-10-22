using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.Entities.Modals
{
    public class FriendModal
    {
        public int ProfileId { get; set; }
        public int FriendProfileId { get; set; }
        public bool IsProfileBlocked { get; set; }
        public bool IsFriend { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
