using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionTester
{
    class Program
    {
        private static CrmServiceClient _service = null;
        static void Main(string[] args)
        {
            string crmConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString);
            _service = new CrmServiceClient(crmConnectionString);
            if(_service.IsReady)
            {
                Console.WriteLine("Connected successfully");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Connection failed");
            }
        }
    }
}
