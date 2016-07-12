using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace Domain
{
    public class Bike
    {
        [BsonId]
        public string Id { get; set; }
        public Brand Brand { get; set; }
        public BikeModel Model { get; set; }
        
        public Price Cost { get; set; }

        public class AdjustPrice
        {
            public string BikeId { get; set; }
            public Price NewPrice { get; set; }
        }

    }
}
