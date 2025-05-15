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
        public void AddToCart(int userId, int productId, int quantity)
        {
            // Add logic to add the product to the user's cart
        }

        public void RemoveFromCart(int userId, int productId)
        {
            // Add logic to remove the product from the user's cart
        }

        public void UpdateCartItem(int userId, int productId, int quantity)
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