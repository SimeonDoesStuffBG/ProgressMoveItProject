using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProgressTestApp.HTTP
{
    class HttpConnection
    {
        public static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://testserver.moveitcloud.com")
        };
    }
}
