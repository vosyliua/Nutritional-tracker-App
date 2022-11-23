using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NutritionApp.Models;
using NutritionApp.Services;


namespace NutritionApp.Data
{
    internal class SettingsData: ISettingsService
    {
        private Settings RetrievedSett { get; set; }
        private readonly HttpClient _httpClient;
        public string RetrievedUser { get; set; }
        private double CaloriesPerDay { get; set; }
        public SettingsData()
        {
            RetrievedSett = new Settings();
            CaloriesPerDay = new double();

#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _httpClient = new HttpClient(insecureHandler);
#else
    HttpClient _httpClient = new HttpClient();
#endif
            _httpClient.BaseAddress = new Uri("http://10.0.2.2:7163/api/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
  

        public async void calculateCalories(Settings settings, string username)
        {
            RetrievedSett = settings;
            Console.WriteLine("save settings user" + RetrievedUser);
            if (RetrievedSett.Height!=0)
            {
                if (RetrievedSett.Gender == "Male")
                {
                    CaloriesPerDay = 66.5 + (13.75 * RetrievedSett.Weight) + (5.003 * RetrievedSett.Height) - (6.75 * RetrievedSett.Age);
                    Console.WriteLine(CaloriesPerDay + "   for men");
                    if (RetrievedSett.Activity == "Light: excersise 1 - 3 times a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 200;
                    }
                    if (RetrievedSett.Activity == "Active: Daily excersise or intense excersise 3 - 4 a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 600;
                    }
                    if (RetrievedSett.Activity == "Very Active: intense excersise 5 - 6 times a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 900;
                    }
                    if (RetrievedSett.Activity == "Extra Active: very intense excersise daily, or physical job")
                    {
                        CaloriesPerDay = CaloriesPerDay + 1200;
                    }
                }
                else
                {
                    CaloriesPerDay = 655.1 + (9.563 * RetrievedSett.Weight) + (1.850 * RetrievedSett.Height) - (4.676 * RetrievedSett.Age);
                    Console.WriteLine(CaloriesPerDay + "   for women");
                    if (RetrievedSett.Activity == "Light: excersise 1 - 3 times a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 200;
                    } 
                    if (RetrievedSett.Activity == "Active: Daily excersise or intense excersise 3 - 4 a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 500 ;
                    }
                    if (RetrievedSett.Activity == "Very Active: intense excersise 5 - 6 times a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 800;
                    }
                    if (RetrievedSett.Activity == "Extra Active: very intense excersise daily, or physical job")
                    {
                        CaloriesPerDay = CaloriesPerDay + 1000;
                    }
                }
            }
            else
            {
                ;
            }
            Console.WriteLine("in sum " + CaloriesPerDay + username);
            await RegisterSettings(CaloriesPerDay, username);
        }

        public async void calculateCaloriesPatch(Settings settings, string username)
        {
            RetrievedSett = settings;
            Console.WriteLine("patch settings user" + username);
            if (RetrievedSett.Height != 0)
            {
                if (RetrievedSett.Gender == "Male")
                {
                    CaloriesPerDay = 66.5 + (13.75 * RetrievedSett.Weight) + (5.003 * RetrievedSett.Height) - (6.75 * RetrievedSett.Age);
                    Console.WriteLine(CaloriesPerDay + "   for men");
                    if (RetrievedSett.Activity == "Light: excersise 1 - 3 times a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 200;
                    }
                    if (RetrievedSett.Activity == "Active: Daily excersise or intense excersise 3 - 4 a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 600;
                    }
                    if (RetrievedSett.Activity == "Very Active: intense excersise 5 - 6 times a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 900;
                    }
                    if (RetrievedSett.Activity == "Extra Active: very intense excersise daily, or physical job")
                    {
                        CaloriesPerDay = CaloriesPerDay + 1200;
                    }
                }
                else
                {
                    CaloriesPerDay = 655.1 + (9.563 * RetrievedSett.Weight) + (1.850 * RetrievedSett.Height) - (4.676 * RetrievedSett.Age);
                    Console.WriteLine(CaloriesPerDay + "   for women");
                    if (RetrievedSett.Activity == "Light: excersise 1 - 3 times a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 200;
                    }
                    if (RetrievedSett.Activity == "Active: Daily excersise or intense excersise 3 - 4 a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 500;
                    }
                    if (RetrievedSett.Activity == "Very Active: intense excersise 5 - 6 times a week")
                    {
                        CaloriesPerDay = CaloriesPerDay + 800;
                    }
                    if (RetrievedSett.Activity == "Extra Active: very intense excersise daily, or physical job")
                    {
                        CaloriesPerDay = CaloriesPerDay + 1000;
                    }
                }
            }
            else
            {
                ;
            }
            Console.WriteLine("in sum " + CaloriesPerDay + username);
            await PatchSettings(CaloriesPerDay, username);
        }

        public double Calories()
        {
            return CaloriesPerDay;
        }



        public async Task<bool> PatchSettings(double updateCalories, string username)
        {

            HttpResponseMessage response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };

            try
            {
                var updateSettings = new Mixed
                {
                    Username = username,
                    Calories = updateCalories
                };
                response = await _httpClient.PutAsync("Accounts/ModifySettings",
            new StringContent(JsonSerializer.Serialize(updateSettings), Encoding.UTF8, "application/json"));
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }


            return response.IsSuccessStatusCode;

        }
        public async Task<bool> RegisterSettings(double calories, string user)
        {

            HttpResponseMessage response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };

            try
            {
                var settings = new Mixed
                {
                    Username = user,
                    Calories = calories
                };
                response = await _httpClient.PostAsync("Accounts/AddSettings",
            new StringContent(JsonSerializer.Serialize(settings), Encoding.UTF8, "application/json"));
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }


            return response.IsSuccessStatusCode;

        }


        public async Task<bool> ModifySettings(double calories, string user)
        {

            HttpResponseMessage response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };

            try
            {
                var settings = new Mixed
                {
                    Username = user,
                    Calories = calories
                };
                response = await _httpClient.PostAsync("Accounts/ModifySettings",
            new StringContent(JsonSerializer.Serialize(settings), Encoding.UTF8, "application/json"));
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }


            return response.IsSuccessStatusCode;

        }




        public async Task <ReturnedSettings> getSettingsCalories(string username)
        {

            HttpResponseMessage response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };
            Console.WriteLine($"Usernmae={username}");

            try
            {
                response = await _httpClient.GetAsync($"Accounts/GetSettings?Username={username}");
                
                if (response.IsSuccessStatusCode)
                {

                    var responseAsString = await response?.Content?.ReadAsStringAsync();

                    var calories = JsonSerializer.Deserialize<List<ReturnedSettings>>(responseAsString)[0];
                    Console.WriteLine(calories);
                    return calories;
                }
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }
            throw new Exception();
        }
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }




    }

}





//upon loggin send user through msg service
// call this function in the above function
//send data to endpoint