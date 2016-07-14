using Platform.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Platform.Client.Services
{
    public class DistributorsServiceClient : BaseModel, IDistributorsServiceClient
    {
        //TODO:: CREATE - Delete Distributor and its lines

        public async Task<JObject> CreateDistributor(Distributor distributor)
        {
            try
            {
                await DistributorsCollection.InsertOneAsync(distributor);

                return
                  JObject.FromObject(
                  new
                  {
                      status = "success",
                      result = distributor,
                      message = "Distributor Created"
                  }
              );
            } catch (Exception ex)
            {
                return
                  JObject.FromObject(
                  new
                  {
                      status = "Exception Thrown",
                      result = false,
                      message = ex.Message
                  }
              );
            }
        }

        public JObject GetDistributor(string Id)
        {
            try
            {
                var queryable = DistributorsCollection.AsQueryable();
                var query = from p in queryable
                            where p.Id.Equals(ObjectId.Parse(Id))
                            select p;
                Distributor distributor = query.FirstOrDefault();
                return
                   JObject.FromObject(
                   new
                   {
                       status = "success",
                       result = distributor
                   }
               );
            }
            catch(Exception ex)
            {
                return
                   JObject.FromObject(
                   new
                   {
                       status = "Exception Thrown",
                       result = false,
                       message = ex.Message
                   }
               );
            }
           
        }

        public JObject GetDistributors()
        {
            try
            {
                List<Distributor> List = DistributorsCollection.Find(new BsonDocument()).ToList();
                return
                   JObject.FromObject(
                   new
                   {
                       status = "success",
                       result = List
                   }
               );
            }
            catch (Exception ex)
            {
                return
                   JObject.FromObject(
                   new
                   {
                       status = "Exception Thrown",
                       result = false,
                       message = ex.Message
                   }
               );
            }
          
        }

        public async Task<JObject> AddOfferToDistributer(string distributorId, Offers offer)
        {
            try
            {
                Distributor distributor = GetDistributorById(distributorId);

                var offers = distributor.Offers;
                offers.Add(offer);

                var filter = Builders<Distributor>.Filter.Eq("_id", ObjectId.Parse(distributorId));
                var update = Builders<Distributor>.Update.Set("Offers", offers);
                UpdateResult result = await DistributorsCollection.UpdateOneAsync(filter, update);

                if (result.IsModifiedCountAvailable)
                {
                    return
                          JObject.FromObject(
                              new
                              {
                                  status = "success",
                                  result = true,
                                  message = "Offer added to distributor."
                              }
                          );
                }
                else
                {
                    return
                           JObject.FromObject(
                               new
                               {
                                   status = "false",
                                   result = false,
                                   message = "Could not update distributor with new offer."
                               }
                           );
                }
            }
            catch (Exception ex)
            {
                return
                  JObject.FromObject(
                      new
                      {
                          status = "Exception Thrown",
                          result = false,
                          message = ex.Message
                      }
                    );
            }
            

        }



        /* *****************
        --------------------------
        Helper Methods 
        -------------------------
        */
        protected Distributor GetDistributorById(string id)
        {
            try
            {
                var queryable = DistributorsCollection.AsQueryable();
                var query = from p in queryable
                            where p.Id.Equals(ObjectId.Parse(id))
                            select p;
                return query.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
       

    }
}
