using BarBuddy.Data;

namespace BarBuddy.Repositories.Interfaces
{
    public interface IVenueSpendRepository
    {
        Task<List<VenueSpend>> GetAll();
        Task<VenueSpend> AddVenueSpend(VenueSpend venueSpend);
    }
}
