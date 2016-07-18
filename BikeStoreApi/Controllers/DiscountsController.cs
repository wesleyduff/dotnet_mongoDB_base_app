using BikeStoreApi.Interfaces;
using BikeStoreApi.Properties;
using Domain;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using Platform.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BikeStoreApi.Controllers
{
    public class DiscountsController : ApiController
    {
        private readonly IDiscountComposer _composer;

        public DiscountsController(IDiscountComposer composer)
        {
            _composer = composer;
        }

        public JObject Get()
        {
            return _composer.Get();
        }

        public JObject Get(string id)
        {
            return _composer.Get(id);
        }

        public async Task<JObject> Create(Discount discount)
        {
            return await _composer.Create(discount);
        }

        public async Task<JObject> Delete(string id)
        {
            return await _composer.Delete(id);
        }
    }
}
