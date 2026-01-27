using AutoMapper;
using GeoLinks.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoLinks.Services.Services
{
    public interface IProfileService
    {
        ProfileModal GetProfile(string user);
        ProfileModal GetProfileById(int profileId);
        Task<string> GetS3ObjectContentAsync();
    }
}
