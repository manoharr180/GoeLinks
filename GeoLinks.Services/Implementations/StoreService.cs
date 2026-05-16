using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLinks.Services.Services;
using System.IO;
using System.Text.Json;
using System.Linq;
using GeoLinks.DataLayer.DalInterface;
using Microsoft.Extensions.Logging;

namespace GeoLinks.Services.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IStoreRepository _storeRepository;
        private readonly ILogger<StoreService> _logger;

        public StoreService(IExternalApiService externalApiService, IStoreRepository storeRepository, ILogger<StoreService> logger)
        {
            _storeRepository = storeRepository;
            _externalApiService = externalApiService;
            _logger = logger;
        }
        public Task<bool> CreateStoreAsync(Store store)
        {
            _logger.LogInformation("Attempting to create store: {Store}", store);
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteStoreAsync(string storeId)
        {
            _logger.LogInformation("Attempting to delete store with ID: {StoreId}", storeId);
            throw new System.NotImplementedException();
        }

        public async Task<List<Store>>  GetAllStoresAsync()
        {
            _logger.LogInformation("Attempting to retrieve all stores from the repository.");
            var stores = (await _storeRepository.GetAllStoresAsync())
            .Where(s => s.IsActive);
            
            return stores.ToList();
        }

        public Task<Store> GetStoreByIdAsync(string storeId)
        {
            _logger.LogInformation("Attempting to retrieve store with ID: {StoreId}", storeId);
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateStoreAsync(Store store)
        {
            _logger.LogInformation("Attempting to update store with ID: {StoreId}", store.StoreId);
            throw new System.NotImplementedException();
        }

    }
}