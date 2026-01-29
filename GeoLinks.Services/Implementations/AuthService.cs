using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoLinks.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private IAuthDal authService;
        public AuthService(IAuthDal authService)
        {
            this.authService = authService;
        }

        public bool ValidateUser(string userMail, string phoneNum, string password)
        {
            return this.authService.ValidateUser(userMail,phoneNum,password);
        }

        public int RegisterUser(ProfileModal profileModal)
        {
            profileModal.CreatedOn = DateTime.UtcNow;
            profileModal.ModifiedOn = DateTime.UtcNow;
            profileModal.IsActive = true;
            return this.authService.RegisterUser(profileModal);
        }

        public Task<ProfileModal> FindUserByEmailOrPhoneAsync(string userIdentifier)
        {
            return Task.FromResult(authService.FindUserByEmailOrPhone(userIdentifier));
        }

        public Task StoreOtpAsync(int userId, string otp)
        {
            authService.StoreOtp(userId, otp);
            return Task.CompletedTask;
        }

        public Task<bool> ValidateOtpAsync(int userId, string otp)
        {
            return Task.FromResult(authService.ValidateOtp(userId, otp));
        }

        public Task UpdatePasswordAsync(int userId, string newPassword)
        {
            authService.UpdatePassword(userId, newPassword);
            return Task.CompletedTask;
        }
    }
}
