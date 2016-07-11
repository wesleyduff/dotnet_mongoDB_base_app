using BikeStoreApi.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace BikeStoreApi.Interfaces
{
    public interface IDistributorComposer
    {
        Task<bool> CreateDistributor(Distributor distributor);
        Task<string> GetDistributorsList();

        Distributor GetDistributor(string Id);
        Task<string> AddProductToInventory(string DistributorId, Bike bike);
    }
}