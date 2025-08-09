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
            int profileId = this.authService.RegisterUser(profileModal);


            if (profileId == 0)
                return Conflict("Profile Already Exists");
            else
                return Created("", profileId);
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] ProfileModal profileModal)
        {
            try
            {
                bool isValidUser = this.authService.ValidateUser(profileModal.mailId, profileModal.PhoneNumber, profileModal.Password);
                if (isValidUser)
                {
                    string jwttoken = await GenerateJsonWebToken(this.profileService.GetProfile(profileModal.mailId));
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

        private async Task<string> GenerateJsonWebToken(ProfileModal profileModal)
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

        [Authorize]
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
    }
}