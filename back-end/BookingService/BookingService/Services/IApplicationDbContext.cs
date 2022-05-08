using Booking_service.Models;
using HotelService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Repository
{
    public interface IApplicationDbContext
    {
        DbSet<Booking> Bookings { get; set; }
        DbSet<Hotel> Hotels { get; set; }
    }
}