using Microsoft.VisualStudio.TestTools.UnitTesting;
using Platform.Client.Mocks;
using Platform.Client.Interfaces;
using BikeStoreApi.Interfaces;
using System.Threading.Tasks;

namespace BikeStoreApi.Composers.Tests
{
    [TestClass()]
    public class DistributorsComposerTests
    {

        private MockDistributorsServiceClient _distributorsMockService;
        private DistributorsComposer _distributorsComposer;

        [TestInitialize]
        public void Initialize()
        {
            _distributorsMockService = new MockDistributorsServiceClient();

            _distributorsComposer = new DistributorsComposer(_distributorsMockService);
        }

       

       
    }
}