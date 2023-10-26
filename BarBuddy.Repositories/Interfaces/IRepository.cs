using MongoDB.Driver;

namespace BarBuddy.Repositories.Interfaces
{
    public interface IRepository
    {
        IMongoDatabase Database { get; }
        IMongoDatabase ConnectToDb();
    }
}
