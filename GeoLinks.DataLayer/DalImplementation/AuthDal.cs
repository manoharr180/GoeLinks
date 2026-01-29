using AutoMapper;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.DbEntities;
using GeoLinks.Entities.Modals;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.ObjectPool;
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
        private Authentication.PasswordHasher<ResetPasswordDto> otpHasher = new Authentication.PasswordHasher<ResetPasswordDto>();
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
                    CreatedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    ModifiedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
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
                        CreatedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                        ModifiedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
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
            SqlParameter paras = new SqlParameter("user", userMail);

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
        public bool StoreOtp(int userId, string otp)
        {
            try
            {
                var userIdDto = 0;
                if (userId > 0)
                {
                    // Expire any existing non-expired OTP entries for this user
                    var existing = this.unitOfWork.GenericResetPasswordRepository
                        .GetAll($"SELECT * FROM resetpassword WHERE \"userid\" = {userId} AND \"isexpired\" IS NOT TRUE")
                        .ToList();

                    if (existing != null && existing.Count > 0)
                    {
                        foreach (var e in existing)
                        {
                            e.IsExpired = true;
                            e.ModifiedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
                            this.unitOfWork.GenericResetPasswordRepository.Update(e);
                        }
                        this.unitOfWork.GenericResetPasswordRepository.Save();
                    }

                    string hashedPassword = otpHasher.HashPassword(null, otp);
                    var resetPasswordDto = new ResetPasswordDto()
                    {
                        UserId = userId,
                        Otp = hashedPassword,
                        IsExpired = false,
                        CreatedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                        ModifiedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc)
                    };
                    unitOfWork.GenericResetPasswordRepository.Insert(resetPasswordDto);
                    unitOfWork.GenericResetPasswordRepository.Save();
                    userIdDto = resetPasswordDto.UserId;
                }
                return userIdDto > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                throw;
            }
        }
        public bool ValidateOtp(int userId, string otp)
        {
            try
            {
                string query = string.Format("SELECT rp.* " +
                                             "FROM resetpassword rp " +
                                             "WHERE rp.\"userid\" = {0} AND rp.\"isexpired\" IS NOT TRUE " +
                                             "ORDER BY rp.\"created_on\" DESC LIMIT 1", userId);

                var resetEntries = unitOfWork.GenericResetPasswordRepository
                    .GetAll(query)
                    .ToList();

                if (resetEntries == null || resetEntries.Count != 1)
                    return false;

                var storedHashedOtp = resetEntries[0].Otp;
                if (string.IsNullOrEmpty(storedHashedOtp))
                    return false;

                PasswordVerificationResult verificationResult = otpHasher.VerifyHashedPassword(null, storedHashedOtp, otp);
                return verificationResult == PasswordVerificationResult.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating OTP: {ex.Message}");
                return false;
            }
        }
        public bool UpdatePassword(int userId, string newPassword)
        {
            try
            {
                var users = unitOfWork.GenericPasswordRepository
                    .GetAll($"SELECT * FROM users WHERE \"ProfileId\" = {userId}")
                    .ToList();

                if (users == null || users.Count != 1)
                    return false;

                var user = users[0];
                string hashedPassword = passwordHasher.HashPassword(null, newPassword);
                user.PasswordHash = hashedPassword;

                // Ensure DateTime kinds are UTC to avoid PostgreSQL timestamptz errors
                // Normalize existing CreatedOn (if present) and set ModifiedOn to UTC now.
                user.CreatedOn = DateTime.SpecifyKind(user.CreatedOn, DateTimeKind.Utc);
                user.ModifiedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

                unitOfWork.GenericPasswordRepository.Update(user);
                unitOfWork.GenericPasswordRepository.Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating password: {ex.Message}");
                return false;
            }
        }
        public ProfileModal FindUserByEmailOrPhone(string emailOrPhone)
        {
            try
            {
                var users = this.unitOfWork.GenericProfileRepository
                    .GetAll($"SELECT * FROM profiledetails WHERE \"mailid\" = '{emailOrPhone}' OR \"phonenumber\" = '{emailOrPhone}'")
                    .ToList();

                if (users != null && users.Count > 0)
                {
                    return mapper.Map<ProfileDto, ProfileModal>(users[0]);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding user: {ex.Message}");
                return null;
            }
        }
    }   
}
