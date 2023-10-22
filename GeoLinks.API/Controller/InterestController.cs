using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoLinks.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private IInterestsService interestsService;
        public InterestController(IInterestsService interestsService)
        {
            this.interestsService = interestsService;
        }
        [HttpPost]
        public ActionResult Post([FromBody]InterestsModal interest)
        {
            return Created("",this.interestsService.AddInterest(interest));
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(this.interestsService.GetAllInterests());
        }
    }
}