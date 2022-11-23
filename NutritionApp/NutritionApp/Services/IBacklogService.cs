using System.Collections.Generic;
using System.Threading.Tasks;
using NutritionApp.Models;
using System.Collections.ObjectModel;

namespace NutritionApp.Services
{
    internal interface IBacklogService
    {
        ObservableCollection<Backlog> getBacklogs();
    }
}
