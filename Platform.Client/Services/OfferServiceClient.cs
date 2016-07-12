using Platform.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Platform.Client.Services
{
    public class OfferServiceClient : BaseModel, IOfferServiceClient
    {
        public async Task<bool> AddOfferToDistributor(string distributorId, string offerId)
        {
            var filter = Builders<Distributor>.Filter.Eq("_id", ObjectId.Parse(distributorId));
            var update = Builders<Distributor>.Update.Push("Offers", offerId);


            //add offer to distributor 
            UpdateResult result = await DistributorsCollection.UpdateOneAsync(filter, update);

            if (result.IsModifiedCountAvailable)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<Offers> CreateOffer(Offers offer)
        {
            try
            {
                await OffersCollection.InsertOneAsync(offer);
                return offer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
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

        public Task<Offers> GetOffer(string id)
        {
            throw new NotImplementedException();
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
    }
}
