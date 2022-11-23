using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NutritionApp.Views
{
    public partial class AboutPage : ContentPage
    {
        public ICommand TapCommand => new Command (() => ChangeTheme());
        public string ThemeName { get; set; }

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = this;

            OSAppTheme currentTheme = Application.Current.RequestedTheme;
            ThemeName = currentTheme == OSAppTheme.Dark ? "Turn white theme on" : "turn Dark theme on";

            
        }
        public void ChangeTheme()
        {
            OSAppTheme currentTheme = Application.Current.RequestedTheme;
            Application.Current.UserAppTheme = currentTheme == OSAppTheme.Dark ? OSAppTheme.Light : OSAppTheme.Dark;
            ThemeName = currentTheme == OSAppTheme.Dark ? "Turn WHite theme on" : "turn dark theme on";
            OnPropertyChanged(nameof(ThemeName));
        }
    }
}