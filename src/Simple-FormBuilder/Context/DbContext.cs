using MongoDB.Driver;
using Simple_Formbuilder.Entities;
using Simple_FormBuilder;

namespace Simple_Formbuilder.Context
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase _database = null;
        public DbContext(MongoConfiguration settings)
        {
            var client = new MongoClient(settings.MongoConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.MongoDatabase);
        }
        public IMongoCollection<FormsCollection> FormsCollection
        {
            get
            {
                return _database.GetCollection<FormsCollection>("FormsCollection");
            }
        }
        public IMongoCollection<T> GetCollection<T>(string serviceId)
        {
            return _database.GetCollection<T>($"FormRecords-{serviceId}");
        }
    }
}
