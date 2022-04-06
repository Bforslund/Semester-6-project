using HotelService.Database;
using HotelService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelService.Controllers
{
    [ApiController]
    [Route("[roomController]")]
    public class RoomController : Controller
    {
        database database = new database();


        private readonly ILogger<RoomController> _logger;

        public RoomController(ILogger<RoomController> logger)
        {
            _logger = logger;
        }

    }
}
