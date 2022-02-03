using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.ViewModel;
using Client.Base;
using Newtonsoft.Json;

namespace Client.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;

        public AccountRepository(Address address, string request = "Accounts/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };

        }
        public async Task<JWTtokenVM> Auth(LoginVM loginVM)
        {
            JWTtokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "Login/", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWTtokenVM>(apiResponse);


            return token;
        }

        public Object ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(forgotPasswordVM), Encoding.UTF8, "application/json");

            Object entity = new Object();
            using (var respone = httpClient.PutAsync(request + "ForgotPassword", content).Result)
            {
                string apiResponse = respone.Content.ReadAsStringAsync().Result;
                entity = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
                return entity;
        }

        public Object ChangePassword(ChangePasswordVM changePasswordVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(changePasswordVM), Encoding.UTF8, "application/json");

            Object entity = new Object();
            using (var respone = httpClient.PutAsync(request + "ChangePassword", content).Result)
            {
                string apiResponse = respone.Content.ReadAsStringAsync().Result;
                entity = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            return entity;
        }


    }
}
