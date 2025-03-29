using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLinks.Services.Services
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetAllStoresAsync();
        Task<Store> GetStoreByIdAsync(int storeId);
        Task<bool> CreateStoreAsync(Store store);
        Task<bool> UpdateStoreAsync(Store store);
        Task<bool> DeleteStoreAsync(int storeId);
    }
}