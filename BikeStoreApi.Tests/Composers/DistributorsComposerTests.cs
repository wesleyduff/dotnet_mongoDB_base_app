using BikeStoreApi.Composers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Platform.Client.DataContracts;
using Domain;
using System.Collections.Generic;
using Platform.Client.Services;

namespace BikeStoreApi.Composers.Tests
{
    [TestClass()]
    public class DistributorsComposerTests
    {

        private static DistributorsServiceClient _distributorsServiceClient;
        private static DistributorsComposer _distributorsComposer;
        private static DistributorsComposer _distributorsComposerMOCK;
        private List<Distributor> _domainDistributorsList;

        private const string FIRSTDISTRIBUTORSID = "57887cfe78198e3b7c8d559b";
        private const string SECONDDISTRIBUTORSID = "5788938478199030d82cfc98";

        JObject _distributorApiResponseFull;
        List<Distributor> _contractFull;

        [TestInitialize]
        public void Initialize()
        {
            _distributorsServiceClient = new DistributorsServiceClient();
            _distributorsComposer = new DistributorsComposer(_distributorsServiceClient);


            var collection = _distributorsComposer.Get();
            _contractFull = JsonConvert.DeserializeObject<List<Distributor>>(collection.Last.First.ToString());

        }

        private List<Distributor> GetDistributorsListFromDataContract(DistributorsDataContract _contractFull)
        {
            List<Distributor> distributorsList = new List<Distributor>() { };

            _contractFull.Result.ForEach(delegate (DistributorContract distributor)
            {

                #region initial build of distributors
                var convertedDistributor = new Distributor()
                {
                    Address = new Address()
                    {
                        City = distributor.Address.City,
                        Country = distributor.Address.Country,
                        PostalCode = distributor.Address.PostalCode,
                        State = distributor.Address.State,
                        StreetAddress = distributor.Address.StreetAddress,
                        StreetAddress2 = distributor.Address.StreetAddress2
                    },
                    Contact = new Contact()
                    {
                        EmailAddress = distributor.Contact.EmailAddress,
                        FirstName = distributor.Contact.FirstName,
                        LastName = distributor.Contact.LastName,
                        FullName = distributor.Contact.FullName,
                        PhoneNumber = distributor.Contact.PhoneNumber
                    },
                    Id = distributor.Id,
                    Inventory = new List<Line>() { },
                    Name = distributor.Name,
                    Offers = new List<Offers>() { },
                    ReceiptTypesOffered = new List<ReceiptType>() { }

                };
                #endregion

                #region Inventory Build
                convertedDistributor.Inventory = new List<Line>() { };
                distributor.Inventory.ForEach(delegate (LineDataContract line)
                {
                    convertedDistributor.Inventory.Add(new Line()
                    {
                        Bike = new Bike()
                        {
                            Brand = new Brand()
                            {
                                Name = line.Bike.Brand.Name
                            },
                            Id = line.Bike.Id,
                            Model = new BikeModel()
                            {
                                Name = line.Bike.Model.Name,
                                Year = line.Bike.Model.Year
                            },
                            Price = new Price()
                            {
                                Value = line.Bike.Price.Value
                            }
                        },
                        Id = line.Id,
                        Quantity = line.Quantity
                    });
                });
                #endregion

                #region Offers Build
                convertedDistributor.Offers = new List<Offers>() { };

                //We will never start with any offers from JSON file
                //User should add offers to their distributor
                /*distributor.Inventory.ForEach(delegate (OffersDataContract Offer)
                {
                    convertedDistributor.OFfers.Add(new Offers()
                    {
                        Discounts = new List<Discount>() { },
                        Id = Offer.Id,
                        Title = Offer.Title
                    });
                });*/
                #endregion

                #region Reciept Build

                #endregion


            });


            return distributorsList;
        }

        [TestMethod()]
        public void GetDistributors()
        {
            var collection = _distributorsComposer.Get();
            List<Distributor> distributors = JsonConvert.DeserializeObject<List<Distributor>>(collection.Last.First.ToString());

            Assert.AreEqual("success", collection.First.First);
            Assert.IsTrue(distributors.Count > 0);
        }

        [TestMethod()]
        public void GetDistributorById()
        {
            //get the ID of the first Distributor
            //The DataBase should not be empty.

            string findIdOfThisDistributor = _contractFull[0].Id;

            var response = _distributorsComposer.Get(findIdOfThisDistributor).Last.First.ToString();
            Distributor distributor = JsonConvert.DeserializeObject<Distributor>(response);
            Assert.AreEqual(findIdOfThisDistributor, distributor.Id);
        }

        [TestMethod()]
        public void DistributorShouldBeDeletedThenAdded()
        {
            /* ------------------------------------------------
           NOTE *
           Make sure you have the Test DB running and not the Local or the Production DB.
           - Found in "BaseModel.cs"
           -----------------------------------------------------*/

            var originalCount = _contractFull.Count;
            var result = _distributorsComposer.Delete(_contractFull[0].Id).Result.First.Last.ToString();

            Assert.IsTrue(result == "success");

            /* ADD the distributor back to the DB */
            Distributor distributor = new Distributor()
            {
                Address = new Address() { },
                Contact = new Contact() { },
                Inventory = new List<Line>() { },
                Name = "New Distributor Bike Outfitter",
                Offers = new List<Offers>() { },
                ReceiptTypesOffered = new List<ReceiptType>() { }
            };

            var _distributorApiResponse = _distributorsComposer.Create(distributor).Result.First.Last.ToString();
            Assert.IsTrue(_distributorApiResponse == "success");

        }

