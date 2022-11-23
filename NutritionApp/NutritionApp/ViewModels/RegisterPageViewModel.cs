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
    internal class RegisterPageViewModel : BaseViewModel
    {
        private string _usernameR;
        private string _passwordR;

        public string UsernameR { get { return _usernameR; } set { _usernameR = value; OnPropertyChanged(nameof(UsernameR)); } }

        public string PasswordR { get { return _passwordR; } set { _passwordR = value; OnPropertyChanged(nameof(PasswordR)); } }

        public ICommand RegisterUserCommand { get; set; }

        public IAccountService _accountService;
        public RegisterPageViewModel(IAccountService accountService) 
        {
            _accountService = accountService;
            RegisterUserCommand = new Command(RegisterUser);
        }

        public async void RegisterUser()
        {
            var yo =  await _accountService.RegisterAccount(UsernameR, PasswordR);
            if(yo == true)
            {
                await NavigationDispatcher.Instance.Navigation.PushAsync(new SettingsPage());
            }
            

        }
    }
}
