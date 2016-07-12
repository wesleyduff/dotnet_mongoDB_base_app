using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    public class Distributor
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
     
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public List<Bike> Inventory { get; set; }
        //List of offers by ID
        public List<string> Offers { get; set; }
        public List<ReceiptType> ReceiptTpesOffered { get; set; }
    }

}
