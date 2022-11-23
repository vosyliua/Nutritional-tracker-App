
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NutritionApp.ViewModels;

namespace NutritionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodsPage : ContentPage
    {
        public FoodsPage()
        {
            InitializeComponent();
        }
            protected override async void OnAppearing()
        {
            BindingContext = IocProvider.ServiceProvider.GetService<FoodSearchPageViewModel>();
            try
            {
                await (BindingContext as FoodSearchPageViewModel).checkSettings();
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }

        }
    }
  }
