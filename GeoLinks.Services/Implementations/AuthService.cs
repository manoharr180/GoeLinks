using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

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
            profileModal.CreatedOn = DateTime.Now;
            profileModal.ModifiedOn = DateTime.Now;
            profileModal.IsActive = true;
            return this.authService.RegisterUser(profileModal);
        }
    }
}
