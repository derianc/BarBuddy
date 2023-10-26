using BarBuddy.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BarBuddy.Repositories
{
    public class Repository : IRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;
        private readonly MongoClientSettings settings;

        public IMongoDatabase Database { get => _database; }

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
            settings = MongoClientSettings.FromConnectionString(_configuration.GetConnectionString("MongoDb"));
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            _database = this.ConnectToDb();
        }

        public IMongoDatabase ConnectToDb() => new MongoClient(settings).GetDatabase(_configuration.GetConnectionString("DbName"));
    }
}
