using MongoDB.Bson.Serialization.Attributes;

namespace BarBuddy.Data
{
    public class VenueCheckin
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VenueId { get; set; }
        public double? AmountSpent { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
