using BarBuddy.Data;
using BarBuddy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBuddy.Services
{
    public class VenueService : IVenueService
    {
        public Task<Venue> GetVenueById(string id)
        {
            throw new NotImplementedException();
        }

        public Task RegisterVenue(string venueName)
        {
            throw new NotImplementedException();
        }
    }
}
