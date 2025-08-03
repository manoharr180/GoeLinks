using GeoLinks.DataLayer.DalInterface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GeoLinks.Entities.DbEntities;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class StoreRepository : IStoreRepository
    {
        private readonly GeoLensContext _context;
        private IMapper mapper;

        public StoreRepository(GeoLensContext context)
        {
            _context = context;

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
            var data = await _context.stores
            .Include(s => s.StoreItemDetails)
            .ToListAsync();
            return mapper.Map<List<StoreDto>, List<Store>>(data);
        }

        public async Task<StoreDto> GetStoreByIdAsync(string id)
        {
            return await _context.stores
            .Include(s => s.StoreItemDetails)
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