using GeoLinks.Entities.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;

namespace GeoLinks.DataLayer
{
    public class GeoLensContext : DbContext
    {
        public GeoLensContext(DbContextOptions<GeoLensContext> options)
            : base(options)
        {

        }
        public DbSet<ProfileDto> ProfileDtos { get; set; }
        public DbSet<Password> PasswordDtos { get; set; }
        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<StoreDto> stores { get; set; }
        public DbSet<StoreItemDetailsDto> StoreItemDetailsDtos { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
        public DbSet<CartItemDto> CartItemsDto { get; set; }
        // public DbSet<CartItemDetailsDto> CartItemsDetailsDto { get; set; }
        public DbSet<UsersDto> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUser>()
                .HasNoKey();

            modelBuilder.Entity<StoreDto>()
                .HasKey(s => s.StoreId);
            modelBuilder.Entity<StoreDto>()
                .Property(s => s.StoreId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StoreItemDetailsDto>()
                .HasKey(s => s.ItemId);
            modelBuilder.Entity<StoreItemDetailsDto>()
                .Property(s => s.ItemId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StoreItemDetailsDto>()
                .HasOne(sid => sid.Store)
                .WithMany(s => s.StoreItemDetails)
                .HasForeignKey(sid => sid.StoreId)
                .HasPrincipalKey(s => s.StoreId);

            // Add OrderDto configuration
            modelBuilder.Entity<OrderDto>()
                .HasKey(o => o.OrderId);
            modelBuilder.Entity<OrderDto>()
                .Property(o => o.OrderId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<CartItemDto>()
                .HasKey(c => c.CartItemId);
            modelBuilder.Entity<CartItemDto>()
                .Property(c => c.CartItemId)
                .ValueGeneratedOnAdd();
                
            modelBuilder.Entity<UsersDto>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<UsersDto>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
