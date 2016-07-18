using Newtonsoft.Json;

namespace Platform.Client.DataContracts
{
    public class ReceiptTypeDataContract
    {
        [JsonProperty("RType")]
        public int RType { get; set; }
        [JsonProperty("RTypeAsString")]
        public string RtypeAsString { get; set; }
    }
}