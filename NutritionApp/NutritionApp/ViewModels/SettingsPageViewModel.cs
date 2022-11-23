using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using NutritionApp.Models;
using NutritionApp.Services;
using System.Linq;
using System.Collections.Generic;
using NutritionApp.Views;
using System.ComponentModel;
using NutritionApp.Data;

namespace NutritionApp.ViewModels
{
    internal class SettingsPageViewModel:BaseViewModel
    {
        private int _age;
        private float _height;
        private float _weight;
        private string _activity;
        private string _gender;

        public int Age1
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age1));
            }
        }
        public float Height1
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height1));
            }
        }
        public float Weight1
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight1));
            }
        }
        public string Activity1
        {
            get => _activity;
            set
            {
                _activity = value;
                OnPropertyChanged(nameof(Activity1));
            }
        }
        public string Gender1
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender1));
            }
        }

        public ISettingsService _settingsService { get; set; }
        public IAccountService _accountService { get; set; }

        public ICommand SaveSettingsCommand { get; set; }
        public SettingsPageViewModel(ISettingsService settingsService, IAccountService accountService)
        {
            _settingsService = settingsService;
            _accountService = accountService;
            SaveSettingsCommand = new Command(saveSettings);
            
        }
        public async void saveSettings()
        {
            var userRetrieved = _accountService.getUsername();
          
            var settings = new Settings
            {
                Age = Age1,
                Weight = Weight1,
                Height = Height1,
                Gender = Gender1,
                Activity = Activity1
            };

            _settingsService.calculateCalories(settings, userRetrieved); // pass in one more arg string user

            await NavigationDispatcher.Instance.Navigation.PushAsync(new FoodsPage());
        }
    }



}
