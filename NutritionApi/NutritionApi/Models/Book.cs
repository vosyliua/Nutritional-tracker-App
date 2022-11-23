using System.Text.Json.Serialization;

namespace NutritionApi.Models
{
    public class Book
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("Author")]
        public string Author { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }
}