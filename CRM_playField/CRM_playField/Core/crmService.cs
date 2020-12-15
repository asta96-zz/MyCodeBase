using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRM_playField
{
     class crmService
    {
        public  CrmServiceClient CrmConnector()
        {
            ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            string connectionString = ConfigurationManager.ConnectionStrings["CrmConnection"].ConnectionString;
            CrmServiceClient conn = new CrmServiceClient(connectionString);
            return conn;
        }
    }
}
