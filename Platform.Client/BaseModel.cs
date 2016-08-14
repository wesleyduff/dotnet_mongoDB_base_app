using MongoDB.Driver;
using Platform.Client.Properties;
using Domain;

namespace Platform.Client
{
    public class BaseModel
    {
        public readonly IMongoDatabase Database;
        private readonly MongoClient mognoClient;
        public BaseModel()
        {
            if (mognoClient == null)
            {
                /* Local 
                var client = new MongoClient(Settings.Default.mongoLocal);
                Database = client.GetDatabase(Settings.Default.mongoLocalDb);
                */
                /* Test 
                mognoClient = new MongoClient(Settings.Default.mongoLocal);
                Database = mognoClient.GetDatabase(Settings.Default.mongoLocalTestDb);
                */

                /* PROD */
                var client = new MongoClient(Settings.Default.BikeDistributorConnectionString);
                Database = client.GetDatabase(Settings.Default.BikeDistributorDatabaseName);
                
            }

        }

        public IMongoCollection<Distributor> DistributorsCollection
        {
            get
            {
                return Database.GetCollection<Distributor>("distributors");
            }
        }

        public IMongoCollection<Offers> OffersCollection
        {
            get
            {
                return Database.GetCollection<Offers>("offers");
            }
        }

        public IMongoCollection<Discount> DiscountCollection
        {
            get
            {
                return Database.GetCollection<Discount>("discounts");
            }
        }

        public IMongoCollection<Line> LineCollection
        {
            get
            {
                return Database.GetCollection<Line>("Lines");
            }
        }

    }
}
