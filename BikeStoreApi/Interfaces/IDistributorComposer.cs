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
        Task<bool> AddProductToInventory(string distributorId, Bike bike);
        Task<bool> AdjustPrice(string distributorId, Bike.AdjustPrice adjustPrice);
    }
}