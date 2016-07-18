using Newtonsoft.Json;

namespace Platform.Client.DataContracts
{
    public class LineDataContract
    {
        public string Id { get; set; }
        [JsonProperty("Bike")]
        public BikeDataContract Bike { get; set; }
        public int Quantity { get; set; }
    }
}