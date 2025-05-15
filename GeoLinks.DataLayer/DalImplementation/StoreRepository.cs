using GeoLinks.DataLayer.DalInterface;
using GeoLinks.DataLayer.GenericRepository;
using GeoLinks.Entities.DbEntities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoLinks.DataLayer; 
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class StoreRepository : IStoreRepository
    {
        private readonly GeoLensContext _context;

        public StoreRepository(GeoLensContext context)
        {
            _context = context;
        }

        public async Task<List<StoresDto>> GetAllStoresAsync()
        {
            return await _context.Stores.ToListAsync();
        }
        public async Task<StoreDto> GetStoreByIdAsync(int id)
        {
            return await _context.Store.FirstOrDefaultAsync(s => s.StoreId == id);
        }
        public Task AddStoreAsync(StoreDto store)
        {
            _context.Store.Add(store);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task UpdateStoreAsync(StoreDto store)
        {
            _context.Store.Update(store);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteStoreAsync(int id)
        {
            var store = _context.Store.FirstOrDefault(s => s.StoreId == id);
            if (store != null)
            {
                _context.Store.Remove(store);
                _context.SaveChanges();
                
            }
            return Task.CompletedTask;
        }
    }
}