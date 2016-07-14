using MongoDB.Bson;
using Platform.Client.DataContracts.Distributor;
using System.Threading.Tasks;
using Domain;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Platform.Client.Interfaces
{
    public interface IDistributorsServiceClient
    {
        JObject GetDistributors();
        Task<JObject> CreateDistributor(Distributor distributor);
        JObject GetDistributor(string Id);
        Task<JObject> AddOfferToDistributer(string distributorId, Offers offer);

    }
}
