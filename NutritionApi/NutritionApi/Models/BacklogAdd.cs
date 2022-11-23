using System.Text.Json.Serialization;
using SQLite;
using System;

namespace NutritionApi.Models
{
    public class BacklogAdd
    {



        [JsonPropertyName("Id")]
        public int id { get; set; }

        [JsonPropertyName("Username")]
        public string Username { get; set; }

        [JsonPropertyName("Date")]
        public string Date { get; set; }

        public string BacklogsBlobbed { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Calories")]
        public double Calories { get; set; }

        [JsonPropertyName("Fat")]
        public double Fat { get; set; }

        [JsonPropertyName("Protein")]
        public double Protein { get; set; }

        [JsonPropertyName("Carbohydrates")]
        public double Carbohydrates { get; set; }

        [JsonPropertyName("Image")]
        public string Image { get; set; }

        [JsonPropertyName("CaloriesEaten")]

        public string CaloriesEaten { get; set; }

        [JsonPropertyName("CaloriesEatenText")]
        public string CaloriesEatenText { get; set; }

    }
}
