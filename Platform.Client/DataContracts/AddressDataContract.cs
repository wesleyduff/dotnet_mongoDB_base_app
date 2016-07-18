using Newtonsoft.Json;

namespace Platform.Client.DataContracts
{
    public class AddressDataContract
    {
        [JsonProperty("StreetAddress")]
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

    }
}