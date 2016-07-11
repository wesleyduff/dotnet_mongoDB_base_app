using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Price
    {
        [BsonRepresentation(BsonType.Double)]
        public decimal Value { get; set; }
    }
}
