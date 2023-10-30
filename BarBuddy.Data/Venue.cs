using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace BarBuddy.Data
{
    public class Venue
    {
        [BsonId] 
        public Guid Id { get; set; }
        public string? VenueName { get; set; }
        public GeoJsonPoint<GeoJson2DCoordinates>? Location { get; set; } 
    }
}
