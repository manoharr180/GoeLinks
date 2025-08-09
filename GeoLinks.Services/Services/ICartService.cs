using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.Entities.Modals;

namespace GeoLinks.Services.Services
{
    public interface ICartService
    {
        Task AddToCartAsync(CartItemModal cartItem);
        Task RemoveFromCartAsync(CartItemModal cartItem);
        Task UpdateCartItemAsync(CartItemModal cartItem);
        Task<IEnumerable<CartItemModal>> GetCartItemsAsync(int userId);
        Task ClearCartAsync(int userId);
    }
}