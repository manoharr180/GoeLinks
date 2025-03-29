using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.Services.Services;

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

        public Task<IEnumerable<Store>> GetAllStoresAsync()
        {
            return this._externalApiService.ReadAsync<List<Store>>("https://manva-store-data.s3.us-east-1.amazonaws.com/storedata.json")
                .ContinueWith(task => (IEnumerable<Store>)task.Result);
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