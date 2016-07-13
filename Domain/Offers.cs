using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Domain
{
    public class Offers
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<Discount> Discounts { get; set; }
        public string Title { get; set; }
    }
}