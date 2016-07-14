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
using System.Web.Http.Cors;
using Platform.Client.Interfaces;
using Newtonsoft.Json.Linq;

namespace BikeStoreApi.Controllers
{
    public class DistributorController : ApiController
    {
        

        #region Constructor

        private readonly IDistributorsServiceClient _distributorsServiceClient;

        public DistributorController(IDistributorsServiceClient distributorsServiceClient)
        {
            _distributorsServiceClient = distributorsServiceClient;
        }

        #endregion





        #region Get All Distributors

        public async Task<JObject> Create(Distributor postDistributor)
        {
            return await _distributorsServiceClient.CreateDistributor(postDistributor);
        }

        public JObject Get()
        {
            return  _distributorsServiceClient.GetDistributors();
        }

        public IHttpActionResult Get(string id)
        {
            return Ok(_distributorsServiceClient.GetDistributor(id));
        }
        



        #endregion
    }

}
