
using Booking_service.Models;
using BookingService.Repository;
using Shared.Messaging;
using System.Threading.Tasks;

namespace PlayerService.MessageHandlers
{
    public class BookingConfirmedMessageHandler : IMessageHandler<Booking>
    {

       private readonly AvalabilityService _avalabilityService;
        public BookingConfirmedMessageHandler(AvalabilityService avalabilityService)
        {
            _avalabilityService = avalabilityService;
        }
        public async Task HandleMessageAsync(string messageType, Booking obj)
        {
            var booking = await _avalabilityService.GetBookingByIdAsync(obj.Id);
            if (booking == null)
            {
                return;
            }
            if(messageType == "BookingConfirmed")
            {
               await _avalabilityService.ConfirmBookingAsync(booking);
            }
           
        }

       
    }
}
