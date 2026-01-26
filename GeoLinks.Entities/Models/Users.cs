using System;

namespace GeoLinks.Entities.Modals;

public class Users
{
    public int Id { get; set; }

    private string _username = string.Empty;
    public required string Username
    {
        get => _username;
        set => _username = value.ToLowerInvariant();
    }

    public required string PasswordHash { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}