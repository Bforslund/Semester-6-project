using HotelService.Controllers;
using HotelService.Database;
using HotelService.Models;
using Shared.Messaging;
using System.Threading.Tasks;

namespace PlayerService.MessageHandlers
{
    public class NewBookingMessageHandler : IMessageHandler<Booking>
    {

        database database = new database();
        private readonly IMessagePublisher _messagePublisher;

        public NewBookingMessageHandler( IMessagePublisher messagePublisher)
        {
           
            _messagePublisher = messagePublisher;
        }
        public Task HandleMessageAsync(string messageType, Booking obj)
        {
           
            if (obj == null)
            {
                return Task.CompletedTask;
            }
            Hotel newBooking = obj.Hotel;

            newBooking.rooms -= 1;
            database.AddHotel(newBooking);
            return Task.CompletedTask;
        }
    }
}
