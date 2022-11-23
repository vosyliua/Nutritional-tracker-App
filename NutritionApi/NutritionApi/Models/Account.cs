using System.Text.Json.Serialization;
using SQLite;
using System;

namespace NutritionApi.Models
{
    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonPropertyName("Username")]
        public string Username { get; set; }

        [JsonPropertyName("Password")]
        public string Password { get; set; }


        [JsonPropertyName("Calories")]
        public double Calories { get; set; }

    }
}