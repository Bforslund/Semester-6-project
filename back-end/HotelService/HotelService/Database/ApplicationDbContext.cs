using HotelService.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<ReservedRoom> ReservedRooms { get; set; }

    }
}
