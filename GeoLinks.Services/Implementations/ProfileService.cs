using GeoLinks.Authentication;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using System;
using Microsoft.Extensions.Logging;
using System.Text;
using Amazon.S3;
using Amazon.S3.Model;
using System.Threading.Tasks;
using System.IO;
using Amazon.Runtime.Internal.Util;

namespace GeoLinks.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileDal profileService;
        private readonly IExternalApiService externalApiService;
        private readonly ILogger<ProfileService> _logger;

        public ProfileService(IProfileDal profileService, IExternalApiService externalApiService, ILogger<ProfileService> logger)
        {
            this.profileService = profileService;
            this.externalApiService = externalApiService;
            this._logger = logger;
        }

        public ProfileModal GetProfile(string user)
        {
            _logger.LogInformation("Retrieving profile for user: {User}", user);
            return this.profileService.GetProfile(user);
        }
        public ProfileModal GetProfileById(int profileId)
        {
            _logger.LogInformation("Retrieving profile for ID: {ProfileId}", profileId);
            return this.profileService.GetProfileById(profileId);
        }

        public async Task<string> GetS3ObjectContentAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to get object from S3 bucket 'manva-stores-data' with key 'data.json'");
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
                _logger.LogError(ex, "Error getting object from S3 bucket 'manva-stores-data' with key 'data.json'");
                throw new Exception($"Error getting object from S3: {ex.Message}", ex);
            }
        }
    }
}
