using MongoDB.Bson;
using Platform.Client.DataContracts.Distributor;
using System.Threading.Tasks;
using Domain;
using System.Collections.Generic;

namespace Platform.Client.Interfaces
{
    public interface IDistributorsServiceClient
    {
        Task<List<Distributor>> GetDistributors();
        Task<long> GetNumberOfDistributors();
        Task<bool> CreateDistributor(Distributor distributor);
        Task<bool> AddProductToInventory(string distributorId, Bike bike);
        Distributor GetDistributor(string Id);
        Task<bool> AdjustPrice(string distributorId, Bike.AdjustPrice adjustPrice);

    }
}
