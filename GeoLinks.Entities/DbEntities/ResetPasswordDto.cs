
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("resetpassword")]
public class ResetPasswordDto
{
    [Key]
    [Column("resetpasswordid")]
    public int Id { get; set; }
    [Column("userid")]
    public int UserId { get; set; }
    [Column("otp")]
    public string Otp { get; set; }
    [Column("isexpired")]
    public bool IsExpired { get; set; }
    [Column("created_on")]
    public DateTime CreatedOn { get; set; }
    [Column("modified_on")]
    public DateTime ModifiedOn { get; set; }
}