using System.Collections.Generic;
using GeoLinks.Entities.DbEntities;
using GeoLinks.DataLayer.DalInterface;
using System.Threading.Tasks;
using System;
using System.Linq;
using AutoMapper;

namespace GeoLinks.DataLayer.DalImplementation
{
    public class CartRepository : ICartRepository
    {
        // Example properties and methods for the CartRepository class
        private readonly GeoLensContext _context;
        private MapperConfiguration mapperconfig;
        private IMapper mapper;

        public CartRepository(GeoLensContext context)
        {
            _context = context;
            mapperconfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CartItemDto, CartItemModal>();
                cfg.CreateMap<CartItemModal, CartItemDto>();
            });
            mapper = mapperconfig.CreateMapper();
        }

        // Example method implementation from ICartRepository
        public async Task AddToCartAsync(CartItemModal cartItemModal)
        {
            // Check if the item already exists in the cart
            var cartItemDto = mapper.Map<CartItemDto>(cartItemModal);
            _context.CartItemsDto.Add(cartItemDto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(CartItemModal cartItemModal)
        {
            
            throw new NotImplementedException("Add logic to add the product to the user's cart in the database.");
            // Add logic to remove the product from the user's cart in the database
        }

        public async Task UpdateCartItemAsync(CartItemModal cartItemModal)
        {
            throw new NotImplementedException("Add logic to add the product to the user's cart in the database.");
            // Add logic to update the quantity of a product in the user's cart in the database
        }

        public async Task<IEnumerable<CartItemModal>> GetCartItemsAsync(int userId)
        {
            // Add logic to retrieve all items in the user's cart from the database
            return new List<CartItemModal>();
        }

        public async Task ClearCartAsync(int userId)
        {
            throw new NotImplementedException("Add logic to add the product to the user's cart in the database.");
            // Add logic to clear all items from the user's cart in the database
        }
    }
}