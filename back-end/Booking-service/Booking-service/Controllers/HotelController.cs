using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking_service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Messaging;

namespace Booking_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {

        private readonly IMessagePublisher _messagePublisher;
        List<Booking> bookingList = new List<Booking>();

        public HotelController(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

       
        [HttpPost]
        public async Task<IActionResult> CompleteBooking(int bookingId)
        {
            Hotel haagHotel = new Hotel(1, "Den Haag Hotel", "fhsdf");
            Booking booking = new Booking(1, haagHotel, "booked 1 room");
            bookingList.Add(booking);
            await _messagePublisher.PublishMessageAsync("BookingCompleted", booking);

            return Ok();
        }
    }
}
