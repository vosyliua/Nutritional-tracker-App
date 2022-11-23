using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NutritionApp.ViewModels;
using NutritionApp.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using NutritionApp.Services;
using System.Text.Json;



namespace NutritionApp.Data
{
    internal class LogData1: IBacklogService1
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<Backlog> BacklogData { get; set; }
        public LogData1()
        {
            BacklogData = new ObservableCollection<Backlog>();
            #if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _httpClient = new HttpClient(insecureHandler);
#else
    HttpClient _httpClient = new HttpClient();
#endif
            _httpClient.BaseAddress = new Uri("http://10.0.2.2:7163/api/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        }


        public async Task<bool> addData(ObservableCollection<Fixed> data, string username)
        {
            List<Fixed> myList = new List<Fixed>(data);
            var date = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            var details = new Backlog
            {
                Username = username,
                Date = date,
                Details = myList

            };
            if (details.Details.Count != 0)// async call
            {
                try
                {
                    var response = await _httpClient.PostAsync("Accounts/AddBacklog",
                         new StringContent(JsonSerializer.Serialize(details), Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        return response.IsSuccessStatusCode;
                    }
                    
                }
                catch ( Exception e)
                {
                    Debug.Fail(e.Message);
                }
            }
            return true;
   
        }


        public async Task<List<Backlog>> returnData(string username)//async call
        {
            var response = await _httpClient.GetAsync($"Accounts/GetBacklog?Username={username}");

            if (response.IsSuccessStatusCode)
            {

                var responseAsString = await response?.Content?.ReadAsStringAsync();

                var backlogs = JsonSerializer.Deserialize<List<Backlog>>(responseAsString);
                return backlogs;

            }
            else
            {
                throw new Exception();
            }
            return new List<Backlog>();
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


