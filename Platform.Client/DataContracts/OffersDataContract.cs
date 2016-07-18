using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Client.DataContracts
{
    public class OffersDataContract
    {
        [JsonProperty("offers")]
        public List<OffersContract> offers;
    }
}
