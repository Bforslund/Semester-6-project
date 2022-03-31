
using Booking_service.Models;
using BookingService.Database;
using Shared.Messaging;
using System;
using System.Threading.Tasks;

namespace PlayerService.MessageHandlers
{
    public class BookingConfirmedMessageHandler : IMessageHandler<Booking>
    {

        MockDB mockDB = new MockDB();
        private readonly IMessagePublisher _messagePublisher;

        public BookingConfirmedMessageHandler( IMessagePublisher messagePublisher)
        {
           
            _messagePublisher = messagePublisher;
        }
        public Task HandleMessageAsync(string messageType, Booking obj)
        {
           
            if (obj == null)
            {
                return Task.CompletedTask;
            }
            if(messageType == "BookingConfirmed")
            {
                mockDB.ConfirmBooking(obj);
            }
            return Task.CompletedTask;
        }
    }
}
