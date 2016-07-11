using Platform.Client.DataContracts.Distributor;
using Platform.Client.Interfaces;
using System.Threading.Tasks;

namespace Platform.Client.Mocks
{
    public class MockDistributorsServiceClient : IDistributorsServiceClient
    {
        public async Task<DistributorsDataContract> GetDistributors()
        {
            var result = await Task.Run(() => Utils.ReadJsonFileAndDeserialize<DistributorsDataContract>("Distributors.json"));
            return result;
        }
    }
}
