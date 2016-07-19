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
        public async Task<JObject> Create(Distributor item)
        {
            try
            {
                if(item.Offers == null)
                {
                    item.Offers = new List<Offers>() { };
                }
                if(item.ReceiptTypesOffered == null)
                {
                    item.ReceiptTypesOffered = new List<ReceiptType>() { };
                }
                if (item.Inventory == null)
                {
                    item.Inventory = new List<Line>() { };
                }

                await DistributorsCollection.InsertOneAsync(item);

                return
                  JObject.FromObject(
                  new
                  {
                      status = "success",
                      result = item,
                      message = "Distributor Created"
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

        public JObject Get(ObjectId Id)
        {
            try
            {
                var queryable = DistributorsCollection.AsQueryable();
                var query = from p in queryable
                            where p.Id.Equals(Id)
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

        public JObject GetReceiptTypes()
        {
               
            return
                JObject.FromObject(
                new
                {
                    status = "success",
                    result = new List<ReceiptType>()
                    {
                       new ReceiptType() {RType = ReceiptType.RTypes.FullHtml, RtypeAsString = ReceiptType.RTypes.FullHtml.ToString() },
                       new ReceiptType() {RType = ReceiptType.RTypes.SummaryHtml, RtypeAsString = ReceiptType.RTypes.SummaryHtml.ToString() },
                       new ReceiptType() {RType = ReceiptType.RTypes.Text, RtypeAsString = ReceiptType.RTypes.Text.ToString() }
                    }
                }
            );
           
        }
        public JObject Get()
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

        public async Task<JObject> Delete(string itemId)
        {
            try
            {
                var filter = Builders<Distributor>.Filter.Eq("_id", ObjectId.Parse(itemId));

                DeleteResult result = await DistributorsCollection.DeleteOneAsync(filter);

                if (result.IsAcknowledged)
                {
                    return
                        JObject.FromObject(
                            new
                            {
                                status = "success",
                                result = true,
                                message = "Distributor was deleted"
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
                                message = "Distributor could not be deleted"
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

        public Task<JObject> Update(Distributor postUpdate)
        {
            throw new NotImplementedException();
        }


        public async Task<JObject> UpdateRecieptList(string distributorId, UdateReceiptList postData)
        {
            try
            {
                return await UpdateDistributor(postData.DistributorId, postData.ReceiptList);
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
            };
        }

        public async Task<JObject> UpdateRecieptTypes(UpdateRecieptTypes postUpdate)
        {
            try
            {
                //Get receipt types
                var distributor = GetDistributorById(postUpdate.DistributorId);
                var recieptTypes = distributor.ReceiptTypesOffered;


                bool newReceiptfound = false;
                bool oldRecieptFound = false;

                if (recieptTypes != null)
                {
                    distributor.ReceiptTypesOffered.ForEach(delegate (ReceiptType recieptType)
                    {

                        if (postUpdate.NewReciept != null && postUpdate.NewReciept.RtypeAsString.Equals(recieptType.RtypeAsString))
                        {
                            newReceiptfound = true;
                        }
                        if (postUpdate.OldReciept != null && postUpdate.OldReciept.RtypeAsString.Equals(recieptType.RtypeAsString))
                        {
                            oldRecieptFound = true;
                        }

                    });
                }
                else
                {
                    recieptTypes = new List<ReceiptType>() { };
                }

                // ---------------------------

                if (!newReceiptfound && postUpdate.NewReciept != null)
                {
                    //add receipt to list
                    recieptTypes.Add(
                        new ReceiptType()
                        {
                            RType = postUpdate.NewReciept.RType,
                            RtypeAsString = postUpdate.NewReciept.RtypeAsString
                        });
                   
                }
                else if (oldRecieptFound && postUpdate.OldReciept != null)
                {
                    ReceiptType receiptToRemove = recieptTypes.Find(r => r.RtypeAsString == postUpdate.OldReciept.RtypeAsString);
                    //remove old receipt from list
                    recieptTypes.Remove(receiptToRemove);
                }
               
                else
                {
                    return
                              JObject.FromObject(
                                  new
                                  {
                                      status = "false",
                                      result = false,
                                      message = "Could not update Receipt Types."
                                  }
                              );
                }

                return await UpdateDistributor(postUpdate.DistributorId, recieptTypes);
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

        private async Task<JObject> UpdateDistributor(string distributorId, List<ReceiptType> recieptTypes)
        {
            try
            {
                var filter = Builders<Distributor>.Filter.Eq("_id", ObjectId.Parse(distributorId));
                var update = Builders<Distributor>.Update.Set("ReceiptTypesOffered", recieptTypes);
                UpdateResult result = await DistributorsCollection.UpdateOneAsync(filter, update);

                if (result.IsModifiedCountAvailable)
                {
                    return
                           JObject.FromObject(
                               new
                               {
                                   status = "success",
                                   result = recieptTypes,
                                   message = "Distributor's Receipt Types Updated"
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
                                    message = "Could not add receipt type."
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
                  });
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
