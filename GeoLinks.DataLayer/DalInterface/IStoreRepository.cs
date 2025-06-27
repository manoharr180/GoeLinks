using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLinks.DataLayer.DalInterface
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAllStoresAsync();
        Task<StoreDto> GetStoreByIdAsync(string storeId);
        Task AddStoreAsync(StoreDto store);
        Task UpdateStoreAsync(StoreDto store);
        Task DeleteStoreAsync(string storeId);
    }
}