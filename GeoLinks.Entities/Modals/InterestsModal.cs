using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.Entities.Modals
{
    public class InterestsModal
    {
        public int InterestId { get; set; }
        public string Interest { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
