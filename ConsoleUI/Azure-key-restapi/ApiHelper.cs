using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Azure_key_restapi
{
   public class ApiHelper
    {
        public  HttpClient client { get; set; }

        public  void InitializeClient ()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
        }
    }
}
