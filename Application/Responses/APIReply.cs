using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class APIReply<T>
    {
        public T? result { get; set; }
        public string message { get; set; } = string.Empty;
     
        public System.Net.HttpStatusCode statusCode { get; set; }
    }
}
