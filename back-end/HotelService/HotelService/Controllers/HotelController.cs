using System.Linq;
using System.Threading.Tasks;
using HotelService.Database;
using HotelService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
 
       
        private ApplicationDbContext _context;
        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("hotels")]
        public async Task<ActionResult> GetAllHotelsAsync()
        {
            var hotels = await _context.Hotels.Include(h => h.Rooms).ToListAsync();
            if (hotels == null) return NotFound();
            return Ok(hotels);
        }
        [HttpGet]
        [Route("getAllReservedRooms")]
        public async Task<ActionResult> GetAllReservedRoomsAsync()
        {
            var reservedRooms = await _context.ReservedRooms.ToListAsync();
            if (reservedRooms == null) return NotFound();
            return Ok(reservedRooms);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetHotelByIdAsync(int id)
        {
            var hotel = await _context.Hotels.Where(a => a.Id == id).Include(h => h.Rooms).FirstOrDefaultAsync();
            if (hotel == null) return NotFound();
            return Ok(hotel);
        }

        [HttpPost]
        [Route("newHotel")]
        public async Task<ActionResult> AddHotelAsync(Hotel h)
        {
            _context.Hotels.Add(h);
            await _context.SaveChangesAsync();
            return Ok(h.Id);
        }

        [HttpPost]
        [Route("addRooms/{hotelId}")]
        public async Task<ActionResult> AddRoomsAsync(int hotelId, Room room)
        {
            var hotel = await _context.Hotels.Where(a => a.Id == hotelId).FirstOrDefaultAsync();
            hotel.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("rooms/{hotelId}")]
        public async Task<ActionResult> GetAllRoomsOfHotelAsync(int hotelId)
        {
            var hotel = await _context.Hotels.Where(a => a.Id == hotelId).Include(h => h.Rooms).FirstOrDefaultAsync();
            return Ok(hotel.Rooms);
        }
     

    }
}
