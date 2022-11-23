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
    public partial class MainSettingsPage : ContentPage
    {
        public MainSettingsPage()
        {
            InitializeComponent();
            BindingContext = IocProvider.ServiceProvider.GetService<MSettingsPageViewModel>();
            Shell.SetTabBarIsVisible(this, false);
            Shell.SetFlyoutItemIsVisible(this, false);
        }
    }
}