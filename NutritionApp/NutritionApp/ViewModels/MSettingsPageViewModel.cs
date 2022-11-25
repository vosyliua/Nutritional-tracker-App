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
    internal class MSettingsPageViewModel : BaseViewModel
    {
        private int _age1;
        private float _height1;
        private float _weight1;
        private string _activity1;
        private string _gender1;

        public int Age11
        {
            get => _age1;
            set
            {
                _age1 = value;
                OnPropertyChanged(nameof(Age11));
            }
        }
        public float Height11
        {
            get => _height1;
            set
            {
                _height1 = value;
                OnPropertyChanged(nameof(Height11));
            }
        }
        public float Weight11
        {
            get => _weight1;
            set
            {
                _weight1 = value;
                OnPropertyChanged(nameof(Weight11));
            }
        }
        public string Activity11
        {
            get => _activity1;
            set
            {
                _activity1 = value;
                OnPropertyChanged(nameof(Activity11));
            }
        }
        public string Gender11
        {
            get => _gender1;
            set
            {
                _gender1 = value;
                OnPropertyChanged(nameof(Gender11));
            }
        }

        public ISettingsService _settingsService1 { get; set; }
        public IAccountService _accountService1 { get; set; }

        public ICommand UpdateSettingsCommand1 { get; set; }
        public MSettingsPageViewModel(ISettingsService settingsService, IAccountService accountService)
        {
            _settingsService1 = settingsService;
            _accountService1 = accountService;
            UpdateSettingsCommand1 = new Command(updateSettings);

        }
        public async void updateSettings()
        {
            var userRetrieved1 = _accountService1.getUsername();
            var settings = new Settings
            {
                Age = Age11,
                Weight = Weight11,
                Height = Height11,
                Gender = Gender11,
                Activity = Activity11
            };

            _settingsService1.calculateCaloriesPatch(settings, userRetrieved1); // pass in one more arg string user

            await NavigationDispatcher.Instance.Navigation.PopAsync();
               
        }
    }



}
