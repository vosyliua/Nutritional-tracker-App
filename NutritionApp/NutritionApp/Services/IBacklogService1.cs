using System.Collections.Generic;
using System.Threading.Tasks;
using NutritionApp.Models;
using System.Collections.ObjectModel;

namespace NutritionApp.Services
{
    internal interface IBacklogService1
    {
        Task<bool> addData(ObservableCollection<Fixed> backlog, string username);
        Task<List<Backlog>> returnData(string username);
    }
}
