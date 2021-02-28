using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommerceTakeHome.WebHost.Models
{
    public class ExceptionResponse
    {
        public string ErrorMessage { get; set; }
        public string Status { get; set; }
    }
}
