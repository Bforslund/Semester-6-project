using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelService.Database;
using HotelService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        database database = new database();
       

        private readonly ILogger<HotelController> _logger;
        
        public HotelController(ILogger<HotelController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("fillDB")]
        public ActionResult fillMockDB()
        {
            database.fillDB();

            return NoContent();
        }


        [HttpGet]
        [Route("hotels")]
        public ActionResult GetAllHotels()
        {

            return Ok(database.HotelList);
        }
        [HttpGet]
        [Route("getAllReservedRooms")]
        public ActionResult GetAllReservedRooms()
        {

            return Ok(database.ReservedRoomsList);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetHotelById(int id)
        {
           Hotel h = database.GetHotelById(id);
            return Ok(h);
        }

        [HttpPost]
        [Route("newHotel")]
        public ActionResult AddHotel(Hotel h)
        {
            Hotel newHotel = new Hotel(h.Title, h.Info);
            database.AddHotel(newHotel);
            return Ok(newHotel);
        }

        [HttpPost]
        [Route("addRooms/{hotelId}")]
        public ActionResult AddRooms(int hotelId, Room room)
        {
            Hotel hotel = database.GetHotelById(hotelId);
            database.AddRoom(hotel, room);
            return Ok();
        }

        [HttpGet]
        [Route("rooms/{hotelId}")]
        public ActionResult GetAllRoomsOfHotel(int hotelId)
        {
            Hotel hotel = database.GetHotelById(hotelId);
            return Ok(database.GetRoomsOfHotel(hotel));
        }
    }
}
