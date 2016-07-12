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
        Task<Offers> GetOffer(string id);
        Task<Offers> CreateOffer(Offers offer);
        Task<bool> DeleteOffer(string id);
        Task<bool> AddOfferToDistributor(string distributorId, string offerId);
    }
}
