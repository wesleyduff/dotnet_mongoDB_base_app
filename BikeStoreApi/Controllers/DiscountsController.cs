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
        private readonly IDiscountServiceClient _discountServiceClient;

        public DiscountsController(IDiscountServiceClient discountServiceClient)
        {
            _discountServiceClient = discountServiceClient;
        }

        public IHttpActionResult Get()
        {
            return Ok(_discountServiceClient.GetDiscounts());
        }

        public IHttpActionResult Get(string id)
        {
            return Ok(_discountServiceClient.GetDiscount(id));
        }

        public async Task<JObject> Create(Discount discount)
        {
            discount.Id = ObjectId.GenerateNewId(DateTime.Now).ToString();
            return await _discountServiceClient.CreateDiscount(discount);
        }

        public async Task<JObject> Delete(string id)
        {
            return await _discountServiceClient.DeleteDiscount(id);

        }
    }
}
