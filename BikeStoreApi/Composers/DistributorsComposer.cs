using System;
using System.Threading.Tasks;
using BikeStoreApi.Interfaces;
using BikeStoreApi.Models;
using Platform.Client.Interfaces;
using Platform.Client.DataContracts.Distributor;
using System.Collections.Generic;
using MongoDB.Bson;
using Domain;
using Platform.Client.Mocks;

namespace BikeStoreApi.Composers
{
    public class DistributorsComposer : IDistributorComposer
    {
        private List<DistributorModels> model;
        private MockDistributorsServiceClient _distributorsMockService;
        private readonly IDistributorsServiceClient _distributorsServiceClient;

        public DistributorsComposer(MockDistributorsServiceClient _distributorsMockService)
        {
            this._distributorsMockService = _distributorsMockService;
        }

        public DistributorsComposer(IDistributorsServiceClient distributorsServiceClient)
        {
            _distributorsServiceClient = distributorsServiceClient;
        }


        public async Task<bool> CreateDistributor(Distributor distributor)
        {
            bool val = await _distributorsServiceClient.CreateDistributor(distributor);

            return val;
        }

        public async Task<string> AddProductToInventory(string DistributorId, Bike bike)
        {
            try
            {
                return await _distributorsServiceClient.AddProductToInventory(DistributorId, bike);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Distributor GetDistributor(string Id)
        {
            try
            {
                Distributor doc = _distributorsServiceClient.GetDistributor(Id);
                return doc;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<string> GetDistributorsList()
        {
            List<Distributor> List = await _distributorsServiceClient.GetDistributors();

            return List.ToJson();
        }


    }
}