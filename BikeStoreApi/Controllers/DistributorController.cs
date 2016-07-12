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
        #region Products
        BikeModels[] products = new BikeModels[]
        {
            new BikeModels
            {
                Brand = new Brand
                {
                    Name = "Yeti"
                },
                Cost = new Price
                {
                    Value = 2900.00M
                },
                Model = new BikeModel
                {
                    Name = "ASRC",
                    Year = new System.DateTime(2016, 11, 01),
                    Features = new List<Feature>()
                    {
                        new Feature
                        {
                             Description = "High modulus carbon fiber main frame and swing arm. Designed for a high life span",
                             Title = "High modulus carbon fiber main frame and swing arm"
                        },
                        new Feature
                        {
                            Description = "Signature Yeti \"loopstays\"",
                            Title = "Loopstays"
                        },
                        new Feature
                        {
                            Description = "Carbon Link",
                            Title = "Carbon Link"
                        }
                    }
                }
            },
            new BikeModels
            {
                Brand = new Brand
                {
                    Name = "Yeti"
                },
                Cost = new Price
                {
                    Value = 3900.00M
                },
                Model = new BikeModel
                {
                    Name = "SB6c",
                    Year = new System.DateTime(2016, 11, 01),
                    Description = "The SB6c was built to excel on the toughest enduro tracks in the world and has earned an EWS World Championship title under factory rider Richie Rude. Although it screams gravity, this bike is light, pedals well, and is tons of fun on the lifts or grinding out big days in the backcountry.",
                    Features = new List<Feature>()
                    {
                        new Feature
                        {
                             Description = "High modulus carbon fiber main frame and swing arm. Designed for a high life span",
                             Title = "High modulus carbon fiber main frame and swing arm"
                        },
                        new Feature
                        {
                            Description = "Switch infinity technology patented suspension system",
                            Title = "Switch infinity suspension system"
                        },
                        new Feature
                        {
                            Description = "Integraged ISCG Mounts",
                            Title = "ISCG Mounts"
                        }
                    }
                }
            },
            new BikeModels
            {
                Brand = new Brand
                {
                    Name = "Niner"
                },
                Cost = new Price
                {
                    Value = 2650.00M
                },
                Model = new BikeModel
                {
                    Name = "Jet 9 RDO",
                    Year = new System.DateTime(2015, 09, 08),
                    Description = "The all new JET anchors the [R]Evolution of our trail bikes. Knowing that technologies and rider expectations have changed over the years, we’ve put a lot of time, energy and research into creating the most versatile and fun full suspension mountain bike money can buy.",
                    Features = new List<Feature>()
                    {
                        new Feature
                        {
                             Description = "All new, completely redesigned frame uses RDO Carbon Compaction to produce the ultimate, lightweight trail worthy machine",
                             Title = "New Frame"
                        },
                        new Feature
                        {
                            Description = "Niner's patented CVA suspension design delivers 120mm of plush, efficient rear travel",
                            Title = "120mm Rear Travel"
                        },
                        new Feature
                        {
                            Description = "Revised geometry is optimized for modern trail riding Boost 148x12mm rear spacing ",
                            Title = "Larger Rear Spacing"
                        }
                    }
                }
            },
             new BikeModels
            {
                Brand = new Brand
                {
                    Name = "Niner"
                },
                Cost = new Price
                {
                    Value = 2650.00M
                },
                Model = new BikeModel
                {
                    Name = "Fuel",
                    Year = new System.DateTime(2015, 05, 18),
                    Description = "The world's best-loved mountain bike keeps getting better. Fuel EX continues to set the bar for full suspension trail bike versatility, bringing race-day tech to all-day adventure.",
                    Features = new List<Feature>()
                    {
                        new Feature
                        {
                             Description = "Full OCLV Mountain Carbon chainstay reduces frame weight by about 100g and dramtically increases stiffness.",
                             Title = "OCLV Mountain Chainstay"
                        },
                        new Feature
                        {
                            Description = "Advanced frame geometry and a custom offset fork give our 29ers precise handling at low speed without the compromising high-spped stability. We cal lit G2 Geometry, and it's why our 29ers handle better than anyone's",
                            Title = "G2 Geometry"
                        },
                        new Feature
                        {
                            Description = "Sometimes you need a little extra protection, so we developed our light, tough external Carbon Armor polymer shield. It provides ressurance when you know your down tube is bound to take some abuse.",
                            Title = "Carbon Armor"
                        }
                    }
                }
            }
        };

        #endregion




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
