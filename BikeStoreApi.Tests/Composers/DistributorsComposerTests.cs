using Microsoft.VisualStudio.TestTools.UnitTesting;
using Platform.Client.Mocks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Platform.Client.DataContracts;
using Domain;
using System.Collections.Generic;
using Platform.Client.Services;
using System;

namespace BikeStoreApi.Composers.Tests
{
    [TestClass()]
    public class DistributorsComposerTests
    {

        private static MockDistributorsServiceClient _distributorsMockService;
        private static DistributorsServiceClient _distributorsServiceClient;
        private static DistributorsComposer _distributorsComposer;
        private static DistributorsComposer _distributorsComposerMOCK;
        private List<Distributor> _domainDistributorsList;

        private const string FIRSTDISTRIBUTORSID = "57887cfe78198e3b7c8d559b";
        private const string SECONDDISTRIBUTORSID = "5788938478199030d82cfc98";

        JObject _distributorApiResponseFull;
        DistributorsDataContract _contractFull;

        [TestInitialize]
        public void Initialize()
        {
            _distributorsMockService = new MockDistributorsServiceClient();
            _distributorsServiceClient = new DistributorsServiceClient();
            _distributorsComposerMOCK = new DistributorsComposer(_distributorsMockService);
            _distributorsComposer = new DistributorsComposer(_distributorsServiceClient);
            _distributorApiResponseFull = _distributorsComposer.Get();
            _contractFull = JsonConvert.DeserializeObject<DistributorsDataContract>(_distributorApiResponseFull.ToString());
            _domainDistributorsList = GetDistributorsListFromDataContract(_contractFull);

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
                    ReceiptTpesOffered = new List<ReceiptType>() { }

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
                convertedDistributor.ReceiptTpesOffered = new List<ReceiptType>() { };
                distributor.ReceiptTpesOffered.ForEach(delegate (ReceiptTypeDataContract receipt)
                {
                    convertedDistributor.ReceiptTpesOffered.Add(new ReceiptType()
                    {
                        RtypeAsString = receipt.RtypeAsString
                        
                    });
                });
                #endregion


            });


            return distributorsList;
        }

        [TestMethod()]
        public void GetDistributors()
        {
            Assert.IsTrue(_contractFull.Status.Equals("success"));
        }

        [TestMethod()]
        public void GetDistributorById()
        {
            JObject _distributorApiResponse = _distributorsComposer.Get(FIRSTDISTRIBUTORSID);
            DistributorContract contractData = JsonConvert.DeserializeObject<DistributorContract>(_distributorApiResponse.ToString());
            Assert.AreEqual(FIRSTDISTRIBUTORSID, contractData.Id);
        }

        [TestMethod()]
        public void DistributorShouldBeDeleted()
        {
            var originalCount = _contractFull.Result.Count;

            JObject _distributorApiResponse = _distributorsComposerMOCK.Delete(FIRSTDISTRIBUTORSID).Result;
            DistributorsDataContract _contract = JsonConvert.DeserializeObject<DistributorsDataContract>(_distributorApiResponse.ToString());

            Assert.IsTrue(originalCount > _contract.Result.Count);
        }

        [TestMethod()]
        public void NewDistributorShouldBeCreated()
        {
            Distributor distributor = new Distributor()
            {
                Address = new Address() { },
                Contact = new Contact() { },
                Id = "11187cfe78198e3b7c8d559c",
                Inventory = new List<Line>() { },
                Name = "New Distributor Bike Outfitter",
                Offers = new List<Offers>() { },
                ReceiptTpesOffered = new List<ReceiptType>() { }
            };

            JObject _distributorApiResponse = _distributorsComposer.Create(distributor).Result;
            DistributorsDataContract returnedContractData = JsonConvert.DeserializeObject<DistributorsDataContract>(_distributorApiResponse.ToString());

            Assert.IsTrue(_contractFull.Result.Count < returnedContractData.Result.Count);
        }

        [TestMethod()]
        public void UpdateAddRecieptTypesOptionTest()
        {
            var reciept = new ReceiptTypeDataContract() { RType = 0, RtypeAsString = "FullHtml" };
            JObject distributorApiResponse_With_Added_RType = _distributorsComposer.UpdateRecieptTypes(
                new UpdateRecieptTypes()
                {
                    DistributorId = FIRSTDISTRIBUTORSID,
                    NewReciept = new ReceiptType()
                    {
                        RType = ReceiptType.RTypes.FullHtml,
                        RtypeAsString = ReceiptType.RTypes.FullHtml.ToString()
                    }
                }).Result;
            DistributorContract contactData = JsonConvert.DeserializeObject<DistributorContract>(distributorApiResponse_With_Added_RType.ToString());

            Assert.IsNotNull(contactData.ReceiptTpesOffered.Find(r => r.RtypeAsString == reciept.RtypeAsString));
        }

        [TestMethod()]
        public void UpdateRemoveRecieptTypesOptionTest()
        {
            var reciept = new ReceiptTypeDataContract() { RType = 0, RtypeAsString = "FullHtml" };
            JObject distributorApiResponse_With_Added_RType = _distributorsComposer.UpdateRecieptTypes(
                new UpdateRecieptTypes()
                {
                    DistributorId = SECONDDISTRIBUTORSID,
                    OldReciept = new ReceiptType()
                    {
                        RType = ReceiptType.RTypes.FullHtml,
                        RtypeAsString = ReceiptType.RTypes.FullHtml.ToString()
                    }
                }).Result;
            DistributorContract contactData = JsonConvert.DeserializeObject<DistributorContract>(distributorApiResponse_With_Added_RType.ToString());

            Assert.IsTrue(contactData.ReceiptTpesOffered.Count == 0);
        }
    }
}