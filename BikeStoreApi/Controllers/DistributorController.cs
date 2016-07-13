using System.Web.Http;
using System.Collections.Generic;
using BikeStoreApi.Interfaces;
using BikeStoreApi.Models;
using System.Threading.Tasks;
using Domain;
using Ninject.Modules;
using System;
using BikeStoreApi.Composers;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Web.Http.Results;

namespace BikeStoreApi.Controllers
{
    public class DistributorController : ApiController
    {
        

        #region Constructor

        private readonly IDistributorComposer distributorsComposer;

        public DistributorController(IDistributorComposer distributorsComposer)
        {
            this.distributorsComposer = distributorsComposer;
        }

        #endregion





        #region Get All Distributors
        /* 
            GET api/distributor/GetDistributors
        */
        [Route("api/Distributors/All")]
        [HttpGet]
        public async Task<string> All()
        {
            var model = await distributorsComposer.GetDistributorsList();
            return model;
        }

        public async Task<string> Create(Distributor postDistributor)
        {
            var distributor = new Distributor()
            {
                Address = postDistributor.Address,
                Contact = postDistributor.Contact,
                Inventory = postDistributor.Inventory,
                Name = postDistributor.Name
            };

            try
            {
                var model = await distributorsComposer.CreateDistributor(distributor);
                return model.ToJson();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return JsonConvert.SerializeObject(new { Status = false } );
            }
            
        }

        public async Task<string> Get()
        {
            try
            {
                var model = await distributorsComposer.GetDistributorsList();
                return model.ToJson();
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Status = false });
            }
        }

        public string Get(string id)
        {
            try
            {
                var model = distributorsComposer.GetDistributor(id);
                return model.ToJson();
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Status = false });
            }
        }

        [Route("api/Distributors/AddInventory")]
        [HttpPost]
        public string AddInventory(string id, Bike bike)
        {
            try
            {
                var model = distributorsComposer.AddProductToInventory(id, bike);
                return model.ToJson();
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Status = false });
            }
        }

        [Route("api/Bike/AdjustPrice")]
        [HttpPost]
        public string AdjustPrice(string id, Bike.AdjustPrice adjustprice)
        {
            try
            {
                var model = distributorsComposer.AdjustPrice(id, adjustprice);
                model.ToJson();
                return JsonConvert.SerializeObject(new { Status = true });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Status = false });
            }
        }


        #endregion
    }

}
