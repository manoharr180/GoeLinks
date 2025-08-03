using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.Entities.DbEntities;

namespace GeoLinks.DataLayer.DalInterface
{
    public interface ICartRepository
    {
        // Define methods for the Cart repository
        Task AddToCartAsync(CartItemModal cartItemModal);
        Task RemoveFromCartAsync(CartItemModal cartItemModal);
        Task UpdateCartItemAsync(CartItemModal cartItemModal);
        Task<IEnumerable<CartItemModal>> GetCartItemsAsync(int userId);
        Task ClearCartAsync(int userId);
    }
}