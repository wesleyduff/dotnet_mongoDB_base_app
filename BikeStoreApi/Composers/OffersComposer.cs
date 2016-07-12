using Domain;
using MongoDB.Bson;
using Platform.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BikeStoreApi.Composers
{
    public interface IOffersComposer
    {
        string GetListOfOffersForDistributor(string distributorId);
    }

    public class OffersComposer : IOffersComposer
    {
        private readonly IOfferServiceClient _offerServiceClient;

        public OffersComposer(IOfferServiceClient offerServiceClient)
        {
            _offerServiceClient = offerServiceClient;
        }

        public string GetListOfOffersForDistributor(string distributorId)
        {
            List<Offers> List =  _offerServiceClient.GetOffers(distributorId);

            return List.ToJson();
        }


    }
}