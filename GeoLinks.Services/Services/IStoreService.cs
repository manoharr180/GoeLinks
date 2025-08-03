using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLinks.Services.Services
{
    public interface IStoreService
    {
        Task<List<Store>> GetAllStoresAsync();
        Task<Store> GetStoreByIdAsync(string storeId);
        Task<bool> CreateStoreAsync(Store store);
        Task<bool> UpdateStoreAsync(Store store);
        Task<bool> DeleteStoreAsync(string storeId);
    }
}