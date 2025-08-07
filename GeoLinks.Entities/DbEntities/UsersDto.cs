using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLinks.Entities.DbEntities
{
    [Table("users")]
    public class UsersDto
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Column("PasswordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [Column("Role")]
        public string Role { get; set; } = "User";

        [Required]
        [EmailAddress]
        [Column("Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [Column("PasswordSalt")]
        public string PasswordSalt { get; set; } = string.Empty;

        [Column("ProfileId")]
        public int? ProfileId { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; } = true;

        [Column("CreatedOn")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Column("ModifiedOn")]
        public DateTime? ModifiedOn { get; set; }

        [Column("NumOfLogInAttempt")]
        public int NumOfLogInAttempt { get; set; } = 0;

        // [ForeignKey("ProfileId")]
        // public ProfileDto? Profile { get; set; }
    }
}