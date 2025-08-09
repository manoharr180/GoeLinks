using GeoLinks.Authentication;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Amazon.S3;
using Amazon.S3.Model;
using System.Threading.Tasks;
using System.IO;

namespace GeoLinks.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileDal profileService;
        private readonly IExternalApiService externalApiService;

        public ProfileService(IProfileDal profileService, IExternalApiService externalApiService)
        {
            this.profileService = profileService;
            this.externalApiService = externalApiService;
        }

        public ProfileModal GetProfile(string user)
        {
            return this.profileService.GetProfile(user);
        }

        public async Task<string> GetS3ObjectContentAsync()
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = "manva-stores-data",
                    Key = "data.json"
                };

                return string.Empty;
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                throw new Exception($"Error getting object from S3: {ex.Message}", ex);
            }
        }
    }
}
