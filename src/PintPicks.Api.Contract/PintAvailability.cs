using Newtonsoft.Json;

namespace PintPicks.Api.Contract
{
    public class PintAvailability
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
