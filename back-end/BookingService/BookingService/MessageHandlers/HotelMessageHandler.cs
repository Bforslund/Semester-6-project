using Booking_service.Models;
using BookingService.Repository;
using Shared.Messaging;
using System.Threading.Tasks;

namespace BookingService.MessageHandlers
{
    public class HotelMessageHandler: IMessageHandler<Hotel>
    {
   
        private readonly HotelManagerService _hotelService;

        public HotelMessageHandler(ApplicationDbContext dbContext)
        {
            _hotelService = new HotelManagerService(dbContext);
        }
        public async Task HandleMessageAsync(string messageType, Hotel obj)
        {

            if (obj == null)
            {
                return;
            }
            if (messageType == "NewHotel")
            {
               await _hotelService.CreateHotelAsync(obj);
            }

        }
    }
}
