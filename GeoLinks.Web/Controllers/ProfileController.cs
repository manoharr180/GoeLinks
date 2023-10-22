using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoLinks.Web.Controllers
{
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
        public ActionResult GetProfile()
        {
            return Ok(this.profileService.GetProfile("", ""));
        }
    }
}