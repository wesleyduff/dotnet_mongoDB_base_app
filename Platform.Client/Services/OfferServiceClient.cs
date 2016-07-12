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

        public Task<bool> DeleteOffer(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Offers> GetOffer(string id)
        {
            throw new NotImplementedException();
        }

        public List<Offers> GetOffers(string distributorId)
        {
            var queryable = DistributorsCollection.AsQueryable();
            var query = from p in queryable
                        where p.Id.Equals(ObjectId.Parse(distributorId))
                        select p;

            List<Offers> OffersList = new List<Offers>();

            foreach(var offerId in query.FirstOrDefault().Offers)
            {
                var queryableOffer = OffersCollection.AsQueryable();
                var queryOffer = from o in queryableOffer
                                 where o.Id.Equals(offerId)
                                 select o;
                OffersList.Add(queryOffer.First());
            }

            return OffersList;

            
        }
    }
}
