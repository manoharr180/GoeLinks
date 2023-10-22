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
            return this.profileService.GetProfile(user);
        }
    }
}
