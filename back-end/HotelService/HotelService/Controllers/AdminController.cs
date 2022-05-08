using HotelService.Database;
using HotelService.Models;
using HotelService.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {

        private readonly AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        [Route("newHotelAdmin")]
        public async Task<ActionResult> AddHotelAdminAsync(HotelAdmin h)
        {
            await _adminService.CreateAdminAsync(h);
            return Ok(h.Id);
        }

        [HttpPost("login")]
        public IActionResult Authenticate(AuthenticateRequest login)
        {
            var authenticatedUser = _adminService.Authenticate(login);

            if (authenticatedUser == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(authenticatedUser);
        }
    }
}
