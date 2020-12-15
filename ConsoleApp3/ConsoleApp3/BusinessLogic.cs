using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;
using System.Net;
using System.ServiceModel.Description;

public abstract class BusinessLogic
{
    private static IOrganizationService _service;
    public static IOrganizationService CrmConnector ()
    {
        string CrmConnection = ConfigurationManager.ConnectionStrings["CRMConnection"].ConnectionString;

        try
        {
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = "user1@interviewprep1.onmicrosoft.com";
            credentials.UserName.Password = "Retro@2020";
            Uri serviceUri = new Uri("https://interviewprep1.crm8.dynamics.com/XRMServices/2011/Organization.svc");
            OrganizationServiceProxy proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
            proxy.EnableProxyTypes();
            _service = (IOrganizationService)proxy;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while connecting to CRM " + ex.Message);
            Console.ReadKey();
        }
       // _service = new CrmServiceClient(CrmConnection);
        return _service;
    }


   public abstract void UseMultipleRequest();

   public  abstract void pagingCookie();
}


public class Solution
{
    public int MyAtoi(string s)
    {

        int length = 0;
        long finalInt = 0;
        s = s.Trim();

        string b = string.Empty;
        //int val;

        for (int i = 0; i < s.Length; i++)
        {
            if (i == 0 && (s[0] == '-' || s[0] == '+') && i + 1 >= s.Length)
            {
                return 0;
            }
            if (i == 0 && (s[0] == '-' || s[0] == '+') && char.IsDigit(s[(i + 1) < s.Length ? i + 1 : i]))
            {
                b += s[0];
            }

            if (((s[0] == '-' || s[0] == '+') && char.IsDigit(s[1])) || Char.IsDigit(s[0]))
            {
                if (Char.IsDigit(s[i]))
                {
                    b += s[i];
                    if (!char.IsDigit(s[(i + 1) < s.Length ? i + 1 : i]))
                    {
                        break;
                    }
                }
            }

            else
            { return 0; }

        }

        if (b.Length > 0)
            finalInt = long.Parse(b);

        if (finalInt <= int.MinValue)
        {
            return int.MinValue;
        }

        else if (finalInt >= int.MaxValue)
        {
            return int.MaxValue;
        }

        return Convert.ToInt32(finalInt);

    }



}