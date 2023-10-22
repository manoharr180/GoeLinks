using AutoMapper;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.DbEntities;
using GeoLinks.Entities.Modals;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class AuthDal : IAuthDal
    {
        private IUnitOfWork unitOfWork { get; set; }
        private MapperConfiguration mapperconfig;
        private IMapper mapper;
        private Authentication.PasswordHasher<ProfileDto> passwordHasher = new Authentication.PasswordHasher<ProfileDto>();
        public AuthDal(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            mapperconfig = AutoMapperConfig.CreateMapperConfig<ProfileDto, ProfileModal>();
            mapper = mapperconfig.CreateMapper();
        }
        public int RegisterUser(ProfileModal profileModal)
        {
            string password = profileModal.Password;
            ProfileDto profileDto = mapper.Map<ProfileModal, ProfileDto>(profileModal);
            this.unitOfWork.GenericProfileRepository.Insert(profileDto);
            this.unitOfWork.GenericProfileRepository.Save();
            if(profileDto.ProfileId > 0)
            {
                string hashedPassword = passwordHasher.HashPassword(profileDto, password);
                this.unitOfWork.GenericPasswordRepository.Insert(new Password()
                {
                    ProfileId = profileDto.ProfileId,
                    HashedPassword = hashedPassword,
                    IsBlocked = false,
                    NumOfLogInAttempt = 0
                }); 
                this.unitOfWork.GenericPasswordRepository.Save();
            }
            return profileDto.ProfileId;
        }

        public bool ValidateUser(string userMail, string phoneNum, string password)
        {
            SqlParameter paras =  new SqlParameter("user", userMail);
                       

            List<LoginUser> loginUser = this.unitOfWork.GenericLogInUserRepository.GetAll("select pf.ProfileId,pf.mailId,ps.HashedPassword,ps.IsBlocked, " +
                "ps.NumOfLogInAttempt from Profile_Details pf" +
                "  inner join Profile_Password ps on" +
                "  pf.ProfileId = ps.ProfileId where pf.mailId = {0}", userMail).ToList();
            if (loginUser.Count > 1 || loginUser.Count == 0)
                return false;

            PasswordVerificationResult passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, loginUser[0].HashedPassword, password);
            if (passwordVerificationResult == PasswordVerificationResult.Success)
                return true;
            else
                return false;
        }
    }
}
