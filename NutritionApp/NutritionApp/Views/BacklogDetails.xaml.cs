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
    public partial class BacklogDetails : ContentPage
    {
        public BacklogDetails()
        {
            InitializeComponent();
            BindingContext = IocProvider.ServiceProvider.GetService<BackLogsDetailsViewModel>();
        }
    }
}