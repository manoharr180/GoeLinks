using AutoMapper;
using GeoLinks.Authentication;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.DbEntities;
using GeoLinks.Entities.Modals;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class ProfilesDal : IProfileDal
    {
        private IUnitOfWork unitOfWork { get; set; }
        private MapperConfiguration mapperconfig;
        private IMapper mapper;
        private Authentication.PasswordHasher<ProfileDto> passwordHasher = new Authentication.PasswordHasher<ProfileDto>();
        public ProfilesDal(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            mapperconfig = AutoMapperConfig.CreateMapperConfig<ProfileDto, ProfileModal>();
            mapper = mapperconfig.CreateMapper();
        }
        public ProfileModal GetProfile(string userName)
        {
            SqlParameter paras = new SqlParameter("user", userName);

            List<ProfileDto> loginUser = this.unitOfWork.GenericProfileRepository
                .GetAll("select * from Profile_Details where mailId = {0}", userName).ToList();

            ProfileDto profileDto = new ProfileDto();
            if (loginUser.Count == 1)
            {
                profileDto = loginUser.FirstOrDefault();
            }
            
            return mapper.Map<ProfileDto,ProfileModal>(profileDto);
        }
    }
}