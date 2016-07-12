using Newtonsoft.Json;

namespace Platform.Client.DataContracts.Offers
{
    public class DiscountContract
    {
        [JsonProperty("title")]
        public string title { get; set; }
        [JsonProperty("code")]
        public string code { get; set; }
        [JsonProperty("percentage")]
        public string percentage { get; set; }
    }
}