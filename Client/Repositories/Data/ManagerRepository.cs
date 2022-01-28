using API.Models;
using Client.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class ManagerRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly HttpClient httpClient;

        public ManagerRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
    }
}
