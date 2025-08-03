using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.Services.Services;
using System.IO;
using System.Text.Json;
using System.Linq;
using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.DbEntities;

namespace GeoLinks.Services.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IStoreRepository _storeRepository;

        public StoreService(IExternalApiService externalApiService, IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
            _externalApiService = externalApiService;
        }
        public Task<bool> CreateStoreAsync(Store store)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteStoreAsync(string storeId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Store>>  GetAllStoresAsync()
        {
            var stores = (await _storeRepository.GetAllStoresAsync())
            .Where(s => s.IsActive);
            
            return stores.ToList();
        }

        public Task<Store> GetStoreByIdAsync(string storeId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateStoreAsync(Store store)
        {
            throw new System.NotImplementedException();
        }

    }
}