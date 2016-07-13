using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    public class Discount
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public object Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public decimal Percentage { get; set; }
       
    }
}