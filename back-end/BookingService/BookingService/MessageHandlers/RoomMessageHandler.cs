using Booking_service.Models;
using BookingService.Models;
using BookingService.Repository;
using Shared.Messaging;
using System.Threading.Tasks;

namespace BookingService.MessageHandlers
{
    public class RoomMessageHandler : IMessageHandler<AddRoomEvent>
    {
        private readonly HotelManagerService _hotelService;

        public RoomMessageHandler(ApplicationDbContext dbContext)
        {
            _hotelService = new HotelManagerService(dbContext);
        }
        public async Task HandleMessageAsync(string messageType, AddRoomEvent obj)
        {

            if (obj == null)
            {
                return;
            }

            if (messageType == "AddRoom")
            {
                await _hotelService.AddRoomAsync(obj);
            }

        }
    }
}
