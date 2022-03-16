using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking_service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Booking_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        [HttpGet]
        [Route("hotels")]
        public ActionResult GetAllHotels()
        {
            List<Hotel> hotelList = new List<Hotel>();
            hotelList.Add(new Hotel(1, "Amsterdam Hotel", "tbd"));
            hotelList.Add(new Hotel(2, "Den haag hotel", "tbd"));
            hotelList.Add(new Hotel(3, "Rotterdam hotel", "tbd"));
            return Ok(hotelList);
        }
    }
}
