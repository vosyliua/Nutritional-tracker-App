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
    internal class LoginPageViewModel:BaseViewModel
    {
        private string _username;  
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }



            public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        public ICommand goToRegisterPageCommand { get; set; }
        public ICommand submitLoginCommand { get; set; }

        public IAccountService _accountService;

        public LoginPageViewModel(IAccountService AccountService)
        {
            goToRegisterPageCommand = new Command(goToRegisterPage);
            submitLoginCommand = new Command(submitLogin);
            _accountService = AccountService;
        }

        public async void goToRegisterPage()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new RegisterPage());
        }

        public async void submitLogin()
        {

            var data = await _accountService.getAccounts(Username, Password);
            if (data!="0")
            {

                await NavigationDispatcher.Instance.Navigation.PushAsync(new FoodsPage());
                Console.WriteLine(data);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Incorrect username or password", "OK");
            }
        }
    }
}
