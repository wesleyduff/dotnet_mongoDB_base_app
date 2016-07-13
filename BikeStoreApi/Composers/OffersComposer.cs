using Domain;
using MongoDB.Bson;
using Platform.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using BikeStoreApi.Interfaces;

namespace BikeStoreApi.Composers
{

    public class OffersComposer : IOffersComposer
    {
        private readonly IOfferServiceClient _offerServiceClient;

        public OffersComposer(IOfferServiceClient offerServiceClient)
        {
            _offerServiceClient = offerServiceClient;
        }

        public Task<string> AddDiscountToOffer(string offerId, string discountId)
        {
            return _offerServiceClient.AddDiscountToOffer(offerId, discountId);
        }

        public async Task<string> AddOfferToDistributor(string distributorId, string offerId)
        {
            return await _offerServiceClient.AddOfferToDistributor(distributorId, offerId);
        }

        public async Task<string> CreateOffer(Offers offer)
        {
            return await _offerServiceClient.CreateOffer(offer);
        }

        public async Task<string> DeleteOffer(string id)
        {
            bool val = await _offerServiceClient.DeleteOffer(id);
            if (val)
            {
                return JsonConvert.SerializeObject(new
                {
                    Status = "success",
                    Result = true
                });
            } else
            {
                return JsonConvert.SerializeObject(new
                {
                    Status = "fail",
                    Result = false
                });
            }
        }

        public string GetListOfOffersForDistributor(string distributorId)
        {
            List<Offers> List =  _offerServiceClient.GetOffers(distributorId);

            return List.ToJson();
        }

        public string GetOffer(string offerId)
        {
            return _offerServiceClient.GetOffer(offerId);
        }

        public List<Offers> GetOffers(string distributorId)
        {
            return _offerServiceClient.GetOffers(distributorId);
        }

        public Task<string> RemoveDiscountFromOffer(string offerId, string discountId)
        {
            return _offerServiceClient.RemoveDiscountFromOffer(offerId, discountId);
        }
    }
}