using BarBuddy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBuddy.Repositories.Interfaces
{
    public interface IVenueRepository
    {
        Task RegisterVenue(string venueName);
        Task<Venue> GetVenueById(string id);
    }
}
