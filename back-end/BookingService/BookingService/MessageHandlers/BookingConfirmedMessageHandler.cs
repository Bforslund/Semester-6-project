
using Shared.Messaging;
using System;
using System.Threading.Tasks;

namespace PlayerService.MessageHandlers
{
    public class BookingConfirmedMessageHandler : IMessageHandler<string>
    {

       
        private readonly IMessagePublisher _messagePublisher;

        public BookingConfirmedMessageHandler( IMessagePublisher messagePublisher)
        {
           
            _messagePublisher = messagePublisher;
        }
        public Task HandleMessageAsync(string messageType, string obj)
        {
           
            if (obj == null)
            {
                return Task.CompletedTask;
            }

            //check if room is available
            // add to reserved rooms table

            //send a room reserved event
            //_messagePublisher.PublishMessageAsync("BookingConfirmed", "confirmed");
            Console.WriteLine("Booking completed: "+ obj);
            return Task.CompletedTask;
        }
    }
}
