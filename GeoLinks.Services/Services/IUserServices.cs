using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.Entities.Modals;

namespace GeoLinks.Services.Services;

public interface IUserServices
{
    Task AddUserAsync(Users user);
    Task UpdateUserAsync(Users user);
    Task ResetPasswordAsync(int userId, string newPassword);
    Task DeleteUserAsync(int userId);
    Task UpdateRolesAsync(int userId, string newRole);
    Task<IEnumerable<Users>> GetAllUsersAsync();
    Task<Users?> VerifyUserAsync(string username, string password);
}
