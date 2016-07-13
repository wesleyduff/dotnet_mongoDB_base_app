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
        Task<bool> AddOfferToDistributor(string distributorId, string offerId);
        Task<Offers> CreateOffer(Offers offer);
        Task<string> DeleteOffer(string id);
    }
}