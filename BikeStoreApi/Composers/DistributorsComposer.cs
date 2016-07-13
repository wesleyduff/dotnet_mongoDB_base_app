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
        private readonly IDistributorsServiceClient _distributorsServiceClient;

        public DistributorsComposer(IDistributorsServiceClient distributorsServiceClient)
        {
            _distributorsServiceClient = distributorsServiceClient;
        }
        
        public async Task<bool> CreateDistributor(Distributor distributor)
        {
            try
            {
                bool val = await _distributorsServiceClient.CreateDistributor(distributor);

                return val;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public async Task<bool> AddProductToInventory(string DistributorId, Bike bike)
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

        public async Task<bool> AdjustPrice(string distributorId, Bike.AdjustPrice adjustPrice)
        {
            try
            {
                return await _distributorsServiceClient.AdjustPrice(distributorId, adjustPrice);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}