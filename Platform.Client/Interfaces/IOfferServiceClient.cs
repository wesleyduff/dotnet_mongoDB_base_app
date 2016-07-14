using Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Client.Interfaces
{
    public interface IOfferServiceClient
    {
        JObject GetOffersForDistributor(string distributorId);
        JObject GetOffer(string offerId);
        JObject GetOffers();
        Task<JObject> CreateOffer(Offers offer);
        Task<JObject> DeleteOffer(string id);
        Task<JObject> AddOfferToDistributor(string distributorId, Offers offer);
        Task<JObject> AddDiscountToOffer(string offerId, Discount discount);
        Task<JObject> RemoveDiscountFromOffer(string offerId, string discountId);
    }
}
