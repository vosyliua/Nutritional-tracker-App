using System;
using System.Collections.Generic;

namespace NutritionApp.Models
{
    public class Fixed
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }
        public double Carbohydrates { get; set; }
        public string Image { get; set; }
        public string CaloriesEaten { get; set; }
        public string CaloriesEatenText { get; set; }
        public int backlogId { get; set; }
    }

    public class Backlog
    {
        public int id { get; set; }
        public List<Fixed> Details { get; set; }
        public string Username { get; set; }
        public string Date { get; set; }
    }
}