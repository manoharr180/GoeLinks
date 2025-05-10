using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.Services.Services;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace GeoLinks.Services.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly IExternalApiService _externalApiService;
        public StoreService(IExternalApiService externalApiService)
        {
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

        public Task<Stores> GetAllStoresAsync()
        {
            // return this._externalApiService.ReadAsync<List<Store>>("https://manva-store-data.s3.us-east-1.amazonaws.com/storedata.json")
            //     .ContinueWith(task => (IEnumerable<Store>)task.Result);
            var json = File.ReadAllText("../GeoLinks.Services/data.json");
            return Task.FromResult(JsonSerializer.Deserialize<Stores>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }));
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