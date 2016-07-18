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
using Newtonsoft.Json.Linq;

namespace Platform.Client.Services
{
    public class DiscountServiceClient : BaseModel, IDiscountServiceClient, ICrud<Discount, ObjectId>
    {
        public async Task<JObject> Create(Discount item)
        {
            try
            {
                await DiscountCollection.InsertOneAsync(item);
                return
                    JObject.FromObject(
                    new
                    {
                        status = "success",
                        result = item
                    }
                );
            }
            catch (Exception ex)
            {
                return
                    JObject.FromObject(
                    new
                    {
                        status = "Exception Thrown",
                        result = false,
                        message = ex.Message
                    }
                );
            }
        }

        public async Task<JObject> Delete(string itemId)
        {
            try
            {
                var filter = Builders<Discount>.Filter.Eq("_id", ObjectId.Parse(itemId));

                DeleteResult result = await DiscountCollection.DeleteOneAsync(filter);

                if (result.IsAcknowledged)
                {
                    return
                        JObject.FromObject(
                            new
                            {
                                status = "success",
                                result = true,
                                message = "Discount was deleted"
                            }
                        );
                }
                else
                {
                    return
                        JObject.FromObject(
                            new
                            {
                                status = "false",
                                result = false,
                                message = "Discount could not be deleted"
                            }
                        );
                }
            }
            catch (Exception ex)
            {
                return
                    JObject.FromObject(
                    new
                    {
                        status = "Exception Thrown",
                        result = false,
                        message = ex.Message
                    }
                );
            }
        }
        public JObject Get()
        {
            try
            {
                List<Discount> collection = DiscountCollection.Find(new BsonDocument()).ToList();
                JObject returnJson = JObject.FromObject(
                    new
                    {
                        status = "success",
                        result = collection
                    }
                );
                return returnJson;
            }
            catch (Exception ex)
            {
                return
                    JObject.FromObject(
                    new
                    {
                        status = "Exception Thrown",
                        result = false,
                        message = ex.Message
                    }
                );
            }
        }

        public JObject Get(ObjectId itemId)
        {
            try
            {
                var queryableDiscount = DiscountCollection.AsQueryable();
                var queryOffer = from d in queryableDiscount
                                 where d.Id.Equals(itemId)
                                 select d;
                var discount = queryableDiscount.First();
                return
                    JObject.FromObject(
                    new
                    {
                        status = "success",
                        result = discount
                    }
                );
            }
            catch (Exception ex)
            {
                return
                   JObject.FromObject(
                   new
                   {
                       status = "Exception Thrown",
                       result = false,
                       message = ex.Message
                   }
               );
            }
        }

        public Task<JObject> Update(Discount postUpdate)
        {
            throw new NotImplementedException();
        }



        /* OLD

        public async Task<JObject> CreateDiscount(Discount discount)
        {
            try
            {
                await DiscountCollection.InsertOneAsync(discount);
                return
                    JObject.FromObject(
                    new
                    {
                        status = "success",
                        result = discount
                    }
                );
            }
            catch (Exception ex)
            {
                return
                    JObject.FromObject(
                    new
                    {
                        status = "Exception Thrown",
                        result = false,
                        message = ex.Message
                    }
                );
            }
        }
        public async Task<JObject> DeleteDiscount(string id)
        {
            try
            {
                var filter = Builders<Discount>.Filter.Eq("_id", ObjectId.Parse(id));

                DeleteResult result = await DiscountCollection.DeleteOneAsync(filter);

                if (result.IsAcknowledged)
                {
                    return
                        JObject.FromObject(
                            new
                            {
                                status = "success",
                                result = true,
                                message = "Discount was deleted"
                            }
                        );
                }
                else
                {
                    return
                        JObject.FromObject(
                            new
                            {
                                status = "false",
                                result = false,
                                message = "Discount could not be deleted"
                            }
                        );
                }
            }
            catch (Exception ex)
            {
                return
                    JObject.FromObject(
                    new
                    {
                        status = "Exception Thrown",
                        result = false,
                        message = ex.Message
                    }
                );
            }
        }
        public JObject GetDiscount(string id)
        {
            try
            {
                var queryableDiscount = DiscountCollection.AsQueryable();
                var queryOffer = from d in queryableDiscount
                                 where d.Id.Equals(ObjectId.Parse(id))
                                 select d;
                var discount = queryableDiscount.First();
                return
                    JObject.FromObject(
                    new
                    {
                        status = "success",
                        result = discount
                    }
                );
            }
            catch (Exception ex)
            {
                return
                   JObject.FromObject(
                   new
                   {
                       status = "Exception Thrown",
                       result = false,
                       message = ex.Message
                   }
               );
            }

        }
        public JObject GetDiscounts()
        {
            try
            {
                List<Discount> collection = DiscountCollection.Find(new BsonDocument()).ToList();
                JObject returnJson = JObject.FromObject(
                    new
                    {
                        status = "success",
                        result = collection
                    }
                );
                return returnJson;
            }
            catch (Exception ex)
            {
                return
                    JObject.FromObject(
                    new
                    {
                        status = "Exception Thrown",
                        result = false,
                        message = ex.Message
                    }
                );
            }

        }
         --*/
    }

}
