using Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BikeStoreApi.Interfaces
{
    public interface IOffersComposer
    {
        JObject GetOffersForDistributor(string distributorId);
        JObject GetOffer(string offerId);
        JObject GetOffers();
        Task<JObject> CreateOffer(Offers offer);
        Task<JObject> DeleteOffer(string id);
        Task<JObject> AddOfferToDistributor(string distributorId, string offerId);
        Task<JObject> AddDiscountToOffer(string offerId, Discount discount);
        Task<JObject> RemoveDiscountFromOffer(string offerId, string discountId);
        Task<JObject> RemoveOfferFromDistributor(string distributorId, string offerId);
    }
}