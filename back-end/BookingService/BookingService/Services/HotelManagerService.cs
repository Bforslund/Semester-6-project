using Booking_service.Models;
using BookingService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Repository
{
    public class HotelManagerService
    {
        private readonly ApplicationDbContext _context;

        public HotelManagerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateHotelAsync(Hotel h)
        {
            _context.Hotels.Add(h);
            await _context.SaveChangesAsync();
        }
        public async Task AddRoomAsync(AddRoomEvent roomEvent)
        {
            var hotel = await _context.Hotels.Where(a => a.Id == roomEvent.HotelId).Include(h => h.Rooms).FirstOrDefaultAsync();
            Room r = new Room(roomEvent.Room.RoomType);
          //  r.Id = roomEvent.Room.Id;
            hotel.Rooms.Add(r);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
           return await _context.Hotels.Include(h => h.Rooms).ToListAsync();
        }
    }
}
