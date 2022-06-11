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

                //ReservedRoom newReservedRoom = new ReservedRoom(obj.RoomId, obj.Start, obj.End);
                //_context.ReservedRooms.Add(newReservedRoom);
                
                await _messagePublisher.PublishMessageAsync("BookingConfirmed", obj);
            }
            await _context.SaveChangesAsync();

        }

        public bool CheckAvailability()
        {
            return true;
        }
    }
}
