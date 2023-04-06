using Newtonsoft.Json;

namespace PintPicks.Api.Contract
{
    public class PintImages
    {
        [JsonProperty("contentAwareIcon")]
        [System.Text.Json.Serialization.JsonPropertyName("contentAwareIcon")]
        public string ContentAwareIcon { get; set; }
        [JsonProperty("contentAwareLarge")]
        [System.Text.Json.Serialization.JsonPropertyName("contentAwareLarge")]
        public string ContentAwareLarge { get; set; }
        [JsonProperty("contentAwareMedium")]
        [System.Text.Json.Serialization.JsonPropertyName("contentAwareMedium")]
        public string ContentAwareMedium { get; set; }
        [JsonProperty("icon")]
        [System.Text.Json.Serialization.JsonPropertyName("icon")]
        public string Icon { get; set; }
        [JsonProperty("large")]
        [System.Text.Json.Serialization.JsonPropertyName("large")]
        public string Large { get; set; }
        [JsonProperty("medium")]
        [System.Text.Json.Serialization.JsonPropertyName("medium")]
        public string Medium { get; set; }
    }
}
