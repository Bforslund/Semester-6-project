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
        public async Task<IActionResult> CompleteBooking(Booking newBooking)
        {

            bookingList.Add(newBooking);
            await _messagePublisher.PublishMessageAsync("BookingCompleted", newBooking);

            return Ok();
        }
    }
}
