using GeoLinks.Authentication;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLinks.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private IProfileDal profileService;
        
        public ProfileService(IProfileDal profileService)
        {
            this.profileService = profileService;
        }
        public ProfileModal GetProfile(string user)
        {
            //return this.profileService.GetProfile(user);
            return new ProfileModal
            {
                FName = "Manohar",
                BloodGroup = "A+",
                CreatedOn = DateTime.Now,
                IsActive = true,
                LName = "R",
                mailId = "manohar@gmail.com",
                ModifiedOn =  DateTime.Now,
                PhoneNumber = "9901351377",
                ProfileId = 1,
                UserName = "manohar"
            };
        }
    }
}
