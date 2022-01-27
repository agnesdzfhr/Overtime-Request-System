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
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;

        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };

        }

        public async Task<RegisterVM> GetRegisterByNIK(string NIK)
        {
            RegisterVM entity = null;

            using (var response = await httpClient.GetAsync(request + $"GetRegisterData/{NIK}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<RegisterVM>(apiResponse);
            }
            return entity;
        }


    }
}
