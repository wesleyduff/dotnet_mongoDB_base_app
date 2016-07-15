using Platform.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Platform.Client.Services;

namespace Platform.Client.Services
{
    public class LineServiceClient : DistributorsServiceClient, ILineServiceClient
    {
        public LineServiceClient()
        {

        }

        public async Task<JObject> Create(Line line)
        {
            try
            {
                await LineCollection.InsertOneAsync(line);
                return
                  JObject.FromObject(
                      new
                      {
                          status = "success",
                          result = true,
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

        public async Task<JObject> Delete(string lineId)
        {
            try
            {
                var filter = Builders<Line>.Filter.Eq("_id", ObjectId.Parse(lineId));

                DeleteResult result = await LineCollection.DeleteOneAsync(filter);

                if (result.IsAcknowledged)
                {
                    return
                        JObject.FromObject(
                            new
                            {
                                status = "success",
                                result = true,
                                message = "Line was deleted"
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
                                message = "Line could not be deleted"
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

        public JObject GetDistributorsInventory(string distributorId)
        {
            try
            {
              
                var distributor = GetDistributorById(distributorId);

                return
                    JObject.FromObject(
                    new
                    {
                        status = "success",
                        result = distributor.Inventory
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

        public async Task<JObject> Update(UpdateLine postUpdateLine)
        {
            try
            {
                var distributor = GetDistributorById(postUpdateLine.DistributorId);
                var inventory = distributor.Inventory;
                bool updated = false;
                
                inventory.ForEach(delegate (Line line)
                {
                    if (line.Id.Equals(postUpdateLine.OldLineId))
                    {
                        line = postUpdateLine.NewLine;
                        updated = true;
                    }
                });

                if (updated)
                {
                    return await UpdateInventoryList(postUpdateLine.DistributorId, inventory);
                }
                else
                {
                    return
                              JObject.FromObject(
                                  new
                                  {
                                      status = "false",
                                      result = false,
                                      message = "Could not update inventory. Line item in inventory not found."
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

        public async Task<JObject> AddNewLineToInventory(string distributorId, Line line)
        {
            try
            {
                line.Id = ObjectId.GenerateNewId(DateTime.Now).ToString();
                line.Bike.Id = ObjectId.GenerateNewId(DateTime.Now).ToString();
                var queryable = DistributorsCollection.AsQueryable();
                var inventoryQuery = from p in queryable
                                     where p.Id.Equals(ObjectId.Parse(distributorId))
                                     select new { p.Inventory };
                List<Line> inventoryList;
                    inventoryList = inventoryQuery.First().Inventory;
                    if(inventoryList != null)
                    {
                        inventoryList.Add(line);
                    } else
                    {
                        inventoryList = new List<Line>() { line };
                    }
                    
                   
               

                return await this.UpdateInventoryList(distributorId, inventoryList);
            }
            catch (Exception ex)
            {
                return
                              JObject.FromObject(
                                  new
                                  {
                                      status = "false",
                                      result = false,
                                      message = "Could not update inventory. Line item in inventory not found."
                                  }
                              );
            }
          
        }

        public async Task<JObject> AdjustPrice(Bike.AdjustPrice adjustPrice)
        {
            string distributorId = ObjectId.Parse(adjustPrice.DistributorId).ToString();
            string bikeId = ObjectId.Parse(adjustPrice.BikeId).ToString();
            List<Line> inventory = ReturnInventoryForDistributor(distributorId);
            //find the correct bike to adjust price
            foreach (var line in inventory.Where(line => line.Bike.Id == bikeId))
            {
                line.Bike.Price = adjustPrice.NewPrice;
            }

            return await this.UpdateInventoryList(adjustPrice.DistributorId, inventory);

        }


        private async Task<JObject> UpdateInventoryList(string distributorId, List<Line> inventory)
        {
            try
            {
                var filter = Builders<Distributor>.Filter.Eq("_id", ObjectId.Parse(distributorId));
                var update = Builders<Distributor>.Update.Set("Inventory", inventory);
                UpdateResult result = await DistributorsCollection.UpdateOneAsync(filter, update);

                if (result.IsModifiedCountAvailable)
                {
                    return
                           JObject.FromObject(
                               new
                               {
                                   status = "success",
                                   result = inventory,
                                   message = "Inventory Updated"
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
                                    message = "Could not update inventory."
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

        private List<Line> ReturnInventoryForDistributor(string distributorId)
        {
            var queryable = DistributorsCollection.AsQueryable();
            var inventoryQuery = from p in queryable
                                 where p.Id.Equals(ObjectId.Parse(distributorId))
                                 select new { p.Inventory };
            var inventoryList = inventoryQuery.First().Inventory;
            return inventoryList;
        }

        public async Task<JObject> DeleteLineFromDistributor(string distributorId, string bikeId)
        {
            try
            {
                string _distributorId = ObjectId.Parse(distributorId).ToString();
                string _bikeId = ObjectId.Parse(bikeId).ToString();
                List<Line> inventory = ReturnInventoryForDistributor(distributorId);


                //find the correct bike to adjust price
                foreach (var line in inventory.Where(line => line.Bike.Id == _bikeId))
                {
                    inventory.Remove(line);
                    break;
                }

                return await UpdateInventoryList(distributorId, inventory);

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
    }
}
