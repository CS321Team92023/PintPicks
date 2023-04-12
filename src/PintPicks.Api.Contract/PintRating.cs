using Newtonsoft.Json;

namespace PintPicks.Api.Contract
{
    public class PintRating
    {
        [JsonProperty("beer_style")]
        [System.Text.Json.Serialization.JsonPropertyName("beer_style")]
        public string BeerStyle { get; set; }
        [JsonProperty("review_aroma")]
        [System.Text.Json.Serialization.JsonPropertyName("review_aroma")]
        public float ReviewAroma { get; set; }
        [JsonProperty("review_palate")]
        [System.Text.Json.Serialization.JsonPropertyName("review_palate")]
        public float ReviewPalate { get; set; }
        [JsonProperty("beer_name")]
        [System.Text.Json.Serialization.JsonPropertyName("beer_name")]
        public string BeerName { get; set; }
        [JsonProperty("review_overall")]
        [System.Text.Json.Serialization.JsonPropertyName("review_overall")]
        public float ReviewOverall { get; set; }

        [JsonProperty("brewery_name")]
        [System.Text.Json.Serialization.JsonPropertyName("brewery_name")]
        public string BreweryName { get; set; }

        [JsonProperty("review_appearance")]
        [System.Text.Json.Serialization.JsonPropertyName("review_appearance")]
        public float ReviewAppearance { get; set; }

        [JsonProperty("review_time")]
        [System.Text.Json.Serialization.JsonPropertyName("review_time")]
        public float ReviewTime { get; set; }

        [JsonProperty("review_profilename")]
        [System.Text.Json.Serialization.JsonPropertyName("review_profilename")]
        public string ReviewPprofilename { get; set; }

        [JsonProperty("PartitionKey")]
        [System.Text.Json.Serialization.JsonPropertyName("PartitionKey")]
        public string PartitionKey { get; set; }

        [JsonProperty("review_taste")]
        [System.Text.Json.Serialization.JsonPropertyName("review_taste")]
        public float ReviewTaste { get; set; }

    }
}
