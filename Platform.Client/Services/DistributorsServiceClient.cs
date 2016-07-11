using Platform.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Platform.Client.Services
{
    public class DistributorsServiceClient : BaseModel, IDistributorsServiceClient
    {

        public async Task<string> AddProductToInventory(string DistributorId, Bike bike)
        {
            var queryable = DistributorsCollection.AsQueryable();
            var inventoryQuery = from p in queryable
                                 where p.Id.Equals(ObjectId.Parse(DistributorId))
                                 select new { p.Inventory };

            var inventoryList = inventoryQuery.First().Inventory;
            inventoryList.Add(bike);

            var filter = Builders<Distributor>.Filter.Eq("_id", ObjectId.Parse(DistributorId));
            var update = Builders<Distributor>.Update.Set("Inventory", inventoryList);
            UpdateResult result = await DistributorsCollection.UpdateOneAsync(filter, update);

            return result.ToString();
        }

        public async Task<bool> CreateDistributor(Distributor distributor)
        {
            try
            {
                await DistributorsCollection.InsertOneAsync(distributor);

                return true;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Distributor GetDistributor(string Id)
        {
            var queryable = DistributorsCollection.AsQueryable();
            var query = from p in queryable
                        where p.Id.Equals(ObjectId.Parse(Id))
                        select p;
            return query.FirstOrDefault();
        }

        public async Task<List<Distributor>> GetDistributors()
        {
            List<Distributor> List = await DistributorsCollection.Find(new BsonDocument()).ToListAsync();
            return List;
        }

        public async Task<long> GetNumberOfDistributors()
        {
            return await DistributorsCollection.CountAsync(new BsonDocument());
        }
    }
}
