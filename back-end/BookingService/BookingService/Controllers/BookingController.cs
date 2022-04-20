using System.Threading.Tasks;
using Booking_service.Models;
using BookingService.Models;
using BookingService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;

namespace Booking_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMessagePublisher _messagePublisher;
        AvalabilityService avalabilityService;
        public BookingController(IMessagePublisher messagePublisher, ApplicationDbContext context)
        {
            avalabilityService = new AvalabilityService(context);
            _messagePublisher = messagePublisher;
            
        }

        [HttpGet]
        [Route("bookings")]
        public async Task<ActionResult> GetAllBookingsAsync()
        {
            var bookings = await avalabilityService.GetBookingsAsync();
            if (bookings == null) return NotFound();
            return Ok(bookings);
        }

        [HttpPost]
        [Route("availableRooms/{hotelId}")]
        public async Task<ActionResult> GetAllAvailableRoomsAsync(int hotelId, AvailabilitySearch availabilitySearch)
        {
            var AvailableRooms = await avalabilityService.GetAvailableRoomsAsync(hotelId, availabilitySearch.start, availabilitySearch.end);
            return Ok(AvailableRooms);
        }


        [HttpPost]
        [Route("availability/{hotelId}")]
        public async Task<ActionResult> CheckavailabilityAsync(int hotelId, AvailabilitySearch availabilitySearch)
        {
            int roomsFree = await avalabilityService.AmountOfAvailableRoomsAsync(hotelId, availabilitySearch.start, availabilitySearch.end);
            return Ok(roomsFree);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            Booking newBooking = await avalabilityService.CreateBookingAsync(booking);
            await _messagePublisher.PublishMessageAsync("NewBooking", newBooking);
            return Ok(newBooking.Id);
        }
    }
}
