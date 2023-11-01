using BarBuddy.Data;
using BarBuddy.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.GeoJsonObjectModel;

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
        [Authorize]
        public async Task<IActionResult> RegisterVenue(string venueName)
        {
            await _venueService.RegisterVenue(venueName);

            return Ok();
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _venueService.GetVenues();

            return Ok(result);
        }

        [HttpPut("UpdateVenue")]
        [Authorize]
        public async Task<IActionResult> UpdateVenue([FromQuery] Guid id, [FromQuery] string name, [FromQuery] double lat, [FromQuery] double lng)
        {
            var updateVenue = new Venue
            {
                Id = id,
                VenueName = name,
                Location = GeoJson.Point(GeoJson.Position(lat, lng))
            };

            await _venueService.UpdateVenue(updateVenue);

            return Ok();
        }
    }
}
