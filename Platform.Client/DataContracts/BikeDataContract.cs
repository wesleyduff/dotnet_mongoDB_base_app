using Newtonsoft.Json;

namespace Platform.Client.DataContracts
{
    public class BikeDataContract
    {
        public string Id { get; set; }
        [JsonProperty("Brand")]
        public BrandDataContract Brand { get; set; }
        [JsonProperty("Model")]
        public BikeModelDataContract Model { get; set; }
        [JsonProperty("Price")]
        public PriceDataContract Price { get; set; }
    }
}