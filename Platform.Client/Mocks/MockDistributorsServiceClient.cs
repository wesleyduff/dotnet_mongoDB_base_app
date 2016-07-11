using Platform.Client.DataContracts.Distributor;
using Platform.Client.Interfaces;
using System.Threading.Tasks;
using System;
using MongoDB.Driver;
using Platform.Client.Properties;
using MongoDB.Bson;

namespace Platform.Client.Mocks
{
    public class MockDistributorsServiceClient : IDistributorsServiceClient
    {
        public IMongoDatabase Database;
        public MockDistributorsServiceClient()
        {
            var client = new MongoClient(Settings.Default.mongoLocal);
            Database = client.GetDatabase(Settings.Default.mongoLocalDb);
        }
        public async Task<DistributorsDataContract> GetDistributors()
        {
            var result = await Task.Run(() => Utils.ReadJsonFileAndDeserialize<DistributorsDataContract>("Distributors.json"));
            return result;
        }

        public async Task<long> GetNumberOfTestDocumentsInCollection()
        {

            await Insert();

            // Task<long> count = GetCollectionCount();
            //  count.Wait();
            var collection = Database.GetCollection<BsonDocument>("bar");
            await collection.CountAsync(new BsonDocument());
            return await collection.CountAsync(new BsonDocument());
        }
        
        public async Task GetDistributor(string name)
        {
            var collection = Database.GetCollection<BsonDocument>("distributors");
            var filter = Builders<BsonDocument>.Filter.Gte("Wes", 1);

            await collection.Find(filter).FirstAsync();

        }

        public async Task Insert()
        {
            var collection = Database.GetCollection<BsonDocument>("bar");

            BsonDocument seventies = new BsonDocument {
                { "Decade" , "1970s" },
                { "Artist" , "Debby WES" },
                { "Title" , "You Light Up My Life" },
                { "WeeksAtOne" , 10 }
              };

            await collection.InsertOneAsync(seventies);
        }
    }
}
