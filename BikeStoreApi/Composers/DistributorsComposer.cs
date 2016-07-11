using System;
using System.Threading.Tasks;
using BikeStoreApi.Interfaces;
using BikeStoreApi.Models;
using Platform.Client.Interfaces;
using Platform.Client.DataContracts.Distributor;
using System.Collections.Generic;

namespace BikeStoreApi.Composers
{
    public class DistributorsComposer : IDistributorComposer
    {
        private List<DistributorModels> model;
        private readonly IDistributorsServiceClient _distributorsServiceClient;

        public DistributorsComposer(IDistributorsServiceClient distributorsServiceClient)
        {
            _distributorsServiceClient = distributorsServiceClient;
        }

        public async Task<List<DistributorModels>> Compose()
        {
            model = new List<DistributorModels>();
            var distributorsContract = await _distributorsServiceClient.GetDistributors();
            var countTest = await _distributorsServiceClient.GetNumberOfTestDocumentsInCollection();

            distributorsContract.Distributors.ForEach(delegate (DistributorContract distributor)
            {
                model.Add(
                    new DistributorModels()
                    {
                        Name = distributor.Name
                    });
            });

            return model;
        }
    }
}