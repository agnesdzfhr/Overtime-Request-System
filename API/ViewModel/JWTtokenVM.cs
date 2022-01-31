using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class JWTtokenVM
    {
        public HttpStatusCode status { get; set; }
        public string idtoken { get; set; }
        public string message { get; set; }
    }
}
