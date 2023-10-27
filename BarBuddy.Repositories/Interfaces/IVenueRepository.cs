using BarBuddy.Data;

namespace BarBuddy.Repositories.Interfaces
{
    public interface IVenueRepository
    {
        Task RegisterVenue(string venueName);
        Task<Venue> GetVenueById(string id);
        Task<List<Venue>> GetVenues();
    }
}
