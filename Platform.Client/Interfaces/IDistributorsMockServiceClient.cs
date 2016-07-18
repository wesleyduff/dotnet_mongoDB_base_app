using MongoDB.Bson;
using System.Threading.Tasks;

namespace Platform.Client.Interfaces
{
    public interface IDistributorsMockServiceClient
    {
        Task GetDistributors();
        Task<long> GetNumberOfDistributors();
        Task<BsonDocument> CreateDistributor();

    }
}
