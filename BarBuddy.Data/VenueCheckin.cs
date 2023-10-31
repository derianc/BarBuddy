﻿using MongoDB.Bson.Serialization.Attributes;

namespace BarBuddy.Data
{
    public class VenueCheckin
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VenueId { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<Guid> VenueSpendIds { get; set; } = new List<Guid>();
    }
}
