using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Platform.Client.DataContracts
{
    public class DistributorContract
    {

        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Address")]
        public AddressDataContract Address { get; set; }
        [JsonProperty("Contact")]
        public ContactDataContract Contact { get; set; }
        [JsonProperty("Inventory")]
        public List<LineDataContract> Inventory { get; set; }
        //List of offers by ID
        [JsonProperty("")]
        public List<OffersDataContract> Offers { get; set; }
        public List<ReceiptTypeDataContract> ReceiptTpesOffered { get; set; }
    }
}