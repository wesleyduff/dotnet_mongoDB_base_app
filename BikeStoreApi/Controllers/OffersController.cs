using System.Web.Http;
using Domain;
using System.Threading.Tasks;
using Platform.Client.Interfaces;
using Newtonsoft.Json.Linq;
using BikeStoreApi.Interfaces;

namespace BikeStoreApi.Controllers
{
    public class OffersController : ApiController
    {
        private readonly IOfferServiceClient _offerServiceClient;
        private readonly IOffersComposer _offerComposer;
     
        public OffersController(IOffersComposer offerComposer)
        {
            _offerComposer = offerComposer;
        }

        [Route("api/Distributor/{id}/Offers")]
        [HttpGet]
        public JObject GetOffersForDistributor(string id)
        {
            return _offerComposer.GetOffersForDistributor(id);
        }

        public JObject Get()
        {
            return _offerComposer.GetOffers();
        }

        /* 
          GET api/distributor/GetDistributors
      */
        [HttpPost]
        public async Task<JObject> create(Offers offer)
        {
            return await _offerComposer.CreateOffer(offer);
        }

        public async Task<JObject> delete(string id)
        {
            return await _offerComposer.DeleteOffer(id);
        }


        public JObject Get(string offerId)
        {
            return _offerComposer.GetOffer(offerId);
        }

        [Route("api/Offers/{offerId}/AddDiscount/{discountId}")]
        [HttpGet]
        public async Task<JObject> AddDiscountToOffer(string offerId, Discount discount)
        {
            return await _offerComposer.AddDiscountToOffer(offerId, discount);
        }
        /**
        Remove Discount from Offer
        */
        [Route("api/Offers/{offerId}/RemoveOffer/{discountId}")]
        [HttpGet]
        public async Task<JObject> RemoveDiscountFromOffer(string offerId, string discountId)
        {
            return await _offerComposer.RemoveDiscountFromOffer(offerId, discountId);
        }


        [Route("api/Distributor/{distributorId}/AddOffer/{offerId}")]
        [HttpGet]
        public async Task<JObject> AddOfferToDistributor(string distributorId, string offerId)
        {
            return await _offerComposer.AddOfferToDistributor(distributorId, offerId);
        }

        [Route("api/Distributor/{distributorId}/RemoveOffer/{offerId}")]
        [HttpGet]
        public async Task<JObject> RemoveOfferToDistributor(string distributorId, string offerId)
        {
            return await _offerComposer.RemoveOfferFromDistributor(distributorId, offerId);
        }

    }
}
