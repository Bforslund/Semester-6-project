using HotelQueryService.Database;
using HotelQueryService.Extensions;
using HotelQueryService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace HotelService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly ApplicationDbContext _context;
        public HotelController(ApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            string recordKey = "Hotels_" + DateTime.Now.ToString("yyyyMMdd_hhmm");

            var hotelsCache = await _cache.GetRecordAsync<Hotel[]>(recordKey);
            if (hotelsCache == null)
            {
                var hotels = await _context.Hotels.Include(h => h.Rooms).ToListAsync();
                await _cache.SetRecordAsync(recordKey, hotels);
                return Ok(hotels);

            }
            else
            {
                return Ok(hotelsCache);
            }


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
