using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GeoLinks.Entities.Modals;
using GeoLinks.Services.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GeoLinks.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartService  cartService;
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        private static readonly List<CartItemModal> Cart = new List<CartItemModal>();

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

            var userCart = Cart.FindAll(item => item.UserId == userId);
            return Ok(userCart);
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
            Cart.Add(newItem);
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
            var existingItem = Cart.Find(item => item.UserId == userId && item.StoreId == storeId && item.ItemNumber == itemNumber);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Quantity = updatedItem.Quantity;
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
            var itemToRemove = Cart.Find(item => item.UserId == userId && item.StoreId == storeId && item.ItemNumber == itemNumber);
            if (itemToRemove == null)
            {
                return NotFound();
            }

            Cart.Remove(itemToRemove);
            return NoContent();
        }
    }

}