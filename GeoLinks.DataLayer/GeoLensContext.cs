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
        public DbSet<StoreDto> Store { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUser>()
                .HasNoKey();
        }
    }
}
