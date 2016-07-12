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
        public string create (Offers offer)
        {
            var model = _offersComposer.CreateOffer(offer);
            return model.ToJson();
        }

        public async Task<string> delete(string id)
        {
            var model = await _offersComposer.DeleteOffer(id);
            return model;
        }
    }
}
