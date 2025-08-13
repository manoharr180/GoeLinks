using GeoLinks.Services.Services;
using GeoLinks.DataLayer.DalInterface;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.Entities.DbEntities;
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

        public CartService(ICartRepository cartRepository, IStoreService storeService)
        {
            _cartRepository = cartRepository;
            _storeService = storeService;
        }

        public async Task AddToCartAsync(CartItemModal cartItem)
        {
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
            await _cartRepository.RemoveFromCartAsync(cartItem);
        }

        public async Task UpdateCartItemAsync(CartItemModal cartItem)
        {
            await _cartRepository.UpdateCartItemAsync(cartItem);
        }

        public async Task<IEnumerable<CartItemModal>> GetCartItemsAsync(int userId)
        {
            var cartDetails = _cartRepository.GetCartItemsAsync(userId).Result;
            if (cartDetails == null || !cartDetails.Any())
            {
                return new List<CartItemModal>();
            }

            // Optionally, you can fetch store details for each cart item if needed
           
            return await _cartRepository.GetCartItemsAsync(userId);
        }

        public async Task ClearCartAsync(int userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }
    }
    
}