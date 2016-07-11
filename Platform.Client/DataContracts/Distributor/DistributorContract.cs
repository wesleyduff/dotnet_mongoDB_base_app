using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Platform.Client.DataContracts.Distributor
{
    public class DistributorContract
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}