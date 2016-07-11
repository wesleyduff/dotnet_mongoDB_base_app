using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain
{
    public class Bike
    {
        public Brand Brand { get; set; }
        public BikeModel Model { get; set; }
        
        public Price Cost { get; set; }
    }
}
