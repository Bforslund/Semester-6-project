using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking_service.Events;
using Booking_service.Models;
using BookingService.Database;
using BookingService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Messaging;

namespace Booking_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {

        private readonly IMessagePublisher _messagePublisher;
        MockDB mockdb = new MockDB();

        public BookingController(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        [Route("fillDB")]
        public ActionResult fillMockDB()
        {
            mockdb.FillDB();

            return Ok();
        }


        [HttpGet]
        [Route("bookings")]
        public ActionResult GetAllBookings()
        {
           
            return Ok(MockDB.BookingList);
        }

        [HttpPost]
        [Route("availableRooms/{hotelId}")]
        public ActionResult GetAllAvailableRooms(int hotelId, AvailabilitySearch availabilitySearch)
        {
            List<Room> AvailableRooms = mockdb.GetAvailableRooms(hotelId, availabilitySearch.start, availabilitySearch.end);
            return Ok(AvailableRooms);
        }


        [HttpPost]
        [Route("availability/{hotelId}")]
        public ActionResult Checkavailability(int hotelId, AvailabilitySearch availabilitySearch)
        {

           int roomsFree = mockdb.AmountOfAvailableRooms(hotelId, availabilitySearch.start, availabilitySearch.end);
            return Ok(roomsFree);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            Booking newBooking = new Booking(booking.HotelId, booking.ContactInfo, booking.Start, booking.End, booking.RoomId);
            MockDB.BookingList.Add(newBooking);
            await _messagePublisher.PublishMessageAsync("NewBooking", newBooking);

            return Ok();
        }
    }
}
