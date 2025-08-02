using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.Entities.DbEntities;

namespace GeoLinks.DataLayer.DalInterface
{
    public interface ICartRepository
    {
        // Define methods for the Cart repository
        Task AddToCartAsync(int userId, int productId, int quantity);
        Task RemoveFromCartAsync(int userId, int productId);
        Task UpdateCartItemAsync(int userId, int productId, int quantity);
        Task<IEnumerable<CartItemDto>> GetCartItemsAsync(int userId);
        Task ClearCartAsync(int userId);
    }
}