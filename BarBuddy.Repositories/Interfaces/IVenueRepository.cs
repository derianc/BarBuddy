using BarBuddy.Data;

namespace BarBuddy.Repositories.Interfaces
{
    public interface IVenueRepository
    {
        Task RegisterVenue(string venueName);
        Task<Venue> GetVenueById(Guid id);
        Task<List<Venue>> GetVenues();
        Task UpdateVenue(Venue venue);
    }
}
