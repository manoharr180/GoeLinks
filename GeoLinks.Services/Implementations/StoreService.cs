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

        public Task<bool> DeleteStoreAsync(int storeId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Store>>  GetAllStoresAsync()
        {
            var stores = await _storeRepository.GetAllStoresAsync();

            //  var json = await File.ReadAllTextAsync("../GeoLinks.Services/data.json");
            // return JsonSerializer.Deserialize<Stores>(json, new JsonSerializerOptions
            // {
            //     PropertyNameCaseInsensitive = true
            // });
            return stores.ToList();
        }

        public Task<Store> GetStoreByIdAsync(int storeId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateStoreAsync(Store store)
        {
            throw new System.NotImplementedException();
        }

    }
}