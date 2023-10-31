using BarBuddy.Data;
using BarBuddy.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBuddy.Repositories
{
    public class VenueSpendRepository : IVenueSpendRepository
    {
        private readonly IRepository _repository;
        private readonly IMongoCollection<VenueSpend> _venueSpendCollection;

        public VenueSpendRepository(IRepository repository)
        {
            _repository = repository;
            _venueSpendCollection = _repository.Database.GetCollection<VenueSpend>("VenueSpend");
        }

        public async Task<VenueSpend> AddVenueSpend(VenueSpend venueSpend)
        {
            await _venueSpendCollection.InsertOneAsync(venueSpend);

            return venueSpend;
        }

        public Task<List<VenueSpend>> GetAll()
        {
            return _venueSpendCollection.Find(v => true).ToListAsync();
        }
    }
}
