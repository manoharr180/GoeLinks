using System.Collections.Generic;
using GeoLinks.Entities.DbEntities;
using GeoLinks.DataLayer.DalInterface;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
namespace GeoLinks.DataLayer.DalImplementation
{
    public class CartRepository : ICartRepository
    {
        private readonly GeoLensContext _context;
        private IUnitOfWork unitOfWork { get; set; }
        private readonly IMapper mapper;

        public CartRepository(GeoLensContext context, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this._context = context;
        
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CartItemDto, CartItemModal>();
                cfg.CreateMap<CartItemModal, CartItemDto>();
                cfg.CreateMap<CartItemDetailsDto, CartItemModal>();
                cfg.CreateMap<CartItemModal, CartItemDetailsDto>();
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

        public async Task<List<CartItemModal>> GetCartItemsAsync(int userId)
        {
            try
            {
                var cartItems = await _context.CartItemsDetailsDto
                                    .FromSqlRaw("SELECT * FROM getcartitems({0})", userId)
                .ToListAsync();

                return mapper.Map<List<CartItemModal>>(cartItems);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error retrieving cart items", ex);
            }
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