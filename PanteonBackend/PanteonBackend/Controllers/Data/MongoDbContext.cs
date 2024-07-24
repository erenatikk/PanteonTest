using MongoDB.Driver;
using Panteon_Backend.Models;

namespace Panteon_Backend.Controllers.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<ConfigurationItem> ConfigurationItems =>
            _database.GetCollection<ConfigurationItem>("ConfigurationItems");
    }
}
