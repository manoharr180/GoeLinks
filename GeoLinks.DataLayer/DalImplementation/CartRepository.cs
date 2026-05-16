using System.Collections.Generic;
using GeoLinks.Entities.DbEntities;
using GeoLinks.DataLayer.DalInterface;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
namespace GeoLinks.DataLayer.DalImplementation
{
    public class CartRepository : ICartRepository
    {
        private readonly GeoLensContext _context;
        private IUnitOfWork unitOfWork { get; set; }
        private ILogger<CartRepository> _logger;
        private readonly IMapper mapper;

        public CartRepository(GeoLensContext context, IUnitOfWork unitOfWork, ILogger<CartRepository> logger)
        {
            this.unitOfWork = unitOfWork;
            this._context = context;
            this._logger = logger;
        
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
            try
            {
                _logger.LogInformation("Attempting to add item to cart for user ID: {UserId}", cartItemModal.UserId);
                var cartItemDto = mapper.Map<CartItemDto>(cartItemModal);
                _context.CartItemsDto.Add(cartItemDto);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Item added to cart successfully for user ID: {UserId}", cartItemModal.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding item to cart for user ID: {UserId}", cartItemModal.UserId);
                throw;
            }
        }

        public async Task RemoveFromCartAsync(CartItemModal cartItemModal)
        {
            try
            {
                _logger.LogInformation("Attempting to remove item from cart for user ID: {UserId}", cartItemModal.UserId);
                var cartItemDto = await _context.CartItemsDto
                    .FirstOrDefaultAsync(c =>
                        c.CartItemId == cartItemModal.CartItemId &&
                        c.UserId == cartItemModal.UserId);


            if (cartItemDto != null)
            {
                _context.CartItemsDto.Remove(cartItemDto);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Item removed from cart successfully for user ID: {UserId}", cartItemModal.UserId);
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing item from cart for user ID: {UserId}", cartItemModal.UserId);
                throw;
            }
        }

        public async Task UpdateCartItemAsync(CartItemModal cartItemModal)
        {
            try
            {
                _logger.LogInformation("Attempting to update cart item for user ID: {UserId}", cartItemModal.UserId);
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating cart item for user ID: {UserId}", cartItemModal.UserId);
                throw;
            }
        }

        public async Task<List<CartItemModal>> GetCartItemsAsync(int userId)
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve cart items for user ID: {UserId}", userId);
                var cartItems = await _context.CartItemsDetailsDto
                                    .FromSqlRaw("SELECT * FROM getcartitems({0})", userId)
                .ToListAsync();

                return mapper.Map<List<CartItemModal>>(cartItems);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "Error retrieving cart items for user ID: {UserId}", userId);
                throw new Exception("Error retrieving cart items", ex);
            }
        }

        public async Task ClearCartAsync(int userId)
        {
            try
            {
                _logger.LogInformation("Attempting to clear cart for user ID: {UserId}", userId);
            var cartItems = _context.CartItemsDto
                .Where(c => c.UserId == userId);

            _context.CartItemsDto.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Cart cleared successfully for user ID: {UserId}", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cart for user ID: {UserId}", userId);
                throw;
            }
        }
    }
}