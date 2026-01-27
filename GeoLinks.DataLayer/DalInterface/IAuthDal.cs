using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.DataLayer.DalInterface
{
    public interface IAuthDal
    {
        int RegisterUser(ProfileModal profileModal);
        bool ValidateUser(string userMail, string phoneNum, string password);
        bool StoreOtp(int userId, string otp);
        bool ValidateOtp(int userId, string otp);
        bool UpdatePassword(int userId, string newPassword);
        ProfileModal FindUserByEmailOrPhone(string emailOrPhone);
    }
}
