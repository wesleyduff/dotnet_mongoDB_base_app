using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Line
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Bike Bike { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateLine
    {
        public Line NewLine { get; set; }
        public string DistributorId { get; set; }
        public string OldLineId { get; set; }
    }
}
