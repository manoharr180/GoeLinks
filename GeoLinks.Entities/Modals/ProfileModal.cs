using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.Entities.Modals
{
    public class ProfileModal
    {
        public int ProfileId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string mailId { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string BloodGroup { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
