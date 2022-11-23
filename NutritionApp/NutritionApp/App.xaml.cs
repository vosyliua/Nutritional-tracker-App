
using System;
using NutritionApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutritionApp
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "AppTheme_experimental" });
            InitializeComponent();
            IocProvider.Init();
            MainPage = new AppShell();
            NavigationDispatcher.Instance.Initialize(MainPage.Navigation);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}