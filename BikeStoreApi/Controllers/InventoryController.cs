using Domain;
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
        //todo:: update price for bike - failing on post
        [HttpPost]
        public async Task<JObject> AdjustPrice(Bike.AdjustPrice adjustprice)
        {
            return await _lineServiceClient.AdjustPrice(adjustprice);
        }

        [Route("api/Inventory/NewLine")]
        [HttpPost]
        public IHttpActionResult AddNewLineToInventory(string distributorId, Line line)
        {
            return Ok(_lineServiceClient.AddNewLineToInventory(distributorId, line));
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

        [Route("api/Inventory/NewLine")]
        [HttpPost]
        public IHttpActionResult GetDistributorsInventory(string distributorId)
        {
            return Ok(_lineServiceClient.GetDistributorsInventory(distributorId));
        }


    }
}
