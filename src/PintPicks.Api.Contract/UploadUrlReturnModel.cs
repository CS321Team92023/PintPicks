using Newtonsoft.Json;

namespace PintPicks.Api.Contract
{
    public class UploadUrlReturnModel
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
