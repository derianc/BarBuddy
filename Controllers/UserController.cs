using Amazon.Runtime.Internal;
using BarBuddy.Data;
using BarBuddy.Services;
using BarBuddy.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BarBuddy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Required][EmailAddress] string username, [Required] string password)
        {
            var result = await _userService.LoginUser(username, password);

            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUser();

            return Ok();
        }

        [HttpGet("ListUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListUsers()
        {
            return Ok(await _userService.ListUsers());
        }

        [HttpPost("AddSpend")]
        [Authorize]
        public async Task<IActionResult> AddSpend(string venueId, double amount)
        {
            var userId = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                await _userService.AddSpend(new Guid(userId), new Guid(venueId), amount);
                return Ok();
            }
            return BadRequest();
        }
    }
}
