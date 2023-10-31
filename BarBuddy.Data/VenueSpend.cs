﻿using MongoDB.Bson.Serialization.Attributes;

namespace BarBuddy.Data
{
    public class VenueSpend
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VenueCheckinId { get; set; }
        public double? Amount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
