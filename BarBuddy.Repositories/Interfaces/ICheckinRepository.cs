using BarBuddy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBuddy.Repositories.Interfaces
{
    public interface ICheckinRepository
    {
        Task CheckinUser(VenueCheckin venueCheckin);
        Task<List<VenueCheckin>> GetUserCheckins(Guid userId);
    }
}
