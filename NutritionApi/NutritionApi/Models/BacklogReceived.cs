using System.Text.Json.Serialization;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace NutritionApi.Models
{
    public class BacklogReceived
    {

        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        [JsonPropertyName("Details")]
        public List<Fixed> Details { get; set; }

        [JsonPropertyName("Username")]
        public string Username { get; set; }

        [JsonPropertyName("Date")]
        public string Date { get; set; }


    }
    public class Fixed
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
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
        [ForeignKey(typeof(BacklogReceived))]
        public int BacklogId { get; set; }
    }
    
}
