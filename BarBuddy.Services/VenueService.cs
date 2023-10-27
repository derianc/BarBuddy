using BarBuddy.Data;
using BarBuddy.Repositories.Interfaces;
using BarBuddy.Services.Interfaces;
using MongoDB.Driver.GeoJsonObjectModel;

namespace BarBuddy.Services
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;
        public VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public Task<Venue> GetVenueById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Venue> GetVenueById(Guid id)
        {
            return await _venueRepository.GetVenueById(id);
        }

        public async Task<List<Venue>> GetVenues()
        {
            return await _venueRepository.GetVenues();
        }

        public async Task RegisterVenue(string venueName)
        {
            await _venueRepository.RegisterVenue(venueName);
        }

        public async Task UpdateVenue(Venue venue)
        {
            await _venueRepository.UpdateVenue(venue);
        }
    }
}
