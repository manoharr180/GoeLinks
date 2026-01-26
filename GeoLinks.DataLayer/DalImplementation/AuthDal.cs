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
        private IMapper mapper;
        private Authentication.PasswordHasher<ProfileDto> passwordHasher = new Authentication.PasswordHasher<ProfileDto>();
        public AuthDal(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProfileDto, ProfileModal>();
                cfg.CreateMap<ProfileModal, ProfileDto>();
            });
            mapper = mapperConfig.CreateMapper();
        }
        public int RegisterUser(ProfileModal profileModal)
        {
            this.unitOfWork.CreateTransaction();
            try
            {
                var usersDto = new UsersDto()
                {
                    Email = profileModal.mailId,
                    PhoneNumber = profileModal.PhoneNumber,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                    Username = profileModal.UserName,
                    IsActive = true
                };
                string password = profileModal.Password;
                ProfileDto profileDto = mapper.Map<ProfileModal, ProfileDto>(profileModal);


                this.unitOfWork.GenericProfileRepository.Insert(profileDto);
                this.unitOfWork.GenericProfileRepository.Save();
                if (profileDto.ProfileId > 0)
                {
                    string hashedPassword = passwordHasher.HashPassword(profileDto, password);
                    this.unitOfWork.GenericPasswordRepository.Insert(new UsersDto()
                    {
                        ProfileId = profileDto.ProfileId,
                        PasswordHash = hashedPassword,
                        IsActive = false,
                        NumOfLogInAttempt = 0,
                        CreatedOn = DateTime.UtcNow,
                        ModifiedOn = DateTime.UtcNow,
                        Email = profileModal.mailId,
                        PhoneNumber = profileModal.PhoneNumber,
                        Username = profileModal.UserName
                    });
                    this.unitOfWork.GenericPasswordRepository.Save();
                    this.unitOfWork.Commit();
                }
                return profileDto.ProfileId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                throw;
            }
            
        }

        public bool ValidateUser(string userMail, string phoneNum, string password)
        {
            SqlParameter paras =  new SqlParameter("user", userMail);

            string query = string.Format("SELECT usr.\"ProfileId\", usr.\"Email\", usr.\"PasswordHash\", usr.\"IsActive\", usr.\"NumOfLogInAttempt\" " +
                           "FROM users usr " +
                           "INNER JOIN profiledetails ps ON usr.\"ProfileId\" = ps.ProfileId " +
                           "WHERE ps.mailid = '{0}'", userMail);

            List<LoginUser> loginUser = this.unitOfWork.GenericLogInUserRepository
            .GetAll(query).ToList();
            if (loginUser.Count > 1 || loginUser.Count == 0)
                return false;

            PasswordVerificationResult passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, loginUser[0].PasswordHash, password);
            if (passwordVerificationResult == PasswordVerificationResult.Success)
                return true;
            else
                return false;
        }
    }
}
