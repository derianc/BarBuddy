using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBuddy.Data
{
    public class Venue
    {
        [BsonId] 
        public Guid Id { get; set; }
        public string? VenueName { get; set; }
    }
}
