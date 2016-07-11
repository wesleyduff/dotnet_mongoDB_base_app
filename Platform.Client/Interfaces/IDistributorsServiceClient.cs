using Platform.Client.DataContracts.Distributor;
using System.Threading.Tasks;

namespace Platform.Client.Interfaces
{
    public interface IDistributorsServiceClient
    {
        Task<DistributorsDataContract> GetDistributors();
    }
}
