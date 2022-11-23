using SQLite;
using System;
using System.Text.Json.Serialization;

namespace NutritionApi.Models;

public class Settingss
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [JsonPropertyName("Username")]
    public string Username { get; set; }

    [JsonPropertyName("Calories")]
    public double Calories { get; set; }





}



