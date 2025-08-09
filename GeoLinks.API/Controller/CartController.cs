using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace GeoLinks.API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartService  cartService;
        private IStoreService storeService;
        public CartController(ICartService cartService, IStoreService storeService)
        {
            // Initialize the cart service
            this.cartService = cartService;
            this.storeService = storeService;
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            
            // var userIdClaim = User.FindFirst("userId");
            // if (userIdClaim == null)
            // {
            //     return Unauthorized("User ID not found in token.");
            // }
            int userId = 1;
            //int.Parse(userIdClaim.Value);
            return Ok(this.cartService.GetCartItemsAsync(userId));
        }

        [HttpPost]
        public IActionResult AddItemToCart([FromBody] CartItemModal newItem)
        {
            // var userIdClaim = User.FindFirst("userId");
            // if (userIdClaim == null)
            // {
            //     return Unauthorized("User ID not found in token.");
            // }
            int userId = 1;
            //int.Parse(userIdClaim.Value);
            newItem.UserId = userId;
            // var itemDetails = this.storeService.GetStoreByIdAsync(newItem.StoreId).Result
            // .StoreItemDetails.Where(x => x.ItemId == newItem.ItemId).FirstOrDefault();
            // if (itemDetails == null)
            // {
            //     return NotFound($"Item with ID {newItem.ItemId} not found in store {newItem.StoreId}.");
            // }

            this.cartService.AddToCartAsync(newItem);
            return CreatedAtAction(nameof(GetCartItems), new { userId = newItem.UserId }, newItem);
        }

        [HttpPut("{storeId}/{itemNumber}")]
        public IActionResult UpdateCartItem( string storeId, int itemNumber, [FromBody] CartItemModal updatedItem)
        {
            // var userIdClaim = User.FindFirst("userId");
            // if (userIdClaim == null)
            // {
            //     return Unauthorized("User ID not found in token.");
            // }
            int userId = 1;
            //int.Parse(userIdClaim.Value);
            
            return NoContent();
        }

        [HttpDelete("{storeId}/{itemNumber}")]
        public IActionResult DeleteCartItem(string storeId, int itemNumber)
        {
            // var userIdClaim = User.FindFirst("userId");
            // if (userIdClaim == null)
            // {
            //     return Unauthorized("User ID not found in token.");
            // }
            int userId = 1;
            //int.Parse(userIdClaim.Value);
            return NoContent();
        }
    }

}