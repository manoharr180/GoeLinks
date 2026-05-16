using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using Microsoft.Extensions.Logging;

namespace GeoLinks.Services.Implementations;

public class UserServices : IUserServices
{
private readonly IUserRepository _userRepository;
private readonly ILogger<UserServices> _logger;

    public UserServices(IUserRepository userRepository, ILogger<UserServices> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    public async Task AddUserAsync(Users user)
    {
        _logger.LogInformation("Attempting to add user: {User}", user);
        await _userRepository.AddUserAsync(user);
    }

    public async Task DeleteUserAsync(int userId)
    {
        _logger.LogInformation("Attempting to delete user with ID: {UserId}", userId);
        await _userRepository.DeleteUserAsync(userId);
    }

    public async Task<IEnumerable<Users>> GetAllUsersAsync()
    {
        _logger.LogInformation("Attempting to retrieve all users from the repository.");
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task ResetPasswordAsync(int userId, string newPassword)
    {
        _logger.LogInformation("Attempting to reset password for user ID: {UserId}", userId);
        await _userRepository.ResetPasswordAsync(userId, newPassword);
    }

    public async Task UpdateRolesAsync(int userId, string newRole)
    {
        _logger.LogInformation("Attempting to update roles for user ID: {UserId} to new role: {NewRole}", userId, newRole);
        await _userRepository.UpdateRolesAsync(userId, newRole);
    }

    public async Task UpdateUserAsync(Users user)
    {
        _logger.LogInformation("Attempting to update user with ID: {UserId}", user.Id);
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task<Users> VerifyUserAsync(string username, string password)
    {
        _logger.LogInformation("Attempting to verify user with username: {Username}", username);
        return await _userRepository.VerifyUserAsync(username, password);
    }
}
