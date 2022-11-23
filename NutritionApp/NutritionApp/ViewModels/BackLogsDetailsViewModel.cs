using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using NutritionApp.Models;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Web;
using NutritionApp.Services;
using NutritionApp.Data;


namespace NutritionApp.ViewModels
{
    internal class BackLogsDetailsViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        public Backlog Item { get; private set; }

        public int x { get; set; }

        public IBacklogService1 _backlogService;
        public IAccountService _accountService;

        public BackLogsDetailsViewModel(IBacklogService1 backlogService, IAccountService accountService)
        {
            _accountService = accountService;
            _backlogService = backlogService;
        }

        public ObservableCollection<Backlog> Backlogs { get; set; }
        public void data()
        {
            MessagingCenter.Subscribe<BackLogsPageViewModel, ObservableCollection<Backlog>>(this, "tack", (sender, backlog) =>
            {
               foreach(Backlog item in backlog)
                {
                    Backlogs.Add(item);
                };
            });
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            // Only a single query parameter is passed, which needs URL decoding.
            string date = HttpUtility.UrlDecode(query["date"]);
            LoadAnimal(date);
        }

        async void LoadAnimal(string date)
        {
            var username  = _accountService.getUsername();
            var foodData = await _backlogService.returnData(username);
            Item = foodData.FirstOrDefault(a => a.Date == date);
            double x = Double.Parse(Item.Details[0].CaloriesEaten);
            double v = Math.Round(x);
            v.ToString();
            if (Item.Details[0].CaloriesEatenText == "You Are Over Your Daily Calorie Intake By: ")
            {
                Item.Details[0].CaloriesEatenText = "You Overate By: ";
                Item.Details[0].CaloriesEaten = Item.Details[0].CaloriesEaten + " Calories";
                Item.Details[0].CaloriesEaten = v + " Calories";
            }
            if(Item.Details[0].CaloriesEatenText == "Calories Remaining To Eat For The Day: ")
            {
                Item.Details[0].CaloriesEatenText = "You Underate by: ";
                Item.Details[0].CaloriesEaten = v + " Calories";
            }

            OnPropertyChanged("Item");
        }
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}