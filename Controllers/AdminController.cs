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

        public AdminController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(string name, string email, string password)
        {
            await _userService.CreateUser(name, email, password);

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
