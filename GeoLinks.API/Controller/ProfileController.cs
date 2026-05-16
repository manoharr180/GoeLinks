using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GeoLinks.API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : AppControllerBase
    {
        private IProfileService profileService;
        private ILogger<ProfileController> _logger;
        public ProfileController(IProfileService profileService, ILogger<ProfileController> logger)
        {
            this.profileService = profileService;
            this._logger = logger;
        }
        [HttpGet]
        public IActionResult GetProfile([FromQuery] string mail)
        {

            _logger.LogInformation("Request started to get profile for email: {Email}", mail);  
            return this.profileService.GetProfile(mail) != null ? 
                Ok(this.profileService.GetProfile(mail)) : 
                NotFound("Profile not found.");
        }

    }
}