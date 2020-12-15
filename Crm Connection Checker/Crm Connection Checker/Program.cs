using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Client;
using System.Net;

namespace Crm_Connection_Checker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();

            CrmServiceClient client = CrmConnection(username, password);
            if(client.IsReady)
            {
                Console.WriteLine("success");
            }
            else
            {
                Console.WriteLine("Failure");
            }
        }

        private static CrmServiceClient CrmConnection(string username, string password)
        {
            CrmServiceClient connector;
            string crmConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString);
            crmConnectionString = crmConnectionString.Replace("{{USERNAME}}", username);
            crmConnectionString = crmConnectionString.Replace("{{PASSWORD}}", password);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            connector = new CrmServiceClient(crmConnectionString);
            return connector;
        }
    }
}
