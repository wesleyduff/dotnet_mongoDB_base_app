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
using System.Web.Http.Cors;
using Platform.Client.Interfaces;
using Newtonsoft.Json.Linq;

namespace BikeStoreApi.Controllers
{
    public class OffersController : ApiController
    {
        private readonly IOfferServiceClient _offerServiceClient;
     
        public OffersController(IOfferServiceClient offerServiceClient)
        {
            _offerServiceClient = offerServiceClient;
        }

        [Route("api/Distributor/{id}/Offers")]
        [HttpGet]
        public IHttpActionResult GetOffersForDistributor(string id)
        {
            return Ok(_offerServiceClient.GetOffersForDistributor(id));
        }

        public IHttpActionResult Get()
        {
            return Ok(_offerServiceClient.GetOffers());
        }

        /* 
          GET api/distributor/GetDistributors
      */
        [HttpPost]
        public async Task<JObject> create(Offers offer)
        {
            return await _offerServiceClient.CreateOffer(offer);
        }

        public async Task<JObject> delete(string id)
        {
            return await _offerServiceClient.DeleteOffer(id);
        }


        public IHttpActionResult Get(string offerId)
        {
            return Ok(_offerServiceClient.GetOffer(offerId));
        }

        [Route("api/Offers/{offerId}/AddDiscount/{discountId}")]
        [HttpGet]
        public async Task<JObject> AddDiscountToOffer(string offerId, Discount discount)
        {
            return await _offerServiceClient.AddDiscountToOffer(offerId, discount);
        }
        /**
        Remove Discount from Offer
        */
        [Route("api/Offers/{offerId}/RemoveOffer/{discountId}")]
        [HttpGet]
        public IHttpActionResult RemoveDiscountFromOffer(string offerId, string discountId)
        {
            return Ok(_offerServiceClient.RemoveDiscountFromOffer(offerId, discountId));
        }


        [Route("api/Distributor/{distributorId}/AddOffer/{offerId}")]
        [HttpGet]
        public IHttpActionResult AddOfferToDistributor(string distributorId, Offers offer)
        {
            return Ok(_offerServiceClient.AddOfferToDistributor(distributorId, offer));
        }

    }
}
