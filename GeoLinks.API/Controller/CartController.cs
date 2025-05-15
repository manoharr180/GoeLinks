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

        [HttpGet("{userId}")]
        public IActionResult GetCartItems(int userId)
        {
            this.cartService.GetCartItems(userId);
            var userCart = Cart.FindAll(item => item.UserId == userId);
            return Ok(userCart);
        }

        [HttpPost]
        public IActionResult AddItemToCart([FromBody] CartItemModal newItem)
        {
            Cart.Add(newItem);
            return CreatedAtAction(nameof(GetCartItems), new { userId = newItem.UserId }, newItem);
        }

        [HttpPut("{userId}/{storeId}/{itemNumber}")]
        public IActionResult UpdateCartItem(int userId, string storeId, int itemNumber, [FromBody] CartItemModal updatedItem)
        {
            var existingItem = Cart.Find(item => item.UserId == userId && item.StoreId == storeId && item.ItemNumber == itemNumber);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Quantity = updatedItem.Quantity;
            return NoContent();
        }

        [HttpDelete("{userId}/{storeId}/{itemNumber}")]
        public IActionResult DeleteCartItem(int userId, string storeId, int itemNumber)
        {
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