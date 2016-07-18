using System;
using System.Threading.Tasks;
using BikeStoreApi.Interfaces;
using Newtonsoft.Json.Linq;
using Domain;
using Platform.Client.Interfaces;
using Platform.Client.Services;
using MongoDB.Bson;

namespace BikeStoreApi.Composers
{
    public class DiscountComposer : IDiscountComposer
    {

        private readonly IDiscountServiceClient _discountServiceClient;

        public DiscountComposer(IDiscountServiceClient discountServiceClient)
        {
            _discountServiceClient = discountServiceClient;
        }

        public void Compose()
        {
            throw new NotImplementedException();
        }

        public Task<JObject> Create(Discount item)
        {
            //Create string ID from MonogDB BsonId
            item.Id = ObjectId.GenerateNewId(DateTime.Now).ToString();
            return _discountServiceClient.Create(item);
        }

        public Task<JObject> Delete(string itemId)
        {
            return _discountServiceClient.Delete(itemId);
        }

        public JObject Get()
        {
            return _discountServiceClient.Get();
        }

        public JObject Get(string itemId)
        {
            ObjectId mongoId = ObjectId.Parse(itemId);
            return _discountServiceClient.Get(mongoId);
        }

        /* Not Implemented For This Demo */
        public Task<JObject> Update(Discount item)
        {
            throw new NotImplementedException();
        }
    }
}