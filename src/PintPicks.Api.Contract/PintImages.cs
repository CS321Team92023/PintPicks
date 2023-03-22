using Newtonsoft.Json;

namespace PintPicks.Api.Contract
{
    public class PintImages
    {
        [JsonProperty("contentAwareIcon")]
        public string ContentAwareIcon { get; set; }
        [JsonProperty("contentAwareLarge")]
        public string ContentAwareLarge { get; set; }
        [JsonProperty("contentAwareMedium")]
        public string ContentAwareMedium { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("large")]
        public string Large { get; set; }
        [JsonProperty("edium")]
        public string Medium { get; set; }
    }
}
