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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = IocProvider.ServiceProvider.GetService<RegisterPageViewModel>();
            Shell.SetTabBarIsVisible(this, false);
            Shell.SetFlyoutItemIsVisible(this, false);
        }
    }
}