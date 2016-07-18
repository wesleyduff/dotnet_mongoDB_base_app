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
        private readonly IDistributorComposer _composer;

        public DistributorController(IDistributorComposer composer)
        {
            _composer = composer;
        }

        #endregion

        #region CRUD

        public async Task<JObject> Create(Distributor postDistributor)
        {
            return await _composer.Create(postDistributor);
        }

        public JObject Get()
        {
            return _composer.Get();
        }

        public JObject Get(string id)
        {
            return _composer.Get(id);
        }

        public async Task<JObject> Delete(string distributorsId)
        {
            return await _composer.Delete(distributorsId);
        }

        [Route("api/Distributor/UpdateRecieptTypes")]
        [HttpPost]
        public async Task<JObject> UpdateRecieptTypes(UpdateRecieptTypes postUpdate)
        {
            return await _composer.UpdateRecieptTypes(postUpdate);
        }



        #endregion
    }

}
