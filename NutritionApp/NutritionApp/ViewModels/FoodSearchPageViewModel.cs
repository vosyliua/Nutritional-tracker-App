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


namespace NutritionApp.ViewModels
{
    internal class FoodSearchPageViewModel : BaseViewModel
    {

        private string _foodName;
        public ObservableCollection<Fixed> _foods;
        private string _measurment;
        public List<double> TotalCal;
        private double _value;

        private Fixed retrievedFood;
        private Fixed saveRetrievedFood;

        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        public string Measurment
        {
            get => _measurment;
            set
            {
                _measurment = value;
                OnPropertyChanged(nameof(Measurment));
            }
        }
        public Fixed SaveRetrievedFood
        {
            get => saveRetrievedFood;
            set
            {
                saveRetrievedFood = value;
                OnPropertyChanged(nameof(SaveRetrievedFood));
            }
        }

        public Fixed RetrievedFood
        {
            get => retrievedFood;
            set
            {
                retrievedFood = value;
                OnPropertyChanged(nameof(RetrievedFood));
            }
        }
      

        public ObservableCollection<Fixed> Foods
        {
            get => _foods;
            set
            {
                _foods = value;
                OnPropertyChanged(nameof(Foods));
            }
        }

        private double _settingsCalories { get; set; }
        public double SettingsCalories
        {
            get => _settingsCalories;
            set
            {
                _settingsCalories = value;
                OnPropertyChanged(nameof(SettingsCalories));
            }
        }

        private string _settingsCalories1 { get; set; }
        public string SettingsCalories1
        {
            get => _settingsCalories1;
            set
            {
                _settingsCalories1 = value;
                OnPropertyChanged(nameof(SettingsCalories1));
            }
        }

        private double _settingsCalories2 { get; set; }
        public double SettingsCalories2
        {
            get => _settingsCalories2;
            set
            {
                _settingsCalories2 = value;
                OnPropertyChanged(nameof(SettingsCalories2));
            }
        }

        private string _settingsCaloriesText { get; set; }
        public string SettingsCaloriesText
        {
            get => _settingsCaloriesText;
            set
            {
                _settingsCaloriesText = value;
                OnPropertyChanged(nameof(SettingsCaloriesText));
            }
        }


        public IFoodService _foodService;
        public IBacklogService1 _backlogService;
        public ISettingsService _settingsService;
        public IAccountService _accountService;
        public string FoodName
        {
            get => _foodName;
            set
            {
                _foodName = value;
                OnPropertyChanged(nameof(FoodName));
            }
        }

        public ICommand getFoodCommand { get; set; }
        public ICommand saveFoodCommand { get; set; }
        public ICommand saveToDiaryCommand { get; set; }
        public ICommand goToSettingsPageCommand { get; set; }
        public FoodSearchPageViewModel(IFoodService foodService, IBacklogService1 backlogService, ISettingsService settingsService, IAccountService accountService)
        {
            _settingsService = settingsService;
            _accountService = accountService;
            SettingsCaloriesText = "Calories Remaining To Eat For The Day: ";
            _foodService = foodService;
            _backlogService = backlogService;
            getFoodCommand = new Command(async ()=>await getFoods(FoodName,Measurment));
            Foods = new ObservableCollection<Fixed>();
            TotalCal = new List<Double>();
            saveFoodCommand = new Command(async () => await saveFood());
            saveToDiaryCommand = new Command(saveDiary);
            goToSettingsPageCommand = new Command(goToSettingsPage);
        }
        async void goToSettingsPage()
        {
            
          
            await NavigationDispatcher.Instance.Navigation.PushAsync(new MainSettingsPage());
            
            
        }

        public async Task<double> checkSettings()
        {
            var user = _accountService.getUsername();
            if (user != null && user !="")
            {
                var data = await _settingsService.getSettingsCalories(user);
                SettingsCalories2 = data.Calories;
                SettingsCalories = data.Calories;
                SettingsCalories1 = SettingsCalories.ToString();
                double x = Double.Parse(SettingsCalories1);
                double y = Math.Round(x);
                string v = y.ToString();
                SettingsCalories1 = v;
                Math.Round(SettingsCalories);
                return SettingsCalories;
            }
            else
            {
                await NavigationDispatcher.Instance.Navigation.PushAsync(new LoginPage());
            }
            return 0;  
        }

        public async Task getFoods(string foodName, string measurment)
        {
            FoodName = foodName;
            Measurment = measurment;

            try
            {
                var retrievedFoods = await _foodService.GetFoods(FoodName, Measurment);
                RetrievedFood = retrievedFoods;
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        public async void saveDiary()
        {

            foreach(var food in Foods)
            {
                food.CaloriesEaten = SettingsCalories1;
                food.CaloriesEatenText = SettingsCaloriesText;
                Console.WriteLine(food.Name);
            }
            var username = _accountService.getUsername();
            if(Foods.Count == 0)
            {
                return;
            }
            await _backlogService.addData(Foods, username);
            Foods.Clear();
            var x = Math.Round(SettingsCalories2);
            SettingsCalories = x;
            SettingsCalories1 = SettingsCalories.ToString();
            Value = 0;
            SettingsCaloriesText = "Calories Remaining To Eat For The Day: ";

        }



        public async Task saveFood()
        {
            try
            {
                SaveRetrievedFood = RetrievedFood;
                if (SaveRetrievedFood != null)
                {
                    Foods.Add(SaveRetrievedFood);
                    var p = Math.Round(SettingsCalories);
                    SettingsCalories = p;
                    SettingsCalories = SettingsCalories - SaveRetrievedFood.Calories;
                    var temporary = SettingsCalories.ToString();
                    SettingsCalories1 = temporary;
                    double x = Double.Parse(SettingsCalories1);
                    Math.Round(x);
                    string v = x.ToString();
                    SettingsCalories1 = v;
                    if (SettingsCalories <= 0)
                    {
                        SettingsCaloriesText = "You Are Over Your Daily Calorie Intake By: ";

                        SettingsCalories1 = SettingsCalories1.Replace('-', ' ');
                    }
                    
                }
                else
                {
                    return;
                }
                TotalCal.Add(SaveRetrievedFood.Calories);
                Value = TotalCal.Sum(x => x);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

    }
}
