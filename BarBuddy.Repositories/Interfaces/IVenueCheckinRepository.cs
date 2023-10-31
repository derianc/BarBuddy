using BarBuddy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBuddy.Repositories.Interfaces
{
    public interface IVenueCheckinRepository
    {
        Task<VenueCheckin> CheckinUser(Guid userId, Guid venueId);
        Task<List<Guid>> GetUserCheckins(Guid loggedInUserId);
        Task<VenueCheckin> GetUserCheckinByVenueAndDate(Guid userId, Guid venueId, DateTime date);
        Task AddSpend(VenueCheckin venueCheckin);
    }
}
