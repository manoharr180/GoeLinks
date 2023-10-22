using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeoLinks.Entities.DbEntities
{
    public class LoginUser
    {
        public int ProfileId { get; set; }
        public string mailId { get; set; }
        public string HashedPassword { get; set; }
        public bool IsBlocked { get; set; }
        public Int16 NumOfLogInAttempt { get; set; }
    }
}
