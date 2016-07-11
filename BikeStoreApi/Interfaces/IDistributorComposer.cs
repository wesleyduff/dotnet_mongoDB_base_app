using BikeStoreApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeStoreApi.Interfaces
{
    public interface IDistributorComposer
    {
        Task<List<DistributorModels>> Compose();
    }
}