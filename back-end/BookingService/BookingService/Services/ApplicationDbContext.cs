using Booking_service.Models;
using HotelService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
          // Database.EnsureDeleted();
          Database.EnsureCreated();
        }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
    }
}
