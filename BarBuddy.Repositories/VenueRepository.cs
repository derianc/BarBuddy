using BarBuddy.Data;
using BarBuddy.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace BarBuddy.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly IRepository _repository;
        private readonly IMongoCollection<Venue> _venueCollection;

        public VenueRepository(IRepository repository)
        {
            _repository = repository;
            _venueCollection = _repository.Database.GetCollection<Venue>("Venue");
        }

        public async Task<Venue> GetVenueById(Guid id)
        {
            return await _venueCollection.Find(v => v.Id == id).FirstAsync();
        }

        public Task<List<Venue>> GetVenues()
        {
            return _venueCollection.Find(v => true).ToListAsync();
        }

        public async Task RegisterVenue(string venueName)
        {
            var venue = new Venue
            {
                VenueName = venueName,
                Location = GeoJson.Point(GeoJson.Position(-74.005, 40.7358879))
            };

            await _venueCollection.InsertOneAsync(venue);
        }

        public async Task UpdateVenue(Venue venue)
        {
            var result = await _venueCollection.ReplaceOneAsync(v => v.Id == venue.Id, venue);
        }
    }
}
