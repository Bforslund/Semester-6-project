using HotelQueryService.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var hotels = await _context.Hotels.Include(h => h.Rooms).ToListAsync();
            return Ok(hotels);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetHotelByIdAsync(int id)
        {
            var hotel = await _context.Hotels.Where(a => a.Id == id).Include(h => h.Rooms).FirstOrDefaultAsync();
            if (hotel == null) return NotFound();
            return Ok(hotel);
        }

        [HttpGet]
        [Route("rooms/{hotelId}")]
        public async Task<ActionResult> GetAllRoomsAsync(int hotelId)
        {
            var rooms = await _context.Hotels.Where(h => h.Id == hotelId).Include(h => h.Rooms).Select(h => h.Rooms).ToListAsync();
            return Ok(rooms);
        }
    }
}
