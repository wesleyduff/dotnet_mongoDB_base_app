using BikeStoreApi.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Platform.Client.Interfaces;
using Newtonsoft.Json.Linq;

namespace BikeStoreApi.Interfaces
{
    public interface IDistributorComposer : ICrud<Distributor, string>
    {
        Task<JObject> UpdateRecieptTypes(UpdateRecieptTypes postUpdate);
    }
}