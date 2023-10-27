using BarBuddy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarBuddy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        [HttpPost("RegisterVenue")]
        public async Task<IActionResult> RegisterVenue(string venueName)
        {
            await _venueService.RegisterVenue(venueName);

            return Ok();
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _venueService.GetVenues();

            return Ok(result);
        }
    }
}
