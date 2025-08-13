using Microsoft.AspNetCore.Mvc;
using GeoLinks.Services.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GeoLinks.API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : AppControllerBase
    {
        private readonly ICartService cartService;
        private readonly IStoreService storeService;

        public CartController(ICartService cartService, IStoreService storeService)
        {
            this.cartService = cartService;
            this.storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            if (CurrentUserId == null)
                return Unauthorized("User ID not found in token.");

            var items = await cartService.GetCartItemsAsync(CurrentUserId.Value);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart([FromBody] CartItemModal newItem)
        {
            if (CurrentUserId == null)
                return Unauthorized("User ID not found in token.");

            newItem.UserId = CurrentUserId.Value;
            await cartService.AddToCartAsync(newItem);
            return CreatedAtAction(nameof(GetCartItems), new { userId = newItem.UserId }, newItem);
        }

        [HttpPut("{storeId}/{itemNumber}")]
        public async Task<IActionResult> UpdateCartItem(string storeId, int itemNumber, [FromBody] CartItemModal updatedItem)
        {
            if (CurrentUserId == null)
                return Unauthorized("User ID not found in token.");

            updatedItem.UserId = CurrentUserId.Value;
            // Optionally set storeId and itemNumber if needed
            await cartService.UpdateCartItemAsync(updatedItem);
            return NoContent();
        }

        [HttpDelete("{itemNumber}")]
        public async Task<IActionResult> DeleteCartItem(int itemNumber)
        {
            if (CurrentUserId == null)
                return Unauthorized("User ID not found in token.");

            // You may need to construct a CartItemModal or pass identifiers as needed
            var itemToRemove = new CartItemModal
            {
                CartItemId = itemNumber,
                UserId = CurrentUserId.Value
            };
            await cartService.RemoveFromCartAsync(itemToRemove);
            return Ok();
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteCartItem()
        {
            if (CurrentUserId == null)
                return Unauthorized("User ID not found in token.");

            
            await cartService.ClearCartAsync(CurrentUserId.Value);
            return Ok();
        }
    }
}