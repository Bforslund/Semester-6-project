using HotelService.Controllers;
using HotelService.Database;
using HotelService.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerService.MessageHandlers
{
    public class NewBookingMessageHandler : IMessageHandler<Booking>
    {

        private readonly ApplicationDbContext _context;
        private readonly IMessagePublisher _messagePublisher;

        public NewBookingMessageHandler(ApplicationDbContext dbContext, IMessagePublisher messagePublisher)
        {
            _context = dbContext;
            _messagePublisher = messagePublisher;
        }
        public async Task HandleMessageAsync(string messageType, Booking obj)
        {
           
            if (obj == null)
            {
                return;
            }

          
            if (CheckAvailability())
            {
                var hotel = await _context.Hotels.Where(a => a.Id == obj.HotelId).Include(h => h.Rooms).FirstOrDefaultAsync();

                Room room = GetRoomById(hotel, obj.RoomId);
                ReservedRoom newReservedRoom = new ReservedRoom(room, obj.Start, obj.End);
                _context.ReservedRooms.Add(newReservedRoom);
                
                await _messagePublisher.PublishMessageAsync("BookingConfirmed", obj);
            }

            //send a room reserved event
            await _context.SaveChangesAsync();

        }

        public Room GetRoomById(Hotel h, int id)
        {
            foreach (var room in h.Rooms)
            {
                if (id == room.Id)
                {
                    return room;
                }
            }
            return null;
        }

        public bool CheckAvailability()
        {
            return true;
        }
    }
}
