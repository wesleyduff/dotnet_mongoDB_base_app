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
        public Task<string> AddOfferToDistributer(string offerId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddProductToInventory(string distributorId, Bike bike)
        {
            var queryable = DistributorsCollection.AsQueryable();
            var inventoryQuery = from p in queryable
                                 where p.Id.Equals(ObjectId.Parse(distributorId))
                                 select new { p.Inventory };

            var inventoryList = inventoryQuery.First().Inventory;
            inventoryList.Add(bike);

            return await this.UpdateInventoryList(distributorId, inventoryList);
        }

        public async Task<bool> AdjustPrice(string distributorId, Bike.AdjustPrice adjustPrice)
        {
            List<Bike> inventory = ReturnInventoryForDistributor(distributorId);
            //find the correct bike to adjust price
            foreach (var product in inventory.Where(b => b.Id == adjustPrice.BikeId))
            {
                product.Cost = adjustPrice.NewPrice;
            }

            return await this.UpdateInventoryList(distributorId, inventory);

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

        private List<Bike> ReturnInventoryForDistributor(string distributorId)
        {
            var queryable = DistributorsCollection.AsQueryable();
            var inventoryQuery = from p in queryable
                                 where p.Id.Equals(ObjectId.Parse(distributorId))
                                 select new { p.Inventory };
            var inventoryList = inventoryQuery.First().Inventory;
            return inventoryList;
        }

        private async Task<bool> UpdateInventoryList(string distributorId, List<Bike> inventory)
        {
            var filter = Builders<Distributor>.Filter.Eq("_id", ObjectId.Parse(distributorId));
            var update = Builders<Distributor>.Update.Set("Inventory", inventory);
            UpdateResult result = await DistributorsCollection.UpdateOneAsync(filter, update);

            if (result.IsModifiedCountAvailable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
