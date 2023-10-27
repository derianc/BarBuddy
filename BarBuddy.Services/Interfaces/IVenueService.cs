using BarBuddy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBuddy.Services.Interfaces
{
    public interface IVenueService
    {
        Task RegisterVenue(string venueName);
        Task<Venue> GetVenueById(string id);
        Task<List<Venue>> GetVenues();
    }
}
