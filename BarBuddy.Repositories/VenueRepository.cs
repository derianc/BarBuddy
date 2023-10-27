using BarBuddy.Data;
using BarBuddy.Repositories.Interfaces;
using MongoDB.Driver;

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

        public Task<Venue> GetVenueById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterVenue(string venueName)
        {
            var venue = new Venue
            {
                VenueName = venueName,
            };

            await _venueCollection.InsertOneAsync(venue);
        }
    }
}
