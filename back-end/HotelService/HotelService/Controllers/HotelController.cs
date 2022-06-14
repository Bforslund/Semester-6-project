using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelService.Authentication;
using HotelService.Database;
using HotelService.EventStore;
using HotelService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;

namespace HotelService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IMessagePublisher _messagePublisher;

        private readonly ApplicationDbContext _context;
        public HotelController(IMessagePublisher messagePublisher, ApplicationDbContext context)
        {
            _context = context;
            _messagePublisher = messagePublisher;
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddHotelAsync(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            await _messagePublisher.PublishMessageAsync("NewHotel", hotel);
            return Ok(hotel.Id);
        }
    }
}
