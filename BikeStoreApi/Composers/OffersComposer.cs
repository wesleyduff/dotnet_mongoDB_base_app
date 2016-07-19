using System;
using System.Threading.Tasks;
using BikeStoreApi.Interfaces;
using Domain;
using Newtonsoft.Json.Linq;
using Platform.Client.Interfaces;

namespace BikeStoreApi.Composers
{

    public class OffersComposer : IOffersComposer
    {

        private readonly IOfferServiceClient _offersServiceClient;


        public OffersComposer(IOfferServiceClient service)
        {
            _offersServiceClient = service;
        }

        public Task<JObject> AddDiscountToOffer(string offerId, Discount discount)
        {
            return _offersServiceClient.AddDiscountToOffer(offerId, discount);
        }

        public Task<JObject> AddOfferToDistributor(string distributorId, string offerId)
        {
            return _offersServiceClient.AddOfferToDistributor(distributorId, offerId);
        }

        public Task<JObject> CreateOffer(Offers offer)
        {
            return _offersServiceClient.CreateOffer(offer);
        }

        public Task<JObject> DeleteOffer(string id)
        {
            return _offersServiceClient.DeleteOffer(id);
        }

        public JObject GetOffer(string offerId)
        {
            return _offersServiceClient.GetOffer(offerId);
        }

        public JObject GetOffers()
        {
            return _offersServiceClient.GetOffers();
        }

        public JObject GetOffersForDistributor(string distributorId)
        {
            return _offersServiceClient.GetOffersForDistributor(distributorId);
        }

        public Task<JObject> RemoveDiscountFromOffer(string offerId, string discountId)
        {
            return _offersServiceClient.RemoveDiscountFromOffer(offerId, discountId);
        }

        public Task<JObject> RemoveOfferFromDistributor(string distributorId, string offerId)
        {
            return _offersServiceClient.RemoveOfferFromDistributor(distributorId, offerId);
        }
    }
}