        [TestMethod()]
        public void ReceiptTypesShouldNotBeUpdated()
        {
            var receipt = new ReceiptType()
            {
                RType = ReceiptType.RTypes.FullHtml,
                RtypeAsString = ReceiptType.RTypes.FullHtml.ToString()
            };
            JObject distributorApiResponse_With_Added_RType = _distributorsComposer.UpdateRecieptTypes(new UpdateRecieptTypes()
                {
                    DistributorId = _contractFull[0].Id,
                    NewReciept = receipt
                }).Result;
            var result = distributorApiResponse_With_Added_RType.First.Last.ToString();


            //receipt type should already be loaded for distributor for FullHtml
            Assert.IsTrue(result == "false");
        }

        [TestMethod()]
        public void ReceiptTypesShouldBeUpdated()
        {

            /* ------------------------------------------------
           NOTE *
           Make sure you have the Test DB running and not the Local or the Production DB.
           - Found in "BaseModel.cs"
           -----------------------------------------------------*/


            var receipt = new ReceiptType()
            {
                RType = ReceiptType.RTypes.SummaryHtml,
                RtypeAsString = ReceiptType.RTypes.SummaryHtml.ToString()
            };
            JObject distributorApiResponse_With_Added_RType = _distributorsComposer.UpdateRecieptTypes(
                new UpdateRecieptTypes()
                {
                    DistributorId = _contractFull[0].Id,
                    NewReciept = receipt
                }).Result;
            var result = distributorApiResponse_With_Added_RType.First.Last.ToString();

            //receipt type should already be loaded for distributor for FullHtml
            Assert.IsTrue(result == "success");

            //remove the newly added receipt type to keep TestDB clean
            ReceiptTypeShouldBeRemoved();
        }

        [TestMethod()]
        public void ReceiptTypeShouldBeRemoved()
        {
            /* ------------------------------------------------
           NOTE *
           Make sure you have the Test DB running and not the Local or the Production DB.
           - Found in "BaseModel.cs"
           -----------------------------------------------------*/


            var collection = _distributorsComposer.Get();
            List<Distributor> _contractFull = JsonConvert.DeserializeObject<List<Distributor>>(collection.Last.First.ToString());
            if (_contractFull[0].ReceiptTypesOffered.Count <= 1)
            {
                //add the data
                var addReceipt = new ReceiptType()
                {
                    RType = ReceiptType.RTypes.SummaryHtml,
                    RtypeAsString = ReceiptType.RTypes.SummaryHtml.ToString()
                };
                var addResult = _distributorsComposer.UpdateRecieptTypes(
                    new UpdateRecieptTypes()
                    {
                        DistributorId = _contractFull[0].Id,
                        NewReciept = addReceipt
                    }).Result.First.Last.ToString();
                Assert.IsTrue(addResult == "success");
            }


            var receipt = new ReceiptType()
            {
                RType = ReceiptType.RTypes.SummaryHtml,
                RtypeAsString = ReceiptType.RTypes.SummaryHtml.ToString()
            };
            JObject distributorApiResponse_With_Added_RType = _distributorsComposer.UpdateRecieptTypes(
                new UpdateRecieptTypes()
                {
                    DistributorId = _contractFull[0].Id,
                    OldReciept = receipt
                }).Result;
            var result = distributorApiResponse_With_Added_RType.First.Last.ToString();

            //receipt type should already be loaded for distributor for FullHtml
            Assert.IsTrue(result == "success");
        }

        [TestMethod()]
        public void UpdateRecieptListTest()
        {
            /* ------------------------------------------------
            NOTE *
            Make sure you have the Test DB running and not the Local or the Production DB.
            - Found in "BaseModel.cs"
            -----------------------------------------------------*/
            List<ReceiptType> OldList = _contractFull[1].ReceiptTypesOffered;
            OldList.Add(new ReceiptType() { RType = ReceiptType.RTypes.Text, RtypeAsString = ReceiptType.RTypes.Text.ToString() });
            List<ReceiptType> NewLIst = new List<ReceiptType>()
            {
                new ReceiptType() { RType = ReceiptType.RTypes.Text, RtypeAsString = ReceiptType.RTypes.Text.ToString() },
                new ReceiptType() { RType = ReceiptType.RTypes.SummaryHtml, RtypeAsString = ReceiptType.RTypes.SummaryHtml.ToString() }
            };

            UdateReceiptList postData = new UdateReceiptList()
            {
                DistributorId = _contractFull[1].Id,
                ReceiptList = NewLIst
            };

            var result = _distributorsComposer.UpdateRecieptList(_contractFull[1].Id, postData).Result.First.Last.ToString();

            Assert.IsTrue(result == "success");

            //remove all Receipt items from DB for clean up testing
            var resultClean = _distributorsComposer.UpdateRecieptList(_contractFull[1].Id, new UdateReceiptList()
            {
                DistributorId = _contractFull[1].Id,
                ReceiptList = new List<ReceiptType>() { }
            }).Result.First.Last.ToString();

            Assert.IsTrue(resultClean == "success");
        }
    }
}