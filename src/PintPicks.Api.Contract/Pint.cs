using Newtonsoft.Json;

namespace PintPicks.Api.Contract
{
    public class Pint
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameDisplay")]
        public string NameDisplay { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("abv")]
        public float ABV { get; set; }

        [JsonProperty("available")]
        public PintAvailability Available { get; set; }

        [JsonProperty("ibu")]
        public float IBU { get; set; }

        [JsonProperty("isOrganic")]
        [System.Text.Json.Serialization.JsonConverter(typeof(YesNoBoolConverter))]
        [JsonConverter(typeof(YesNoBoolNewtonsoftConverter))]
        public bool IsOrganic { get; set; }

        [JsonProperty("isRetired")]
        [System.Text.Json.Serialization.JsonConverter(typeof(YesNoBoolConverter))]
        [JsonConverter(typeof(YesNoBoolNewtonsoftConverter))]
        public bool IsRetired { get; set; }

        [JsonProperty("labels")]
        public PintImages Images { get; set; }

        [JsonProperty("style")]
        public PintStyle Style { get; set; }
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
