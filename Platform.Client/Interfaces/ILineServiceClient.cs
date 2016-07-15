using Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Client.Interfaces
{
    public interface ILineServiceClient
    {
        JObject GetDistributorsInventory(string distributorId);
        Task<JObject> Create(Line line);
        Task<JObject> DeleteLineFromDistributor(string distributorId, string bikeId);
        Task<JObject> Delete(string lineId);
        //remove and add bike to line through update
        Task<JObject> Update(UpdateLine postUpdateLine);
        Task<JObject> AddNewLineToInventory(string distributorId, Line line);
        Task<JObject> AdjustPrice(Bike.AdjustPrice adjustPrice);
    }
}
