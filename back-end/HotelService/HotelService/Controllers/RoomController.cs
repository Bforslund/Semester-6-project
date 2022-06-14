using HotelService.Authentication;
using HotelService.Database;
using HotelService.EventStore;
using HotelService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelService.Controllers
{
    [Route("[controller]/{hotelId}/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IMessagePublisher _messagePublisher;

        private readonly ApplicationDbContext _context;
        public RoomController(IMessagePublisher messagePublisher, ApplicationDbContext context)
        {
            _context = context;
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync(int hotelId)
        {
            var rooms = await _context.Hotels.Where(h => h.Id == hotelId).Include(h => h.Rooms).Select(h => h.Rooms).ToListAsync();
            return Ok(rooms);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddRoomAsync(int hotelId, RoomProjection room)
        {
            var hotel = await _context.Hotels.Where(a => a.Id == hotelId).FirstOrDefaultAsync();

            hotel.Rooms.Add(room);
            await _context.SaveChangesAsync();

            var rooms = new Room(room.RoomNumber, room.RoomType);
            _context.RoomEvents.Add(EventContainer.Create($"hotel:{hotelId}:room:{room.RoomNumber}", 1, rooms.PendingChanges[0]));
            await _context.SaveChangesAsync();

            await _messagePublisher.PublishMessageAsync("AddRoom", new AddRoomEvent(hotel.Id, room));

            return Ok(room);
        }
        [Authorize]
        [HttpPost]
        [Route("{roomNumber}/checkIn")]
        public async Task<ActionResult> CheckinAsync(int hotelId, int roomNumber)
        {
            var savedEvents = await _context.RoomEvents.Where(e => e.Aggregate == $"hotel:{hotelId}:room:{roomNumber}").ToListAsync();
            if (savedEvents.Count == 0)
            {
                throw new ArgumentException($"Room {roomNumber} does not exist at hotel {hotelId}");
            }

            var room = new Room(savedEvents.Select(e => e.Deserialize()));

            room.RoomIsOccupied();

            var expectedVersion = room.Version;

            foreach (var @event in room.PendingChanges)
            {
                _context.RoomEvents.Add(EventContainer.Create($"hotel:{hotelId}:room:{roomNumber}", ++expectedVersion, @event));
            }

            await _context.SaveChangesAsync();

            savedEvents = await _context.RoomEvents.Where(e => e.Aggregate == $"hotel:{hotelId}:room:{roomNumber}").ToListAsync();
            room = new Room(savedEvents.Select(e => e.Deserialize()));

            var roomProjection = await _context.RoomProjections.FirstAsync(r => r.RoomNumber == roomNumber);
            roomProjection.Available = room.Available;
            await _context.SaveChangesAsync();

            return Ok(roomProjection);
        }
        [Authorize]
        [HttpPost]
        [Route("{roomNumber}/checkOut")]
        public async Task<ActionResult> CheckOutAsync(int hotelId, int roomNumber)
        {
            var savedEvents = await _context.RoomEvents.Where(e => e.Aggregate == $"hotel:{hotelId}:room:{roomNumber}").ToListAsync();
            if (savedEvents.Count == 0)
            {
                throw new ArgumentException($"Room {roomNumber} does not exist at hotel {hotelId}");
            }

            var room = new Room(savedEvents.Select(e => e.Deserialize()));

            room.RoomIsAvailable();

            var expectedVersion = room.Version;

            foreach (var @event in room.PendingChanges)
            {
                _context.RoomEvents.Add(EventContainer.Create($"hotel:{hotelId}:room:{roomNumber}", ++expectedVersion, @event));
            }

            await _context.SaveChangesAsync();

            savedEvents = await _context.RoomEvents.Where(e => e.Aggregate == $"hotel:{hotelId}:room:{roomNumber}").ToListAsync();
            room = new Room(savedEvents.Select(e => e.Deserialize()));

            var roomProjection = await _context.RoomProjections.FirstAsync(r => r.RoomNumber == roomNumber);
            roomProjection.Available = room.Available;
            await _context.SaveChangesAsync();

            return Ok(roomProjection);
        }
    }
}
