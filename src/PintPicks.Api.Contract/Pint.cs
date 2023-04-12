using Newtonsoft.Json;

namespace PintPicks.Api.Contract
{
    public class Pint
    {
        [JsonProperty("name")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("nameDisplay")]
        [System.Text.Json.Serialization.JsonPropertyName("nameDisplay")]
        public string NameDisplay { get; set; }

        [JsonProperty("description")]
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonProperty("abv")]
        [System.Text.Json.Serialization.JsonPropertyName("abv")]
        public float ABV { get; set; }

        [JsonProperty("available")]
        [System.Text.Json.Serialization.JsonPropertyName("available")]
        public PintAvailability Available { get; set; }

        [JsonProperty("ibu")]
        [System.Text.Json.Serialization.JsonPropertyName("ibu")]
        public float IBU { get; set; }

        [JsonProperty("isOrganic")]
        [System.Text.Json.Serialization.JsonPropertyName("isOrganic")]
        [System.Text.Json.Serialization.JsonConverter(typeof(YesNoBoolConverter))]
        [JsonConverter(typeof(YesNoBoolNewtonsoftConverter))]
        public bool IsOrganic { get; set; }

        [JsonProperty("isRetired")]
        [System.Text.Json.Serialization.JsonPropertyName("isRetired")]
        [System.Text.Json.Serialization.JsonConverter(typeof(YesNoBoolConverter))]
        [JsonConverter(typeof(YesNoBoolNewtonsoftConverter))]
        public bool IsRetired { get; set; }

        [JsonProperty("labels")]
        [System.Text.Json.Serialization.JsonPropertyName("labels")]
        public PintImages Images { get; set; }

        [JsonProperty("style")]
        [System.Text.Json.Serialization.JsonPropertyName("style")]
        public PintStyle Style { get; set; }

        [JsonProperty("ratings")]
        [System.Text.Json.Serialization.JsonPropertyName("ratings")]
        public IEnumerable<PintRating> Ratings { get; set; }
    }

    public class YesNoBoolConverter : System.Text.Json.Serialization.JsonConverter<bool>
    {
        public override void Write(System.Text.Json.Utf8JsonWriter writer, bool value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value ? "Y" : "N");
        }

        public override bool Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return !string.IsNullOrWhiteSpace(value) && value.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

    }

    public class YesNoBoolNewtonsoftConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var boolVal = (bool)value;
            writer.WriteValue(boolVal ? "Y" : "N");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();
            return !string.IsNullOrWhiteSpace(value) && value.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
