using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Authentication;
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
using System.Threading.Tasks;

namespace GeoLinks.API.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : AppControllerBase
    {
        private IConfiguration configuration;
        private IAuthService authService;
        private IProfileService profileService;
        private IEmailService emailService;
        private ISmsService smsService;

        public AuthController(IAuthService authService, IConfiguration configuration, IProfileService profileService, IEmailService emailService, ISmsService smsService)
        {
            this.authService = authService;
            this.configuration = configuration;
            this.profileService = profileService;
            this.emailService = emailService;
            this.smsService = smsService;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public ActionResult RegisterUser([FromBody]ProfileModal profileModal)
        {
            int profileId = this.authService.RegisterUser(profileModal);


            if (profileId == 0)
                return Conflict("Profile Already Exists");
            else
                return Created("", profileId);
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody] ProfileModal profileModal)
        {
            try
            {
                bool isValidUser = this.authService.ValidateUser(profileModal.mailId, profileModal.PhoneNumber, profileModal.Password);
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
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            
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
                new Claim("ProfileId", profileModal.ProfileId.ToString()),
                //new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            // Note: This method is not async, so you can't use 'await' here.
            // If you want to sign in, you need to make this method async and return Task<string>
            // No need to sign in with cookies when using JWT authentication.
            // Set HttpContext.User with the generated claims principal (optional, for current request context)
            var identity = new ClaimsIdentity(claims, "jwt");
            var principal = new ClaimsPrincipal(identity);
            HttpContext.User = principal;

            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],this.configuration["Jwt:Issuer"],claims,expires: DateTime.Now.AddMinutes(5),signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [Route("logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            // JWT tokens are stateless and cannot be "cleared" server-side.
            // To "logout", instruct the client to remove the token.
            // Optionally, you can add the token to a denylist (not implemented here).

            // Clear the session if session is enabled
            var sessionFeature = HttpContext.Features.Get<Microsoft.AspNetCore.Http.Features.ISessionFeature>();
            if (sessionFeature != null && HttpContext.Session != null)
            {
            HttpContext.Session.Clear();
            }

            // Instruct client to remove JWT (e.g., by removing from storage)
            return Ok(new { message = "Logged out successfully. Please remove the JWT token from your client." });
        }

        [AllowAnonymous]
        [HttpPost("reset-password-request")]
        public async Task<IActionResult> ResetPasswordRequest([FromBody] string userIdentifier)
        {
            // userIdentifier can be email or phone number
            var user = await authService.FindUserByEmailOrPhoneAsync(userIdentifier);
            if (user == null)
                return NotFound("User not found.");

            // Generate OTP
            var otp = new Random().Next(100000, 999999).ToString();

            // Store OTP in DB or cache with expiry (pseudo-code)
            await authService.StoreOtpAsync(user.ProfileId, otp);

            // Send OTP to email or phone
            if (!string.IsNullOrEmpty(user.mailId))
            {
                await emailService.SendAsync(user.mailId, "Your OTP Code", $"Your OTP is: {otp}");
            }
            else if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                await smsService.SendAsync(user.PhoneNumber, $"Your OTP is: {otp}");
            }

            return Ok("OTP sent.");
        }

        [AllowAnonymous]
        [HttpPost("reset-password-confirm")]
        public async Task<IActionResult> ResetPasswordConfirm([FromBody] ResetPasswordModel model)
        {
            // model: { UserId, Otp, NewPassword }
            var isValid = await authService.ValidateOtpAsync(model.UserId, model.Otp);
            if (!isValid)
                return BadRequest("Invalid OTP.");

            await authService.UpdatePasswordAsync(model.UserId, model.NewPassword);
            return Ok("Password reset successful.");
        }
        
        [AllowAnonymous]
        [HttpPost("login-otp-request")]
        public async Task<IActionResult> LoginOtpRequest([FromBody] string userIdentifier)
        {
            var user = await authService.FindUserByEmailOrPhoneAsync(userIdentifier);
            if (user == null)
                return NotFound("User not found.");

            var otp = new Random().Next(100000, 999999).ToString();
            await authService.StoreOtpAsync(user.ProfileId, otp);

            if (!string.IsNullOrEmpty(user.mailId))
            {
                await emailService.SendAsync(user.mailId, "Your login OTP", $"Your OTP is: {otp}");
            }
            else if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                await smsService.SendAsync(user.PhoneNumber, $"Your OTP is: {otp}");
            }

            return Ok(new { ProfileId = user.ProfileId, message = "OTP sent." });
        }

        [AllowAnonymous]
        [HttpPost("login-otp-verify")]
        public async Task<IActionResult> LoginOtpVerify([FromBody] LoginOtpVerifyModel model)
        {
            var isValid = await authService.ValidateOtpAsync(model.ProfileId, model.Otp);
            if (!isValid)
                return BadRequest("Invalid OTP.");

            // fetch profile to issue token
            var profile = (authService as dynamic)?.GetProfileById != null
                ? (authService as dynamic).GetProfileById(model.ProfileId) as ProfileModal
                : this.profileService.GetProfile(""); // fallback - adapt as needed

            if (profile == null)
                return NotFound("Profile not found.");

            var jwttoken = GenerateJsonWebToken(profile);
            return Ok(new { token = jwttoken, userInfo = profile });
        }
    }
}