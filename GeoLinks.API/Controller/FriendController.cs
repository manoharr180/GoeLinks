using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using GeoLinks.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoLinks.API.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private IFriendService friendService;
        public FriendController(IFriendService friendService)
        {
            this.friendService = friendService;
        }

        [HttpPost]
        public ActionResult Post(int UserId, int FriendId)
        {
            try
            {
                return Created("", this.friendService.AddFriend(FriendId, UserId));
            }
            catch (Exception)
            {
                return StatusCode(500,"Not able to add friend");
            }
            
        }

        [HttpGet]
        public ActionResult GetAll(int userId)
        {

            try
            {
                var friends = this.friendService.GetFriends(userId).ToList();
                if (friends != null && friends.Count > 0)
                {
                    return Ok(friends);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500,"");  
            }
            
        }
    }
}