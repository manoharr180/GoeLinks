using AutoMapper;
using GeoLinks.Authentication;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.DbEntities;
using GeoLinks.Entities.Modals;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class ProfilesDal : IProfileDal
    {
        private IUnitOfWork unitOfWork { get; set; }
        private MapperConfiguration mapperconfig;
        private ILogger<ProfilesDal> _logger;
        private IMapper mapper;
        private Authentication.PasswordHasher<ProfileDto> passwordHasher = new Authentication.PasswordHasher<ProfileDto>();
        public ProfilesDal(IUnitOfWork unitOfWork, ILogger<ProfilesDal> logger)
        {
            this.unitOfWork = unitOfWork;
            this._logger = logger;
            mapperconfig = AutoMapperConfig.CreateMapperConfig<ProfileDto, ProfileModal>();
            mapper = mapperconfig.CreateMapper();
        }
        public ProfileModal GetProfile(string userName)
        {
            try
            {
            _logger.LogInformation("Attempting to retrieve profile for user ID: {UserId}", userName);
            SqlParameter paras = new SqlParameter("user", userName);

            List<ProfileDto> loginUser = this.unitOfWork.GenericProfileRepository
                .GetAll("select * from profiledetails where mailId = {0}", userName).ToList();

            ProfileDto profileDto = new ProfileDto();
            if (loginUser.Count == 1)
            {
                profileDto = loginUser.FirstOrDefault();
            }
            _logger.LogInformation("Profile retrieved successfully for user ID: {UserId}", userName);
            return mapper.Map<ProfileDto, ProfileModal>(profileDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving profile for user ID: {UserId}", userName);
                throw;
            }
        }
        public ProfileModal GetProfileById(int profileId)
        {
            try
            {
                SqlParameter paras = new SqlParameter("profileId", profileId);

            List<ProfileDto> profileList = this.unitOfWork.GenericProfileRepository
                .GetAll("select * from profiledetails where profileid = {0}", profileId).ToList();

            ProfileDto profileDto = new ProfileDto();
            if (profileList.Count == 1)
            {
                profileDto = profileList.FirstOrDefault();
            }

            return mapper.Map<ProfileDto, ProfileModal>(profileDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving profile by ID: {ProfileId}", profileId);
                throw;
            }
        }
    }
}