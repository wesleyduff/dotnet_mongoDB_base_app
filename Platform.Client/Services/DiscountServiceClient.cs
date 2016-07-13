using Platform.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Platform.Client.Services
{
    public class DiscountServiceClient : BaseModel, IDiscountServiceClient
    {
        public async Task<string> CreateDiscount(Discount discount)
        {
            try
            {
                await DiscountCollection.InsertOneAsync(discount);
                return discount.ToBsonDocument().ToJson();
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    status = "Exception Thrown",
                    result = false,
                    message = ex.Message
                });
            }
        }

        public async Task<string> DeleteDiscount(string id)
        {
            try
            {
                var filter = Builders<Discount>.Filter.Eq("_id", ObjectId.Parse(id));

                DeleteResult result = await DiscountCollection.DeleteOneAsync(filter);

                if (result.IsAcknowledged)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = "success",
                        result = true
                    });
                }
                else
                {
                   return JsonConvert.SerializeObject(new
                    {
                        status = "false",
                        result = false
                    });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    status = "Exception Thrown",
                    result = false,
                    message = ex.Message
                });
            }
        }

        public string GetDiscount(string id)
        {
            try
            {
                var queryableDiscount = DiscountCollection.AsQueryable();
                var queryOffer = from d in queryableDiscount
                                 where d.Id.Equals(ObjectId.Parse(id))
                                 select d;
                return queryableDiscount.First().ToJson();

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    status = "Exception Thrown",
                    result = false,
                    message = ex.Message
                });
            }
           
        }

        public string GetDiscounts()
        {
            return DiscountCollection.Find(new BsonDocument()).ToJson();
        }
    }
}
