using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class APIReplyPaginacion
    {
        public object paginacion { get; set; }
        public object result { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public HttpStatusCode statusCode { get; set; }
    }
}
