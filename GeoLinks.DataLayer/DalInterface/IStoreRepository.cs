using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLinks.DataLayer.DalInterface
{
    public interface IStoreRepository
    {
        Task<List<StoresDto>> GetAllStoresAsync();
        Task<StoreDto> GetStoreByIdAsync(int storeId);
        Task AddStoreAsync(StoreDto store);
        Task UpdateStoreAsync(StoreDto store);
        Task DeleteStoreAsync(int storeId);
    }
}