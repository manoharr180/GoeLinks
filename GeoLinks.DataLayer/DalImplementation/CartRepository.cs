using System.Collections.Generic;
using GeoLinks.Entities.DbEntities;
using GeoLinks.DataLayer.DalInterface;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System.Data.Entity;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class CartRepository : ICartRepository
    {
        private readonly GeoLensContext _context;
        private readonly IMapper mapper;

        public CartRepository(GeoLensContext context)
        {
            _context = context;
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CartItemDto, CartItemModal>();
                cfg.CreateMap<CartItemModal, CartItemDto>();
            });
            mapper = mapperConfig.CreateMapper();
        }

        public async Task AddToCartAsync(CartItemModal cartItemModal)
        {
            var cartItemDto = mapper.Map<CartItemDto>(cartItemModal);
            _context.CartItemsDto.Add(cartItemDto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(CartItemModal cartItemModal)
        {
            var cartItemDto = await _context.CartItemsDto
                .FirstOrDefaultAsync(c =>
                    c.CartItemId == cartItemModal.CartItemId &&
                    c.UserId == cartItemModal.UserId);

            if (cartItemDto != null)
            {
                _context.CartItemsDto.Remove(cartItemDto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCartItemAsync(CartItemModal cartItemModal)
        {
            var cartItemDto = await _context.CartItemsDto
                .FirstOrDefaultAsync(c =>
                    c.CartItemId == cartItemModal.CartItemId &&
                    c.UserId == cartItemModal.UserId);

            if (cartItemDto != null)
            {
                // Update properties
                cartItemDto.Quantity = cartItemModal.Quantity;
                cartItemDto.IsItemAvailable = cartItemModal.IsItemAvailable;
                cartItemDto.IsActive = cartItemModal.IsActive;
                cartItemDto.CreatedOn = cartItemModal.CreatedOn;
                // Add more property updates as needed

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CartItemModal>> GetCartItemsAsync(int userId)
        {
            var cartItems = await _context.CartItemsDto
                .Where(c => c.UserId == userId && c.IsActive)
                .ToListAsync();

            return mapper.Map<List<CartItemModal>>(cartItems);
        }

        public async Task ClearCartAsync(int userId)
        {
            var cartItems = _context.CartItemsDto
                .Where(c => c.UserId == userId);

            _context.CartItemsDto.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}