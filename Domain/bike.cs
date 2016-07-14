using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace Domain
{
    public class Bike
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Brand Brand { get; set; }
        public BikeModel Model { get; set; }
        
        public Price Price { get; set; }

        public class AdjustPrice
        {
            [BsonId]
            public string DistributorId { get; set; }
            [BsonId]
            public string BikeId { get; set; }
            public Price NewPrice { get; set; }
        }

    }
}
