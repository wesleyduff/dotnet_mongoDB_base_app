using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Platform.Client.DataContracts
{
    public class DistributorsDataContract
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("result")]
        public List<DistributorContract> Result { get; set; }
    }
}