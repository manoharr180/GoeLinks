using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.DbEntities;
using GeoLinks.Entities.Modals;

namespace GeoLinks.DataLayer.DalImplementation;

public class UserRepository : IUserRepository
{
    private readonly GeoLensContext _context;
    private MapperConfiguration mapperconfig;
    private IMapper mapper;
    public UserRepository(GeoLensContext context)
    {
        _context = context;
            mapperconfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UsersDto, Users>();
                cfg.CreateMap<Users, UsersDto>();
                
            });
            mapper = mapperconfig.CreateMapper();
    }
    public async Task AddUserAsync(Users user)
    {
        // Generate salt
        var salt = Guid.NewGuid().ToString("N");
        // Hash password with salt
        var saltedPassword = user.PasswordHash + salt;
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(saltedPassword));
        var hash = Convert.ToBase64String(hashBytes);

        var userDto = mapper.Map<Users, UsersDto>(user);
        userDto.Id = 0; // Let DB auto-increment
        userDto.PasswordHash = hash;
        userDto.PasswordSalt = salt;
        _context.Users.Add(userDto);
        await _context.SaveChangesAsync();
        user.Id = userDto.Id;
    }

    public async Task DeleteUserAsync(int userId)
    {
        var userDto = await _context.Users.FindAsync(userId);
        if (userDto != null)
        {
            _context.Users.Remove(userDto);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Users>> GetAllUsersAsync()
    {
        var userDtos = await _context.Users.ToListAsync();
        return mapper.Map<List<UsersDto>, List<Users>>(userDtos);
    }

    public async Task ResetPasswordAsync(int userId, string newPassword)
    {
        var userDto = await _context.Users.FindAsync(userId);
        if (userDto != null)
        {
            // Generate new salt
            var salt = Guid.NewGuid().ToString("N");
            // Hash new password with new salt
            var saltedPassword = newPassword + salt;
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(saltedPassword));
            var hash = Convert.ToBase64String(hashBytes);

            userDto.PasswordHash = hash;
            userDto.PasswordSalt = salt;
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateRolesAsync(int userId, string newRole)
    {
        var userDto = await _context.Users.FindAsync(userId);
        if (userDto != null)
        {
            userDto.Role = newRole;
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateUserAsync(Users user)
    {
        var userDto = await _context.Users.FindAsync(user.Id);
        if (userDto != null)
        {
            userDto.Username = user.Username;
            userDto.Email = user.Email;
            userDto.PhoneNumber = user.PhoneNumber;
            // Do not update PasswordHash here
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Users?> VerifyUserAsync(string username, string password)
    {
        var userDto = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (userDto == null) return null;

        // Combine input password with stored salt and hash it
        var saltedPassword = password + userDto.PasswordSalt;
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(saltedPassword));
        var hash = Convert.ToBase64String(hashBytes);

        if (userDto.PasswordHash != hash) return null;

        return mapper.Map<UsersDto, Users>(userDto);
    }
}
