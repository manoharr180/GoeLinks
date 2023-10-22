using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoLinks.API.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IProfileService profileService;
        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }
        [HttpGet]
        public ActionResult GetProfile([System.Web.Http.FromUri]string mail)
        {
            return Ok(this.profileService.GetProfile(mail));
        }

    }
}