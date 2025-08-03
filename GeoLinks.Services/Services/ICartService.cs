using System.Collections.Generic;
using GeoLinks.Entities.Modals;

namespace GeoLinks.Services.Services
{
    public interface ICartService
    {
        void AddToCart(CartItemModal cartItem);
        void RemoveFromCart(CartItemModal cartItem);
        void UpdateCartItem(CartItemModal cartItem);
        IEnumerable<CartItemModal> GetCartItems(int userId);
        void ClearCart(int userId);
    }
}