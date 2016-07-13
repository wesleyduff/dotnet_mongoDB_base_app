using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BikeStoreApi.Interfaces
{
    public interface IOffersComposer
    {
        string GetListOfOffersForDistributor(string distributorId);
        Task<string> AddOfferToDistributor(string distributorId, string offerId);
        Task<string> CreateOffer(Offers offer);
        Task<string> DeleteOffer(string id);
        Task<string> RemoveDiscountFromOffer(string offerId, string discountId);
        List<Offers> GetOffers(string distributorId);
        string GetOffer(string id);
        Task<string> AddDiscountToOffer(string offerId, string discountId);
    }
}