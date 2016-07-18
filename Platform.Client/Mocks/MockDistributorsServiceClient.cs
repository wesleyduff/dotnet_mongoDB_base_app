using Platform.Client.DataContracts;
using Platform.Client.Interfaces;
using System.Threading.Tasks;
using System;
using MongoDB.Driver;
using Platform.Client.Properties;
using MongoDB.Bson;
using Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Platform.Client.Mocks
{
    public class MockDistributorsServiceClient : BaseModel, IDistributorsServiceClient
    {

        private DistributorsDataContract mockContract;
        private const string FIRSTDISTRIBUTORSID = "57887cfe78198e3b7c8d559b";

        public MockDistributorsServiceClient()
        {
            mockContract = Utils.ReadJsonFileAndDeserialize<DistributorsDataContract>(@"Distributors.json");
        }

        public Task<JObject> AddOfferToDistributer(string distributorId, Offers offer)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> Create(Distributor item)
        {
            var json = JObject.FromObject(mockContract);
            DistributorsDataContract dataContract = JsonConvert.DeserializeObject<DistributorsDataContract>(json.ToString());
            DistributorContract distributorToAdd = new DistributorContract()
            {
                Address = new AddressDataContract() { },
                Contact = new ContactDataContract() { },
                Id = item.Id,
                Inventory = new List<LineDataContract>() { },
                Name = item.Name,
                Offers = new List<OffersDataContract>() { },
                ReceiptTpesOffered = new List<ReceiptTypeDataContract>() { }
            };
            dataContract.Result.Add(distributorToAdd);
            return await Task.Run(() => JObject.FromObject(dataContract));
        }

        public async Task<JObject> Delete(string itemId)
        {
            //cannot run mock data here without writing code to remove data from JSON file.
            var json = JObject.FromObject(mockContract);
            DistributorsDataContract dataContract = JsonConvert.DeserializeObject<DistributorsDataContract>(json.ToString());
            DistributorContract _contract = dataContract.Result.Find(d => d.Id == itemId);
            dataContract.Result.Remove(_contract);
            return await Task.Run(() => JObject.FromObject(dataContract));
        }

        public JObject Get()
        {
            return JObject.FromObject(mockContract);
        }

        public JObject Get(ObjectId itemId)
        {
            var json = JObject.FromObject(mockContract);
            DistributorsDataContract dataContract = JsonConvert.DeserializeObject<DistributorsDataContract>(json.ToString());
            return JObject.FromObject(dataContract.Result.Find(d => d.Id == itemId.ToString()));
        }

        public Task<JObject> Update(Distributor item)
        {
            throw new NotImplementedException();
        }

        public async Task<JObject> UpdateRecieptTypes(UpdateRecieptTypes postUpdate)
        {
            //Get receipt types
            var json = JObject.FromObject(mockContract);
            DistributorsDataContract dataContracts = JsonConvert.DeserializeObject<DistributorsDataContract>(json.ToString());
            DistributorContract dataContract = dataContracts.Result.Find(d => d.Id == postUpdate.DistributorId);
            var recieptTypes = dataContract.ReceiptTpesOffered;

            bool newReceiptfound = false;
            bool oldRecieptFound = false;

            if (recieptTypes != null)
            {
                dataContract.ReceiptTpesOffered.ForEach(delegate (ReceiptTypeDataContract recieptType)
                {
                    if (postUpdate.NewReciept != null && postUpdate.NewReciept.RtypeAsString.Equals(recieptType.RtypeAsString))
                    {
                        newReceiptfound = true;
                    }
                    if (postUpdate.OldReciept != null && postUpdate.OldReciept.RtypeAsString.Equals(recieptType.RtypeAsString)){
                        oldRecieptFound = true;
                    }
                });
            }
            else
            {
                recieptTypes = new List<ReceiptTypeDataContract>() { };
            }

            if (!newReceiptfound && postUpdate.NewReciept != null)
            {
                //add receipt to list
                recieptTypes.Add(
                    new ReceiptTypeDataContract()
                    {
                        RType = (int)postUpdate.NewReciept.RType,
                        RtypeAsString = postUpdate.NewReciept.RtypeAsString
                    });
                return await UpdateDistributor(postUpdate.DistributorId, recieptTypes);
            }
            else if (oldRecieptFound && postUpdate.OldReciept != null)
            {
                ReceiptTypeDataContract receiptToRemove = recieptTypes.Find(r => r.RtypeAsString == postUpdate.OldReciept.RtypeAsString);
                //remove old receipt from list
                recieptTypes.Remove(receiptToRemove);

                return await UpdateDistributor(postUpdate.DistributorId, recieptTypes);
            }

            else
            {
                //Could not update
                return JObject.FromObject(new
                {
                    status = "false",
                    message = "Could not update your reciept list to the specified Distributor",
                    result = false
                });

               
            }
        }

        private Task<JObject> UpdateDistributor(string distributorId, List<ReceiptTypeDataContract> recieptTypes)
        {

            DistributorContract distributor = GetDistributorById(distributorId);
            distributor.ReceiptTpesOffered = recieptTypes;

            return Task.Run(() => JObject.FromObject(distributor));
        }

        private DistributorContract GetDistributorById(string distributorId)
        {
            var json = JObject.FromObject(mockContract);
            DistributorsDataContract dataContracts = JsonConvert.DeserializeObject<DistributorsDataContract>(json.ToString());
            DistributorContract dataContract = dataContracts.Result.Find(d => d.Id == distributorId);
            return dataContract;
        }
    }
}
