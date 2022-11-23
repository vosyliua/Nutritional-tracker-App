using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NutritionApp.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using NutritionApp.Models;

namespace NutritionApp.Services
{
    public class ApiFoodService : IFoodService
    {
        private readonly HttpClient _httpClient;

        private List<Backlog> _backlogs;



        public ApiFoodService()
        {
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _httpClient = new HttpClient(insecureHandler);
#else
    HttpClient _httpClient = new HttpClient();
#endif
            _httpClient.BaseAddress = new Uri("https://api.edamam.com/api/food-database/v2/parser");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        }
        
        public async Task<Fixed> GetFoods(string foodToGet, string measurment)
        {
            HttpResponseMessage response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };
            try
            {
                if (foodToGet.Contains(" "))
                {
                    foodToGet = foodToGet.Replace(" ", "%20");
                }
                response = await _httpClient.GetAsync($"?app_id=0dc8dbf9&app_key=26d3a8f5d794b2698a1b8f8bf2c1802b&ingr={foodToGet}&nutrition-type=cooking");
                
                if (response.IsSuccessStatusCode)
                {


                    var mlen = measurment.Length;
                    if (mlen == 1)
                    {
                        measurment = measurment.Insert(0, "0.0");
                    }
                    if (mlen == 2)
                    {
                        measurment =measurment.Insert(0,"0.");
                    }
                    if (mlen == 3)
                    {
                        measurment = measurment.Insert(1, ".");
                    }
                    if (mlen == 4)
                    {
                        measurment = measurment.Insert(2, ".");
                    }
                    if (mlen == 5)
                    {
                        measurment = measurment.Insert(3, ".");
                    }
                    var responseAsString = await response?.Content?.ReadAsStringAsync();
                    float multipli = float.Parse(measurment);
                    var options = new JsonSerializerSettings();
                    options.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                    dynamic result = JsonConvert.DeserializeObject<Root>(responseAsString);
                    if (result.parsed.Count==0)
                    {
                        var food1 = new Fixed
                        {
                            Name = result.hints[0].food.label,
                            Calories = Math.Truncate(result.hints[0].food.nutrients.ENERC_KCAL * multipli),
                            Protein = Math.Truncate(result.hints[0].food.nutrients.PROCNT * multipli),
                            Carbohydrates = Math.Truncate(result.hints[0].food.nutrients.CHOCDF * multipli),
                            Fat = Math.Truncate(result.hints[0].food.nutrients.FAT * multipli),
                            Image = result.hints[0].food.image

                        };
                        return food1;
                    }
                    else
                    {
                        var food2 = new Fixed
                        {
                            Name = result.parsed[0].food.label,
                            Calories = Math.Truncate(result.parsed[0].food.nutrients.ENERC_KCAL * multipli  ),
                            Protein = Math.Truncate(result.parsed[0].food.nutrients.PROCNT * multipli),
                            Carbohydrates = Math.Truncate(result.parsed[0].food.nutrients.CHOCDF * multipli),
                            Fat = Math.Truncate(result.parsed[0].food.nutrients.FAT * multipli),
                            Image = result.parsed[0].food.image

                        };
                        return food2;
                    }
                }

            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }
            throw new Exception("No food found");

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