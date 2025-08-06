using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLinks.Entities.DbEntities
{
    [Table("profiledetails")]
    public class ProfileDto
    {
        [Key]
        [Column("profileid")]
        public int ProfileId { get; set; }

        [Column("fname")]
        public string FName { get; set; }

        [Column("lname")]
        public string LName { get; set; }

        [Column("mailid")]
        public string MailId { get; set; }

        [Column("phonenumber")]
        public string PhoneNumber { get; set; }

        [Column("bloodgroup")]
        public string BloodGroup { get; set; }

        [Column("username")]
        public string UserName { get; set; }

        [Column("createdon")]
        public DateTime CreatedOn { get; set; }

        [Column("modifiedon")]
        public DateTime ModifiedOn { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }
    }
}
