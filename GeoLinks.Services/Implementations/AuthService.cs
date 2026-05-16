using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using System;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;

namespace GeoLinks.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private IAuthDal authService;
        private ILogger<AuthService> _logger;
        public AuthService(IAuthDal authService, ILogger<AuthService> logger)
        {
            this.authService = authService;
            this._logger = logger;
        }

        public bool ValidateUser(string userMail, string phoneNum, string password)
        {
            _logger.LogInformation("Attempting to validate user with email: {Email} and phone number: {PhoneNumber}", userMail, phoneNum);
            return this.authService.ValidateUser(userMail,phoneNum,password);
        }

        public int RegisterUser(ProfileModal profileModal)
        {
            profileModal.CreatedOn = DateTime.UtcNow;
            profileModal.ModifiedOn = DateTime.UtcNow;
            profileModal.IsActive = true;
            _logger.LogInformation("Attempting to register new user with email: {Email} and phone number: {PhoneNumber}", profileModal.mailId, profileModal.PhoneNumber);
            return this.authService.RegisterUser(profileModal);
        }

        public Task<ProfileModal> FindUserByEmailOrPhoneAsync(string userIdentifier)
        {
            _logger.LogInformation("Attempting to find user by email or phone number: {UserIdentifier}", userIdentifier);
            return Task.FromResult(authService.FindUserByEmailOrPhone(userIdentifier));
        }

        public Task StoreOtpAsync(int userId, string otp)
        {
            _logger.LogInformation("Attempting to store OTP for user ID: {UserId}", userId);
            authService.StoreOtp(userId, otp);
            return Task.CompletedTask;
        }

        public Task<bool> ValidateOtpAsync(int userId, string otp)
        {
            _logger.LogInformation("Attempting to validate OTP for user ID: {UserId}", userId);
            return Task.FromResult(authService.ValidateOtp(userId, otp));
        }

        public Task UpdatePasswordAsync(int userId, string newPassword)
        {
            _logger.LogInformation("Attempting to update password for user ID: {UserId}", userId);
            authService.UpdatePassword(userId, newPassword);
            return Task.CompletedTask;
        }
    }
}
