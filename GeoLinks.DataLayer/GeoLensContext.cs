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
        public DbSet<FriendDto> Friends { get; set; }
        public DbSet<HobbiesDto> Hobbies { get; set; }
        public DbSet<InterestsDto> Interests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUser>()
                .HasNoKey();
            modelBuilder.Entity<FriendDetailDto>()
                .HasNoKey();
            modelBuilder.Entity<FriendDto>();
        }
    }
}
