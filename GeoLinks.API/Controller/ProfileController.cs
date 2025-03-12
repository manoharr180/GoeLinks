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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace GeoLinks.API.Controller
{
    //[Authorize]
    [AllowAnonymous]
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
        public IActionResult GetProfile([System.Web.Http.FromUri]string mail)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            
            string json = string.Empty;

            using (StreamReader reader = new StreamReader("./Assets/data.json"))
            {
                json = reader.ReadToEnd();
            }
                
            return Ok(json);
        }

    }
}