using HotelService.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Database
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
        public DbSet<ReservedRoom> ReservedRooms { get; set; }

        public DbSet<HotelAdmin> Admins { get; set; }

    }
}
