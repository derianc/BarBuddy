using BarBuddy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarBuddy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IVenueService _venueService;

        public AdminController(IUserService userService, 
                               IRoleService roleService,
                               IVenueService venueService)
        {
            _userService = userService;
            _roleService = roleService;
            _venueService = venueService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(string firstName, string lastName, string username, string email, string password)
        {
            await _userService.CreateUser(firstName, lastName, username, email, password);

            return Ok();
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string name)
        {
            await _roleService.CreateRole(name);

            return Ok();
        }
    }
}
