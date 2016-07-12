using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BikeStoreApi.Composers;

namespace BikeStoreApi.Controllers
{
    public class OffersController : ApiController
    {
        private readonly IOffersComposer _offersComposer;
     
        public OffersController(IOffersComposer offersComposer)
        {
            _offersComposer = offersComposer;
        }

        /* 
           GET api/distributor/GetDistributors
       */
        [Route("api/Distributor/{id}/Offers")]
        [HttpGet]
        public string Ditributor(string id)
        {
            var model = _offersComposer.GetListOfOffersForDistributor(id);
            return model;
        }
    }
}
