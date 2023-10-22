using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GeoLinks.API.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration configuration;
        private IAuthService authService;
        private IProfileService profileService;
        public AuthController(IAuthService authService, IConfiguration configuration, IProfileService profileService)
        {
            this.authService = authService;
            this.configuration = configuration;
            this.profileService = profileService;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public ActionResult RegisterUser([FromBody]ProfileModal profileModal)
        {
            int profileId = 0;
            //for (int i=1; i<= 50; i++)
            //{
            //    profileId = this.authService.RegisterUser(new ProfileModal()
            //    {
            //        FName = "User"+i,
            //        LName = "Lname",
            //        BloodGroup= GenerateBloodGroup(),
            //        CreatedOn =  DateTime.Now,
            //        IsActive = true,
            //        mailId="user"+i+"@gmail.com",
            //        ModifiedOn = DateTime.Now,
            //        Password = "user"+i+"1234",
            //        PhoneNumber = "9901351374",
            //        UserName= "User"+i
            //    });
            //}

            profileId = this.authService.RegisterUser(profileModal);


            if (profileId == 0)
                return Conflict("Profile Already Exists");
            else
                return Created("", profileId);
        }

        public string GenerateBloodGroup()
        {
            string bloodGroup = string.Empty;
            string[] blood = { "A-", "A+", "B-","B+","O","O-","AB+","AB-"};

            Random random = new Random();
            int index = random.Next(blood.Length);
            bloodGroup = blood[index];

            return bloodGroup;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody]ProfileModal profileModal)
        {
            bool isValidUser = false;
            isValidUser = this.authService.ValidateUser(profileModal.mailId, profileModal.PhoneNumber, profileModal.Password);
            if (isValidUser)
            {
                string jwttoken = GenerateJsonWebToken(this.profileService.GetProfile(profileModal.mailId));
                return Ok(new
                {
                    token = jwttoken,
                    userInfo = this.profileService.GetProfile(profileModal.mailId)
                });
            }
            else
                return Unauthorized("Invalid username or password");
        }

        private string GenerateJsonWebToken(ProfileModal profileModal)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.NameId, profileModal.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, profileModal.mailId),
                    new Claim("phoneNum", profileModal.PhoneNumber),
                    new Claim("FName", profileModal.FName),
                    //new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],this.configuration["Jwt:Issuer"],claims,expires: DateTime.Now.AddMinutes(5),signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}