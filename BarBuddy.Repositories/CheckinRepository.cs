using BarBuddy.Data;
using BarBuddy.Repositories.Interfaces;
using MongoDB.Driver;

namespace BarBuddy.Repositories
{
    public class CheckinRepository : ICheckinRepository
    {
        private readonly IRepository _repository;
        private readonly IMongoCollection<VenueCheckin> _venueCheckinCollection;

        public CheckinRepository(IRepository repository)
        {
            _repository = repository;
            _venueCheckinCollection = _repository.Database.GetCollection<VenueCheckin>("VenueCheckin");
        }

        public async Task CheckinUser(VenueCheckin venueCheckin)
        {
            await _venueCheckinCollection.InsertOneAsync(venueCheckin);
        }

        public async Task<List<VenueCheckin>> GetUserCheckins(Guid userId)
        {
            var checkins = await _venueCheckinCollection.FindAsync(c => c.UserId == userId);

            return checkins.ToList();
        }
    }
}
