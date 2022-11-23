using System.Collections.Generic;
using System.Threading.Tasks;
using NutritionApp.Models;
using System.Collections.ObjectModel;

namespace NutritionApp.Services
{
    internal interface IFoodService
    {
        Task<Fixed> GetFoods(string food, string measurment);
    }
}
