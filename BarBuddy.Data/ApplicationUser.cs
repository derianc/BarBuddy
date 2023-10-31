using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace BarBuddy.Data
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? QrCode { get; set; }
        public DateTime? DOB { get; set; }
        public List<Guid>? Checkins { get; set; }
    }
}
