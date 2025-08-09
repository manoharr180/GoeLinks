using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace GeoLinks.API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : AppControllerBase
    {
        private IProfileService profileService;
        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }
        [HttpGet]
        public IActionResult GetProfile([FromQuery] string mail)
        {

            return this.profileService.GetProfile(mail) != null ? 
                Ok(this.profileService.GetProfile(mail)) : 
                NotFound("Profile not found.");
        }

    }
}