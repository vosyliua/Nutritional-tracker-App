using System.Linq;
using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NutritionApp.Models;
using NutritionApp.ViewModels;
using Xamarin.Forms;

namespace NutritionApp.Views
{
    public partial class BacklogsPage : ContentPage
    {
        public BacklogsPage()
        {
            InitializeComponent();
            BindingContext = IocProvider.ServiceProvider.GetService<BackLogsPageViewModel>();

        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string BacklogDate = (e.CurrentSelection.FirstOrDefault() as Backlog).Date;
            // The following route works because route names are unique in this application.
            await Shell.Current.GoToAsync($"backlogdetails?date={BacklogDate}");
           
        }
        protected override async void OnAppearing()
        {
            BindingContext = IocProvider.ServiceProvider.GetService<BackLogsPageViewModel>();
            try
            {
                await (BindingContext as BackLogsPageViewModel).Initialise();
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }

        }
    }
}