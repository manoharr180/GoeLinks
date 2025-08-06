
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.Entities.Modals;
namespace GeoLinks.DataLayer.DalInterface;



/// <summary>
/// Repository interface for user management and AAA operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Adds a new user.
    /// </summary>
    Task AddUserAsync(Users user);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    Task UpdateUserAsync(Users user);

    /// <summary>
    /// Resets the password for a user.
    /// </summary>
    Task ResetPasswordAsync(int userId, string newPassword);

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    Task DeleteUserAsync(int userId);

    /// <summary>
    /// Updates the role for a user.
    /// </summary>
    Task UpdateRolesAsync(int userId, string newRole);

    /// <summary>
    /// Gets all users.
    /// </summary>
    Task<IEnumerable<Users>> GetAllUsersAsync();

    /// <summary>
    /// Verifies a user's credentials (for login/authentication).
    /// </summary>
    Task<Users?> VerifyUserAsync(string username, string password);
}
