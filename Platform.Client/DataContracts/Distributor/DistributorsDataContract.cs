using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Platform.Client.DataContracts.Distributor
{
    public class DistributorsDataContract
    {
        [JsonProperty("distributors")]
        public List<DistributorContract> Distributors;
    }
}