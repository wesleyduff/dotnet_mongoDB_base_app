using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Client.Interfaces
{
    public interface IOfferServiceClient
    {
        List<Offers> GetOffers(string distributorId);
        string GetOffer(string offerId);
        Task<string> CreateOffer(Offers offer);
        Task<bool> DeleteOffer(string id);
        Task<string> AddOfferToDistributor(string distributorId, string offerId);
        Task<string> AddDiscountToOffer(string offerId, string discountId);
        Task<string> RemoveDiscountFromOffer(string offerId, string discountId);
    }
}
