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

        [TestMethod()]
        public async Task ComposerShouldNotBeNull()
        {
            var composer = await _distributorsComposer.Compose();
            Assert.IsNotNull(composer);
        }

        [TestMethod()]
        public async Task ComposerShouldContainAtLeaseOneDistributor()
        {
            var composer = await _distributorsComposer.Compose();
            Assert.IsTrue(composer.Count > 0);
        }
    }
}