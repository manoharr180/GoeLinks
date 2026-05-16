using GeoLinks.Services.Services;
using GeoLinks.DataLayer.DalInterface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using GeoLinks.DataLayer.GenericRepository;

namespace GeoLinks.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IStoreService _storeService;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository cartRepository, IStoreService storeService, ILogger<CartService> logger)
        {
            _cartRepository = cartRepository;
            _storeService = storeService;
            _logger = logger;
        }

        public async Task AddToCartAsync(CartItemModal cartItem)
        {
            _logger.LogInformation("Attempting to add item to cart for user ID: {UserId}", cartItem.UserId);
            var existingItems = await _cartRepository.GetCartItemsAsync(cartItem.UserId);
            var existingItem = existingItems.FirstOrDefault(ci => ci.ItemId == cartItem.ItemId && ci.UserId == cartItem.UserId);
            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
                await _cartRepository.UpdateCartItemAsync(existingItem);
            }
            else
            {
                await _cartRepository.AddToCartAsync(cartItem);
            }
        }

        public async Task RemoveFromCartAsync(CartItemModal cartItem)
        {
            _logger.LogInformation("Attempting to remove item from cart for user ID: {UserId}", cartItem.UserId);
            await _cartRepository.RemoveFromCartAsync(cartItem);
        }

        public async Task UpdateCartItemAsync(CartItemModal cartItem)
        {
            _logger.LogInformation("Attempting to update cart item for user ID: {UserId}", cartItem.UserId);
            await _cartRepository.UpdateCartItemAsync(cartItem);
        }

        public async Task<IEnumerable<CartItemModal>> GetCartItemsAsync(int userId)
        {
            _logger.LogInformation("Attempting to retrieve cart items for user ID: {UserId}", userId);
            var cartDetails = _cartRepository.GetCartItemsAsync(userId).Result;
            if (cartDetails == null || !cartDetails.Any())
            {
                return new List<CartItemModal>();
            }

            // Optionally, you can fetch store details for each cart item if needed
            _logger.LogInformation("Retrieved cart items for user ID: {UserId}", userId);
            return await _cartRepository.GetCartItemsAsync(userId);
        }

        public async Task ClearCartAsync(int userId)
        {
            _logger.LogInformation("Attempting to clear cart for user ID: {UserId}", userId);
            await _cartRepository.ClearCartAsync(userId);
        }
    }
    
}