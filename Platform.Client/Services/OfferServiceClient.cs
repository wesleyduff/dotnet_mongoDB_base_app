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
    public class OfferServiceClient : BaseModel, IOfferServiceClient
    {
        public async Task<string> AddOfferToDistributor(string distributorId, string offerId)
        {
            try
            {
                var filter = Builders<Distributor>.Filter.Eq("_id", ObjectId.Parse(distributorId));
                var update = Builders<Distributor>.Update.Push("Offers", offerId);


                //add offer to distributor 
                UpdateResult result = await DistributorsCollection.UpdateOneAsync(filter, update);

                if (result.IsModifiedCountAvailable)
                {
                    return JsonConvert.SerializeObject(
                           new
                           {
                               status = "success",
                               result = true
                           }
                       );
                }
                else
                {
                    return JsonConvert.SerializeObject(
                            new
                            {
                                status = "fail",
                                result = false,
                                message = "Offer could not be added to Distributor"
                            }
                        );
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(
                            new
                            {
                                status = "fail",
                                result = false,
                                message = "Execption : " + ex.Message
                            }
                        );
            }

        }

        public async Task<string> CreateOffer(Offers offer)
        {
            try
            {
                await OffersCollection.InsertOneAsync(offer);
                return JsonConvert.SerializeObject(
                    new
                    {
                        status = "success",
                        result = true
                    }
               );
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

        public async Task<bool> DeleteOffer(string id)
        {
            try
            {
                var filter = Builders<Offers>.Filter.Eq("_id", ObjectId.Parse(id));

                DeleteResult result = await OffersCollection.DeleteOneAsync(filter);

                if (result.IsAcknowledged)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

           
        }

        public string GetOffer(string id)
        {
            try
            {
               return GetOfferById(id).ToJson();

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

        public List<Offers> GetOffers(string distributorId)
        {
            Distributor distributor = GetDistributorById(distributorId);

            List<Offers> OffersList = new List<Offers>();

            foreach(var offerId in distributor.Offers)
            {
                Offers offer = GetOfferById(offerId);
                OffersList.Add(offer);
            }

            return OffersList;
        }

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

        public async Task<string> AddDiscountToOffer(string offerId, string discountId)
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
                discounts.Add(discount);

                var filter = Builders<Offers>.Filter.Eq("_id", ObjectId.Parse(offerId));
                var update = Builders<Offers>.Update.Set("Discounts", discounts);
                UpdateResult result = await OffersCollection.UpdateOneAsync(filter, update);

                if (result.IsModifiedCountAvailable)
                {
                    return JsonConvert.SerializeObject(
                        new
                        {
                            status = "success",
                            result = true
                        }
                    );
                }
                else
                {
                    return JsonConvert.SerializeObject(
                        new
                        {
                            status = "fail",
                            result = false
                        }
                    );
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

        public async Task<string> RemoveDiscountFromOffer(string offerId, string discountId)
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
                        return JsonConvert.SerializeObject(
                            new
                            {
                                status = "success",
                                result = true
                            }
                        );
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(
                            new
                            {
                                status = "fail",
                                result = false
                            }
                        );
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(
                            new
                            {
                                status = "fail",
                                result = false,
                                message = "Discount does not exist in Offer"
                            }
                        );
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
    }
}
