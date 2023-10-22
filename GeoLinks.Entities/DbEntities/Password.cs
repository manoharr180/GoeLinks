using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeoLinks.Entities.DbEntities
{
    [Table("Profile_Password")]
    public class Password
    {
        [Key]
        public int ProfileId { get; set; }
        public string HashedPassword { get; set; }
        public int NumOfLogInAttempt { get; set; }
        public bool IsBlocked { get; set; }
    }
}
