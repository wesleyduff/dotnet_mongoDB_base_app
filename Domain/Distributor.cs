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
        public List<Line> Inventory { get; set; }
        //List of offers by ID
        public List<Offers> Offers { get; set; }
        public List<ReceiptType> ReceiptTypesOffered { get; set; }
    }

    public class UpdateRecieptTypes
    {
        public ReceiptType NewReciept { get; set; }
        public ReceiptType OldReciept { get; set; }
        public string DistributorId { get; set; }

    }

    public class UdateReceiptList
    {
       public  List<ReceiptType> ReceiptList { get; set; }
        public string DistributorId { get; set; }
    }

}
