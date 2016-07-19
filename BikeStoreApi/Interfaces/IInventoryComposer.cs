using Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStoreApi.Interfaces
{
    public interface IInventoryComposer
    {
        JObject GetDistributorsInventory(string distributorId);
        Task<JObject> Create(Line line);
        Task<JObject> DeleteLineFromDistributor(string distributorId, string bikeId);
        Task<JObject> Delete(string lineId);
        Task<JObject> Update(UpdateLine postUpdateLine);
        Task<JObject> AddNewLineToInventory(string distributorId, Line line);
        Task<JObject> AdjustPrice(Bike.AdjustPrice adjustPrice);
        JObject GetReceiptData(string distributorId);
    }
}
