using GeoLinks.DataLayer.DalInterface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GeoLinks.Entities.DbEntities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class StoreRepository : IStoreRepository
    {
        private readonly GeoLensContext _context;
        private IMapper mapper;
        private readonly ILogger<StoreRepository> _logger;
        public StoreRepository(GeoLensContext context, ILogger<StoreRepository> logger)
        {
            _context = context;
            _logger = logger;

            var mapperconfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StoreDto, Store>();
                cfg.CreateMap<Store, StoreDto>();
                cfg.CreateMap<StoreItemDetailsDto, StoreItemDetails>();
                cfg.CreateMap<StoreItemDetails, StoreItemDetailsDto>();
            });
            mapper = mapperconfig.CreateMapper();
        }

        public async Task<List<Store>> GetAllStoresAsync()
        {
            try{
                    _logger.LogInformation("Attempting to retrieve all stores");
                    var data = await _context.stores
                    .Include(s => s.StoreItemDetails)
                    .ToListAsync();
                    return mapper.Map<List<StoreDto>, List<Store>>(data);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error retrieving all stores");
                    throw;
                }
        }

        public async Task<StoreDto> GetStoreByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve store for store ID: {StoreId}", id);
                return await _context.stores
                .Include(s => s.StoreItemDetails)
                .FirstOrDefaultAsync(s => s.StoreId == id);
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error retrieving store for store ID: {StoreId}", id);
                throw;
            }
        }

        public async Task AddStoreAsync(StoreDto store)
        {
            try
            {
                _logger.LogInformation("Attempting to add new store with ID: {StoreId}", store.StoreId);
                _context.stores.Add(store);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new store with ID: {StoreId}", store.StoreId);
                throw;
            }
        }

        public async Task UpdateStoreAsync(StoreDto store)
        {
            try
            {
                _logger.LogInformation("Attempting to update store with ID: {StoreId}", store.StoreId);
                _context.stores.Update(store);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating store with ID: {StoreId}", store.StoreId);
                throw;
            }
            
        }
        public async Task DeleteStoreAsync(string id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete store with ID: {StoreId}", id);
                var store = await _context.stores.FirstOrDefaultAsync(s => s.StoreId == id);
                if (store != null)
                {
                    _context.stores.Remove(store);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting store with ID: {StoreId}", id);
                throw;
            }
        }
    }
}