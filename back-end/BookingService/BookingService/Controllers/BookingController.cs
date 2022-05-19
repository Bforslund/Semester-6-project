using System.Threading.Tasks;
using Booking_service.Models;
using BookingService.Models;
using BookingService.Repository;
using Microsoft.AspNetCore.DataProtection;
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
        private AvalabilityService _avalabilityService;
        private HotelManagerService _managerService;

        public BookingController(IMessagePublisher messagePublisher, AvalabilityService avalabilityService, HotelManagerService hotelManagerService)
        {
            _avalabilityService = avalabilityService;
            _managerService = hotelManagerService;
            _messagePublisher = messagePublisher;

        }

        [HttpGet]
        [Route("bookings")]
        public async Task<ActionResult> GetAllBookingsAsync()
        {
            var bookings = await _avalabilityService.GetBookingsAsync();
            if (bookings == null) return NotFound();
            return Ok(bookings);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetBookingById(int id)
        {
            Booking b = await _avalabilityService.GetBookingByIdAsync(id);
           
            return Ok(b);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBooking(Booking b)
        {
            Booking booking = await _avalabilityService.UpdateBookingAsync(b);
            return Ok(booking);
        }


        [HttpPost]
        [Route("availableRooms/{hotelId}")]
        public async Task<ActionResult> GetAllAvailableRoomsAsync(int hotelId, AvailabilitySearch availabilitySearch)
        {
            var AvailableRooms = await _avalabilityService.GetAvailableRoomsAsync(hotelId, availabilitySearch.start, availabilitySearch.end);
            return Ok(AvailableRooms);
        }


        [HttpPost]
        [Route("availability/{hotelId}")]
        public async Task<ActionResult> CheckavailabilityAsync(int hotelId, AvailabilitySearch availabilitySearch)
        {
            int roomsFree = await _avalabilityService.AmountOfAvailableRoomsAsync(hotelId, availabilitySearch.start, availabilitySearch.end);
            return Ok(roomsFree);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            Booking newBooking = await _avalabilityService.CreateBookingAsync(booking);
            await _messagePublisher.PublishMessageAsync("NewBooking", newBooking);
            return Ok(newBooking.Id);
        }

        [HttpGet]
        [Route("hotels")]
        public async Task<ActionResult> GetAllHotelsAsync()
        {
            var hotels = await _managerService.GetAllHotelsAsync();
            if (hotels == null) return NotFound();
            return Ok(hotels);
        }


        [HttpGet]
        [Route("hello")]
        public ActionResult GetHello()
        {
            
            return Ok("Hello");
        }
    }
}
