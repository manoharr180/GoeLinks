using GeoLinks.Services.Services;
using GeoLinks.DataLayer.DalInterface;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.Entities.DbEntities;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoLinks.DataLayer.GenericRepository;

namespace GeoLinks.Services.Implementations
{
    public class CartService : ICartService
    {
        // Example properties and methods for the CartService class
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // Example method implementation from ICartService
        public void AddToCart(CartItemModal cartItem)
        {
            // Check if the item already exists in the cart
            var existingItem = _cartRepository.GetCartItemsAsync(cartItem.UserId).Result
                .FirstOrDefault(ci => ci.ItemId == cartItem.ItemId && ci.UserId == cartItem.UserId);
            if (existingItem != null)
            {
                // Increase the quantity
                existingItem.Quantity += cartItem.Quantity;
                _cartRepository.UpdateCartItemAsync(existingItem).Wait();
            }
            else
            {
                // Add new item to cart
                _cartRepository.AddToCartAsync(cartItem);
            }
        }

        public void RemoveFromCart(CartItemModal cartItem)
        {
            // Add logic to remove the product from the user's cart
        }

        public void UpdateCartItem(CartItemModal cartItem)
        {
            // Add logic to update the quantity of a product in the user's cart
        }

        public IEnumerable<CartItemModal> GetCartItems(int userId)
        {
            // Add logic to retrieve all items in the user's cart
            return new List<CartItemModal>();
        }

        public void ClearCart(int userId)
        {
            // Add logic to clear all items from the user's cart
        }
    }
    
}