using Amazon.Runtime.Internal;
using BarBuddy.Data;
using BarBuddy.Services;
using BarBuddy.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        [HttpPost("ListUsers")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListUsers()
        {
            return Ok(await _userService.ListUsers());
        }

        [HttpPost("CheckinToVenue")]
        public async Task<IActionResult> CheckinToVenue(string username, string venueId)
        {
            var loggedInUserName = User.Identity?.Name;
            if (!string.IsNullOrEmpty(loggedInUserName))
            {
                await _userService.CheckInToVenue(loggedInUserName, venueId);
                return Ok();
            }

            return BadRequest();
        }
    }
}
