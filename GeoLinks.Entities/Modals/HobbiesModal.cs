using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.Entities.Modals
{
    public class HobbiesModal
    {
        public int HobbiesId { get; set; }
        public string Hobby { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
