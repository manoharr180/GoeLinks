using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoLinks.Services.Services
{
    public interface IAuthService
    {
        int RegisterUser(ProfileModal profileModal);
        bool ValidateUser(string userMail, string phoneNum, string password);

        // Add these for password reset and OTP
        Task<ProfileModal> FindUserByEmailOrPhoneAsync(string userIdentifier);
        Task StoreOtpAsync(int userId, string otp);
        Task<bool> ValidateOtpAsync(int userId, string otp);
        Task UpdatePasswordAsync(int userId, string newPassword);
    }
}
