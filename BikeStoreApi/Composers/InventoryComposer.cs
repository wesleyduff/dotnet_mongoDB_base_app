using BikeStoreApi.Interfaces;
using Newtonsoft.Json.Linq;
using Platform.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using System.Threading.Tasks;

namespace BikeStoreApi.Composers
{
    public class InventoryComposer : IInventoryComposer
    {
        private readonly ILineServiceClient _inventoryServiceClient;


        public InventoryComposer(ILineServiceClient inventoryServiceClient)
        {
            _inventoryServiceClient = inventoryServiceClient;
        }

        public Task<JObject> AddNewLineToInventory(string distributorId, Line line)
        {
            return _inventoryServiceClient.AddNewLineToInventory(distributorId, line);
        }

        public Task<JObject> AdjustPrice(Bike.AdjustPrice adjustPrice)
        {
            return _inventoryServiceClient.AdjustPrice(adjustPrice);
        }

        public Task<JObject> Create(Line line)
        {
            return _inventoryServiceClient.Create(line);
        }

        public Task<JObject> Delete(string lineId)
        {
            return _inventoryServiceClient.Delete(lineId);
        }

        public Task<JObject> DeleteLineFromDistributor(string distributorId, string bikeId)
        {
            return _inventoryServiceClient.DeleteLineFromDistributor(distributorId, bikeId);
        }

        public JObject GetDistributorsInventory(string distributorId)
        {
            return _inventoryServiceClient.GetDistributorsInventory(distributorId);
        }

        public JObject GetReceiptData(string distributorId)
        {
            return _inventoryServiceClient.GetReceiptData(distributorId);
        }

        public Task<JObject> Update(UpdateLine postUpdateLine)
        {
            return _inventoryServiceClient.Update(postUpdateLine);
        }
    }
}