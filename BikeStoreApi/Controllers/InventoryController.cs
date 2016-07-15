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

namespace BikeStoreApi.Controllers
{
    public class InventoryController : ApiController
    {
        ILineServiceClient _lineServiceClient;
        public InventoryController(ILineServiceClient lineServiceClient)
        {
            _lineServiceClient = lineServiceClient;
        }

        [Route("api/Inventory/AdjustPrice")]
        [HttpPost]
        public async Task<JObject> AdjustPrice(Bike.AdjustPrice adjustprice)
        {
            return await _lineServiceClient.AdjustPrice(adjustprice);
        }

        [Route("api/Inventory/NewLine")]
        [HttpPost]
        public async Task<JObject> AddNewLineToInventory(string distributorId, Line line)
        {
            return await _lineServiceClient.AddNewLineToInventory(distributorId, line);
        }

        [Route("api/Distributor/Bike")]
        [HttpGet]
        public async Task<JObject> DeleteLineFromDistributor(string distributorId, string bikeId)
        {
            return await _lineServiceClient.DeleteLineFromDistributor(distributorId, bikeId);
        }
        


        public IHttpActionResult Update(UpdateLine postUpdateLine)
        {
            return Ok(_lineServiceClient.Update(postUpdateLine));
        }

        public IHttpActionResult delete(string lineId)
        {
            return Ok(_lineServiceClient.Delete(lineId));
        }

        public IHttpActionResult Create(Line line)
        {
            return Ok(_lineServiceClient.Create(line));
        }

        [Route("api/Distributor/Inventory")]
        [HttpPost]
        public IHttpActionResult GetDistributorsInventory(string distributorId)
        {
            return Ok(_lineServiceClient.GetDistributorsInventory(distributorId));
        }


    }
}
