using Microsoft.Extensions.DependencyInjection;
using NutritionApp.Services;
using NutritionApp.Data;
using NutritionApp.ViewModels;

namespace NutritionApp
{
    public static class DependencyInjectionContainer
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IFoodService, ApiFoodService>();
            services.AddSingleton<IBacklogService, LogData>();
            services.AddSingleton<IBacklogService1, LogData1>();
            services.AddSingleton<ISettingsService, SettingsData>();
            services.AddSingleton<IAccountService, AccountsData>();

            return services;
        }

        public static IServiceCollection ConfigureMockServices(this IServiceCollection services)
        {
            //add your mocks.

            return services;
        }

        public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
        {
            services.AddTransient<FoodSearchPageViewModel>();
            services.AddTransient<BackLogsPageViewModel>();
            services.AddTransient<BackLogsDetailsViewModel>();
            services.AddTransient<SettingsPageViewModel>();
            services.AddTransient<LoginPageViewModel>();
            services.AddTransient<RegisterPageViewModel>();
            services.AddTransient<MSettingsPageViewModel>();


            return services;
        }
    }
}