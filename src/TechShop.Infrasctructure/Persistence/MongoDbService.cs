using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Infrasctructure.Persistence
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;
        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;

            string connectionString = _configuration.GetConnectionString("DbConnection") ?? string.Empty;

            MongoUrl mongoUrl = MongoUrl.Create(connectionString);
            MongoClient mongoClient = new MongoClient(mongoUrl);

            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }
        public IMongoDatabase? Database => _database;
    }
}
