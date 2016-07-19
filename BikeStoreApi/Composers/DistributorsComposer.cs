using System;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json.Linq;
using BikeStoreApi.Interfaces;
using Platform.Client.Interfaces;
using MongoDB.Bson;

namespace BikeStoreApi.Composers
{
    public class DistributorsComposer : IDistributorComposer
    {

        private readonly IDistributorsServiceClient _distributorsServiceClient;
     

        public DistributorsComposer(IDistributorsServiceClient service)
        {
            _distributorsServiceClient = service;
        }

        public Task<JObject> Create(Distributor item)
        {
            //Create string ID from MonogDB BsonId
            item.Id = ObjectId.GenerateNewId(DateTime.Now).ToString();
            return _distributorsServiceClient.Create(item);
        }

        public Task<JObject> Delete(string itemId)
        {
            return _distributorsServiceClient.Delete(itemId);
        }

        public JObject Get()
        {
            return _distributorsServiceClient.Get();
        }

        public JObject Get(string itemId)
        {
            ObjectId monogoId = ObjectId.Parse(itemId);
            return _distributorsServiceClient.Get(monogoId);
        }

        public Task<JObject> Update(Distributor postUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<JObject> UpdateRecieptTypes(UpdateRecieptTypes postUpdate)
        {
            return _distributorsServiceClient.UpdateRecieptTypes(postUpdate);
        }

        public Task<JObject> UpdateRecieptList(string distributorId, UdateReceiptList postData)
        {
            return _distributorsServiceClient.UpdateRecieptList(distributorId, postData);
        }

        public JObject GetReceiptTypes()
        {
            return _distributorsServiceClient.GetReceiptTypes();
        }
    }
}