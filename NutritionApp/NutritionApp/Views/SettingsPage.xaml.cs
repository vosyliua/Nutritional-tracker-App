using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NutritionApp.ViewModels;
namespace NutritionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = IocProvider.ServiceProvider.GetService<SettingsPageViewModel>();
            Shell.SetTabBarIsVisible(this, false);
            Shell.SetFlyoutItemIsVisible(this, false); 
        }
    }
}