using Newtonsoft.Json;

namespace PintPicks.Api.Contract
{
    public class PintStyle
    {
        [JsonProperty("name")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonProperty("shortName")]
        [System.Text.Json.Serialization.JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("description")]
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonProperty("abvMax")]
        [System.Text.Json.Serialization.JsonPropertyName("abvMax")]
        public float ABVMax { get; set; }
        [JsonProperty("abvMin")]
        [System.Text.Json.Serialization.JsonPropertyName("abvMin")]
        public float ABVMin { get; set; }

        [JsonProperty("fgMax")]
        [System.Text.Json.Serialization.JsonPropertyName("fgMax")]
        public float FGMax { get; set; }
        [JsonProperty("fgMin")]
        [System.Text.Json.Serialization.JsonPropertyName("fgMin")]
        public float FGMin { get; set; }

        [JsonProperty("ibuMax")]
        [System.Text.Json.Serialization.JsonPropertyName("ibuMax")]
        public float IBUMax { get; set; }
        [JsonProperty("ibuMin")]
        [System.Text.Json.Serialization.JsonPropertyName("ibuMin")]
        public float IBUMin { get; set; }

        [JsonProperty("srmMax")]
        [System.Text.Json.Serialization.JsonPropertyName("srmMax")]
        public float SRMMax { get; set; }
        [JsonProperty("srmMin")]
        [System.Text.Json.Serialization.JsonPropertyName("srmMin")]
        public float SRMMin { get; set; }
    }
}
