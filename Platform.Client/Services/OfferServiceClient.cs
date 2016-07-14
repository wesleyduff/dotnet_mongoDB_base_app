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
    public class OfferServiceClient : BaseModel, IOfferServiceClient
    {
        public async Task<JObject> AddOfferToDistributor(string distributorId, Offers offer)
        {
            try
            {
                var filter = Builders<Distributor>.Filter.Eq("_id", ObjectId.Parse(distributorId));
                var update = Builders<Distributor>.Update.Push("Offers", offer);


                //add offer to distributor 
                UpdateResult result = await DistributorsCollection.UpdateOneAsync(filter, update);

                if (result.IsModifiedCountAvailable)
                {
                    return
                       JObject.FromObject(
                           new
                           {
                               status = "success",
                               result = true,
                               message = "Offer added to distributor"
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
                                message = "Offer could not be added to distributor"
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

        public async Task<JObject> CreateOffer(Offers offer)
        {
            try
            {
                await OffersCollection.InsertOneAsync(offer);
                return
                        JObject.FromObject(
                            new
                            {
                                status = "success",
                                result = offer,
                                message = "Offer was created"
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

        public async Task<JObject> DeleteOffer(string id)
        {
            try
            {
                var filter = Builders<Offers>.Filter.Eq("_id", ObjectId.Parse(id));

                DeleteResult result = await OffersCollection.DeleteOneAsync(filter);

                if (result.IsAcknowledged)
                {
                    return
                        JObject.FromObject(
                            new
                            {
                                status = "success",
                                result = true,
                                message = "Offer was deleted"
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
                                message = "Offer could not be deleted"
                            }
                        );
                }
            }
            catch(Exception ex)
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

        public JObject GetOffer(string id)
        {
            try
            {
                var offer = GetOfferById(id);
                return
                   JObject.FromObject(
                   new
                   {
                       status = "success",
                       result = offer
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
        public JObject GetOffers()
        {
            List<Offers> collection = OffersCollection.Find(new BsonDocument()).ToList();
            try
            {
                JObject returnJson = JObject.FromObject(
                        new
                        {
                            status = "success",
                            result = collection
                        }
                    );
                return returnJson;
            }
            catch(Exception ex)
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
        public JObject GetOffersForDistributor(string distributorId)
        {
            try
            {
                Distributor distributor = GetDistributorById(distributorId);

                List<Offers> OffersList = new List<Offers>();

                foreach (var offer in distributor.Offers)
                {
                    Offers selectedoffer = GetOfferById(offer.Id);
                    OffersList.Add(selectedoffer);
                }

                return
                       JObject.FromObject(
                       new
                       {
                           status = "success",
                           result = OffersList
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

        public async Task<JObject> AddDiscountToOffer(string offerId, Discount discount)
        {
            try
            {

                //get Offer
                Offers offer = GetOfferById(offerId);
                List<Discount> discounts = offer.Discounts;
                discounts.Add(discount);

                var filter = Builders<Offers>.Filter.Eq("_id", ObjectId.Parse(offerId));
                var update = Builders<Offers>.Update.Set("Discounts", discounts);
                UpdateResult result = await OffersCollection.UpdateOneAsync(filter, update);

                if (result.IsModifiedCountAvailable)
                {
                    return
                       JObject.FromObject(
                           new
                           {
                               status = "success",
                               result = true,
                               message = "Added Discount to Offer"
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
                                message = "Could not add Discount to Offer"
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

        public async Task<JObject> RemoveDiscountFromOffer(string offerId, string discountId)
        {
            try
            {
                //get discount
                var discountQueryable = DiscountCollection.AsQueryable();
                var discountQuery = from d in discountQueryable
                                    where d.Id.Equals(ObjectId.Parse(discountId))
                                    select d;
                Discount discount = discountQuery.First();

                //get Offer
                Offers offer = GetOfferById(offerId);
                List<Discount> discounts = offer.Discounts;
                if (discounts.Contains(discount))
                {
                    discounts.Remove(discount);

                    var filter = Builders<Offers>.Filter.Eq("_id", ObjectId.Parse(offerId));
                    var update = Builders<Offers>.Update.Set("Discounts", discounts);
                    UpdateResult result = await OffersCollection.UpdateOneAsync(filter, update);

                    if (result.IsModifiedCountAvailable)
                    {
                        return
                           JObject.FromObject(
                               new
                               {
                                   status = "success",
                                   result = true,
                                   message = "Added Discount to Offer"
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
                                    message = "Could not add Discount to Offer"
                                }
                            );
                    }
                }
                else
                {
                    return
                           JObject.FromObject(
                               new
                               {
                                   status = "false",
                                   result = false,
                                   message = "That offer does not contain this discount"
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

        /* ******************
        Helper Methods 
        */
        private Offers GetOfferById(string id)
        {
            var queryableOffer = OffersCollection.AsQueryable();
            var queryOffer = from o in queryableOffer
                             where o.Id.Equals(ObjectId.Parse(id))
                             select o;
            return queryOffer.First();
        }

        private Distributor GetDistributorById(string id)
        {
            var queryable = DistributorsCollection.AsQueryable();
            var query = from p in queryable
                        where p.Id.Equals(ObjectId.Parse(id))
                        select p;
            return query.First();
        }

      
    }
}
