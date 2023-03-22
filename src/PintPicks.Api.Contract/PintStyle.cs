using Newtonsoft.Json;

namespace PintPicks.Api.Contract
{
    public class PintStyle
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("abvMax")]
        public float ABVMax { get; set; }
        [JsonProperty("abvMin")]
        public float ABVMin { get; set; }

        [JsonProperty("fgMax")]
        public float FGMax { get; set; }
        [JsonProperty("fgMin")]
        public float FGMin { get; set; }

        [JsonProperty("ibuMax")]
        public float IBUMax { get; set; }
        [JsonProperty("ibuMin")]
        public float IBUMin { get; set; }

        [JsonProperty("srmMax")]
        public float SRMMax { get; set; }
        [JsonProperty("srmMin")]
        public float SRMMin { get; set; }
    }
}
