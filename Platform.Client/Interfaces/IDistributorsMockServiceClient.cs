using MongoDB.Bson;
using Platform.Client.DataContracts.Distributor;
using System.Threading.Tasks;
using Domain;

namespace Platform.Client.Interfaces
{
    public interface IDistributorsMockServiceClient
    {
        Task GetDistributors();
        Task<long> GetNumberOfDistributors();
        Task<BsonDocument> CreateDistributor();

    }
}
