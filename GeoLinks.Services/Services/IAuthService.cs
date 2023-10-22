using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.Services.Services
{
    public interface IAuthService
    {
        int RegisterUser(ProfileModal profileModal);
        bool ValidateUser(string userMail, string phoneNum, string password);
    }
}
