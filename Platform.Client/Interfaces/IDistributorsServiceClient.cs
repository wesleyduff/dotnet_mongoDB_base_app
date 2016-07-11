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
        Task<string> AddProductToInventory(string DistributorId, Bike bike);
        Distributor GetDistributor(string Id);

    }
}
