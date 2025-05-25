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
        public DbSet<StoreItemDetailsDto> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUser>()
                .HasNoKey();
            modelBuilder.Entity<StoreDto>()
                .HasKey(s => s.StoreId);
            modelBuilder.Entity<StoreDto>()
                .Property(s => s.StoreId)
                .ValueGeneratedOnAdd();

            // Ensure StoreItemDetailsDto has a primary key
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
        }
    }
}
