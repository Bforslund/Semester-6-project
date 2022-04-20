using Booking_service.Models;
using HotelService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Repository
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
        public DbSet<Booking> Bookings { get; set; }

    }
}
