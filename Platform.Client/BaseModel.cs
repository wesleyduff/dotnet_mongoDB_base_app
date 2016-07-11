using MongoDB.Driver;
using Platform.Client.Properties;
using Domain;

namespace Platform.Client
{
    public class BaseModel
    {
        public IMongoDatabase Database;
        public BaseModel()
        {
            var client = new MongoClient(Settings.Default.mongoLocal);
            Database = client.GetDatabase(Settings.Default.mongoLocalDb);
        }

        public IMongoCollection<Distributor> DistributorsCollection {
            get
            {
                return Database.GetCollection<Distributor>("distributors");
            }
        }
    }
}
