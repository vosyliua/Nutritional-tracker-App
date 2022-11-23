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
using Xamarin.Forms;


namespace NutritionApp.Data
{
    internal class AccountsData:IAccountService
    {
        private readonly HttpClient _httpClient;
        private string _username;
        
        
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
            }
        }
        public AccountsData()
        {
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _httpClient = new HttpClient(insecureHandler);
#else
    HttpClient _httpClient = new HttpClient();
#endif
            _httpClient.BaseAddress = new Uri("http://10.0.2.2:7163/api/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<bool> RegisterAccount(string user, string pass)
        {

            HttpResponseMessage response = new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest };

            try
            {
                var account = new Account
                {
                    Username = user,
                    Password = pass
                };
                Username = account.Username;
                response = await _httpClient.PostAsync("Accounts/AddAccount",
            new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8, "application/json"));
                MessagingCenter.Send<AccountsData, string>(this, "settings", user);


            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }

            return response.IsSuccessStatusCode;

        }

        public string getUsername()
        {
            return Username;

        }




        public async Task<string> getAccounts(string user, string pass)
        {
          var authData = string.Format("{0}:{1}", user, pass);
          var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

          _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeaderValue);
          Console.WriteLine(_httpClient.DefaultRequestHeaders.Authorization);
          var response = await _httpClient.GetAsync("Accounts/GetAccounts");
                
          if (response.IsSuccessStatusCode)
            {
                    
                var responseAsString = await response?.Content?.ReadAsStringAsync();
                    
                var accounts =  JsonSerializer.Deserialize<IEnumerable<Account>>(responseAsString);
                Username = user;
                Console.WriteLine("in get accounts testing if user set " + Username);
                return Username;
                    
             }
           else
            {
                return Username = "0";
            }
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


