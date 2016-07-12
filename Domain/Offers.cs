using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    public class Offers
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Discount Discount { get; set; }
        public string Title { get; set; }
    }
}