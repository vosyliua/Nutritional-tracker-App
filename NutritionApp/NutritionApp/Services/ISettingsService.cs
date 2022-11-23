using System.Collections.Generic;
using System.Threading.Tasks;
using NutritionApp.Models;
using System.Collections.ObjectModel;

namespace NutritionApp.Services
{
    internal interface ISettingsService
    {
        void calculateCalories(Settings cal, string user);

        Task<bool> RegisterSettings(double calories, string user);
        Task<ReturnedSettings> getSettingsCalories(string username);
        void calculateCaloriesPatch(Settings cal, string user);

        double Calories();
    }
}
