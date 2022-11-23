using System.Collections.Generic;
using System.Threading.Tasks;
using NutritionApp.Models;
using System.Collections.ObjectModel;

namespace NutritionApp.Services
{
    interface IAccountService
    {
        Task<bool> RegisterAccount(string user, string pass);
        Task<string> getAccounts(string user, string pass);
        string getUsername();

 
    }
}
