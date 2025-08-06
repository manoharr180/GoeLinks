using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;

namespace GeoLinks.Services.Implementations;

public class UserServices : IUserServices
{
private readonly IUserRepository _userRepository;
    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task AddUserAsync(Users user)
    {
        await _userRepository.AddUserAsync(user);
    }

    public async Task DeleteUserAsync(int userId)
    {
        await _userRepository.DeleteUserAsync(userId);
    }

    public async Task<IEnumerable<Users>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task ResetPasswordAsync(int userId, string newPassword)
    {
        await _userRepository.ResetPasswordAsync(userId, newPassword);
    }

    public async Task UpdateRolesAsync(int userId, string newRole)
    {
        await _userRepository.UpdateRolesAsync(userId, newRole);
    }

    public async Task UpdateUserAsync(Users user)
    {
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task<Users?> VerifyUserAsync(string username, string password)
    {
        return await _userRepository.VerifyUserAsync(username, password);
    }
}
