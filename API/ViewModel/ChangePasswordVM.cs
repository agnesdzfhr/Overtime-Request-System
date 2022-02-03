using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }
        public int TokenOTP { get; set; }
        public string NewPassword { get; set; }
    }
}
