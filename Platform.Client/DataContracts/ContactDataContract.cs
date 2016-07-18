using Newtonsoft.Json;

namespace Platform.Client.DataContracts
{
    public class ContactDataContract
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }

    }
}