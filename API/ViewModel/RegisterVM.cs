using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class RegisterVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public float Salary { get; set; }
        public string Department_ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role_ID { get; set; }
    }
}
