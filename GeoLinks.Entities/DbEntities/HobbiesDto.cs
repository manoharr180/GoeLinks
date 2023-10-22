using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeoLinks.Entities.DbEntities
{
    [Table("Hobbies_Details")]
    public class HobbiesDto
    {
        [Key]
        public int HobbiesId { get; set; }
        public string Hobby { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
