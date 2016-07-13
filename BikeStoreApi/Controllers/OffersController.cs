using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BikeStoreApi.Composers;
using Domain;
using MongoDB.Bson;
using System.Threading.Tasks;
using BikeStoreApi.Interfaces;

namespace BikeStoreApi.Controllers
{
    public class OffersController : ApiController
    {
        private readonly IOffersComposer _offersComposer;
     
        public OffersController(IOffersComposer offersComposer)
        {
            _offersComposer = offersComposer;
        }

        [Route("api/Distributor/{id}/Offers")]
        [HttpGet]
        public string Ditributor(string id)
        {
            var model = _offersComposer.GetListOfOffersForDistributor(id);
            return model;
        }

        /* 
          GET api/distributor/GetDistributors
      */
        [HttpPost]
        public async Task<string> create(Offers offer)
        {
            return await _offersComposer.CreateOffer(offer);
        }

        public async Task<string> delete(string id)
        {
            var model = await _offersComposer.DeleteOffer(id);
            return model;
        }


        public string Get(string offerId)
        {
            return _offersComposer.GetOffer(offerId);
        }

        [Route("api/Offers/{offerId}/AddDiscount/{discountId}")]
        [HttpGet]
        public async Task<string> AddDiscountToOffer(string offerId, string discountId)
        {
            return await _offersComposer.AddDiscountToOffer(offerId, discountId);
        }
        /**
        Remove Discount from Offer
        */
        [Route("api/Offers/{offerId}/{discountId}")]
        [HttpGet]
        public async Task<string> RemoveDiscountFromOffer(string offerId, string discountId)
        {
            return await _offersComposer.RemoveDiscountFromOffer(offerId, discountId);
        }


        [Route("api/Distributor/{distributorId}/AddOffer/{offerId}")]
        [HttpGet]
        public async Task<string> AddOfferToDistributor(string distributorId, string offerId)
        {
            return await _offersComposer.AddOfferToDistributor(distributorId, offerId);
        }

    }
}
