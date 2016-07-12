using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    public class Offers
    {
        [BsonId]
        public string Id { get; set; }
        public Discount Discount { get; set; }
        public string Title { get; set; }
    }
}