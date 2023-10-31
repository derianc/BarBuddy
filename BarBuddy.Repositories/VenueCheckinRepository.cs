using BarBuddy.Data;
using BarBuddy.Repositories.Interfaces;
using MongoDB.Driver;

namespace BarBuddy.Repositories
{
    public class VenueCheckinRepository : IVenueCheckinRepository
    {
        private readonly IRepository _repository;
        private readonly IMongoCollection<VenueCheckin> _venueCheckinCollection;

        public VenueCheckinRepository(IRepository repository)
        {
            _repository = repository;
            _venueCheckinCollection = _repository.Database.GetCollection<VenueCheckin>("VenueCheckin");
        }

        public async Task AddSpend(VenueCheckin venueCheckin)
        {
            var result = await _venueCheckinCollection.ReplaceOneAsync(vc => vc.Id == venueCheckin.Id, venueCheckin);
        }

        public async Task<VenueCheckin> CheckinUser(Guid userId, Guid venueId)
        {
            var venueCheckin = new VenueCheckin
            {
                UserId = userId,
                VenueId = venueId,
                CreatedOn = DateTime.Now,
            };

            await _venueCheckinCollection.InsertOneAsync(venueCheckin);

            return venueCheckin;
        }

        public async Task<VenueCheckin> GetUserCheckinByVenueAndDate(Guid userId, Guid venueId, DateTime date)
        {
            var userCheckin = await _venueCheckinCollection.FindAsync(vc => vc.UserId == userId && 
                                                                      vc.CreatedOn.Date == date.Date &&
                                                                      vc.VenueId == venueId);

            return userCheckin.FirstOrDefault();
        }

        public async Task<List<Guid>> GetUserCheckins(Guid loggedInUserId)
        {
            var userCheckin = (await _venueCheckinCollection.FindAsync(vc => vc.UserId == loggedInUserId)).ToList().Select(c => c.Id).ToList();

            return userCheckin;
        }
    }
}
