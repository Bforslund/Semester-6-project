
using HotelQueryService.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelQueryService.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<RoomProjection> RoomProjections { get; set; }

    }
}
