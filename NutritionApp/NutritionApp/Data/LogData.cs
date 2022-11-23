using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using NutritionApp.Models;
using NutritionApp.Services;
using System.Linq;
using System.Collections.Generic;
using NutritionApp.ViewModels;


namespace NutritionApp.Data
{
    public class LogData : IBacklogService
    {
        public ObservableCollection<Backlog> Bcklogs { get; set; }
        public ObservableCollection <Backlog> getBacklogs()
        {
            
            MessagingCenter.Subscribe<BackLogsPageViewModel, ObservableCollection<Backlog>>(this, "tack", (sender, backlog) =>
            {
                foreach(Backlog item in backlog)
                {
                    Bcklogs.Add(item);
                }
                
            });
            return Bcklogs;
            
            
        }
    }
}
