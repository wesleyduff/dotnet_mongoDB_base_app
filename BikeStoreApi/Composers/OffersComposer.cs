using Domain;
using MongoDB.Bson;
using Platform.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace BikeStoreApi.Composers
{
    public interface IOffersComposer
    {
        string GetListOfOffersForDistributor(string distributorId);
        Task<bool> AddOfferToDistributor(string distributorId, string offerId);
        Task<Offers> CreateOffer(Offers offer);
        Task<string> DeleteOffer(string id);
    }

    public class OffersComposer : IOffersComposer
    {
        private readonly IOfferServiceClient _offerServiceClient;

        public OffersComposer(IOfferServiceClient offerServiceClient)
        {
            _offerServiceClient = offerServiceClient;
        }

        public async Task<bool> AddOfferToDistributor(string distributorId, string offerId)
        {
            return await _offerServiceClient.AddOfferToDistributor(distributorId, offerId);
        }

        public async Task<Offers> CreateOffer(Offers offer)
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


    }
}