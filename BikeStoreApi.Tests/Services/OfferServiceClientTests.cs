using BikeStoreApi.Composers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Platform.Client.Interfaces;
using Platform.Client.Mocks;
using Platform.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Client.Services.Tests
{
    [TestClass()]
    public class OfferServiceClientTests
    {
        private IOfferServiceClient _offersServiceClient;
        private OffersComposer _offersComposer;

        [TestInitialize]
        public void Initialize()
        {
        }


        [TestMethod()]
        public void CreateOfferTest()
        {
            Assert.Fail();
        }
    }
}