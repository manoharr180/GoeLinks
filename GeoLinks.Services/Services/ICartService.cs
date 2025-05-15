using System.Collections.Generic;
using GeoLinks.Entities.Modals;

namespace GeoLinks.Services.Services
{
    public interface ICartService
    {
        void AddToCart(int userId, int productId, int quantity);
        void RemoveFromCart(int userId, int productId);
        void UpdateCartItem(int userId, int productId, int quantity);
        IEnumerable<CartItemModal> GetCartItems(int userId);
        void ClearCart(int userId);
    }
}