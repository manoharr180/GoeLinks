using GeoLinks.DataLayer.DalInterface;
using GeoLinks.Entities.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class StoreRepository : IStoreRepository
    {
        private readonly GeoLensContext _context;

        public StoreRepository(GeoLensContext context)
        {
            _context = context;
        }

        public async Task<List<StoreDto>> GetAllStoresAsync()
        {
            var data = await _context.stores
            .Include(s => s.StoreItemDetails)
            .ToListAsync();
            return data;
        }

        public async Task<StoreDto> GetStoreByIdAsync(string id)
        {
            return await _context.stores
            //.Include(s => s.storeItems)
                .FirstOrDefaultAsync(s => s.StoreId == id);
        }

        public async Task AddStoreAsync(StoreDto store)
        {
            _context.stores.Add(store);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStoreAsync(StoreDto store)
        {
            _context.stores.Update(store);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStoreAsync(string id)
        {
            var store = await _context.stores.FirstOrDefaultAsync(s => s.StoreId == id);
            if (store != null)
            {
                _context.stores.Remove(store);
                await _context.SaveChangesAsync();
            }
        }
    }
}