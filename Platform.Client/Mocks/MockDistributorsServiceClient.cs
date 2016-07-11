using Platform.Client.DataContracts.Distributor;
using Platform.Client.Interfaces;
using System.Threading.Tasks;
using System;
using MongoDB.Driver;
using Platform.Client.Properties;
using MongoDB.Bson;
using Domain;

namespace Platform.Client.Mocks
{
    public class MockDistributorsServiceClient : BaseModel, IDistributorsMockServiceClient
    {
        
        public async Task GetDistributors()
        {
            var collection = Database.GetCollection<BsonDocument>("distributors");

            await collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<long> GetNumberOfDistributors()
        {
            var collection = Database.GetCollection<BsonDocument>("bar");
            return await collection.CountAsync(new BsonDocument());
        }
        
        public async Task<BsonDocument> CreateDistributor()
        {
           var distributor = new Distributor
           {
               Name = "Austin Tri Cyclist",
               Address = new Address()
               {
                   City = "Austin",
                   Country = "US",
                   PostalCode = "78550",
                   State = "TX",
                   StreetAddress = "123 Red Bud"
               },
               Contact = new Contact()
                {
                    EmailAddress = "test@test.com",
                    FirstName = "Jane",
                    FullName = "Jane Doe",
                    IsPrimary = true,
                    LastName = "Doe",
                    PhoneNumber = "555-555-5555",
                    SalesDepartment = "Merch"
                }
            };

            BsonDocument doc = distributor.ToBsonDocument();

            var collection = Database.GetCollection<BsonDocument>("distributors");

            await collection.InsertOneAsync(doc);

            return doc;

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
