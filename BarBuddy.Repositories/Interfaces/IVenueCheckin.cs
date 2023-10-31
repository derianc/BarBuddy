namespace BarBuddy.Repositories.Interfaces
{
    public interface IVenueCheckin
    {
        Task CreateUserVenueCheckin(string venueId);
        Task<IVenueCheckin> GetCheckinForUserByDate(string venueId, DateTime? date);
    }
}
