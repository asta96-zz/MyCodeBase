using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReadAzureKeyVault
{
    class Program
    {
        //const string CLIENTSECRET = "f_X1U_1vK-F7-r_QsTeV9i9I323HKIp8~O";
        //const string CLIENTID = "a87ed707-fa2d-406a-8926-61ae57b535ba";
        //const string BASESECRETURI =
        //    "https://crmcred123.vault.azure.net"; // available from the Key Vault resource page

        //static KeyVaultClient kvc = null;

        //public static string SECRETNAME { get; private set; }

        //static void Main(string[] args)
        //{
        //    DoVault();

        //    Console.ReadLine();
        //}
        //public static async Task<string> GetToken(string authority, string resource, string scope)
        //{
        //    var authContext = new AuthenticationContext(authority);
        //    ClientCredential clientCred = new ClientCredential(CLIENTID, CLIENTSECRET);
        //    AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

        //    if (result == null)
        //        throw new InvalidOperationException("Failed to obtain the JWT token");

        //    return result.AccessToken;
        //}
        //private static void DoVault()
        //{
        //    try
        //    {
        //        kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));

        //        // write
        //        writeKeyVault();
        //        Console.WriteLine("Press enter after seeing the bundle value show up");
        //        Console.ReadLine();

        //        SecretBundle secret = Task.Run(() => kvc.GetSecretAsync(BASESECRETURI +
        //            @"/secrets/" + SECRETNAME)).ConfigureAwait(false).GetAwaiter().GetResult();
        //        Console.WriteLine(secret.Tags["Test1"].ToString());
        //        Console.WriteLine(secret.Tags["Test2"].ToString());
        //        Console.WriteLine(secret.Tags["CanBeAnything"].ToString());

        //        Console.ReadLine();
        //    }
        //    catch(Exception ex)
        //    {

        //    }

        //}
        //private static async void writeKeyVault()// string szPFX, string szCER, string szPassword)
        //{
        //    try
        //    {
        //        SecretAttributes secretattributes = new SecretAttributes
        //        {
        //            Enabled = true//,
        //                          //Expires = DateTime.UtcNow.AddYears(2), // if you want to expire the info
        //                          //NotBefore = DateTime.UtcNow.AddDays(1) // if you want the info to 
        //                          // start being available later
        //        };

        //        IDictionary<string, string> alltags = new Dictionary<string, string>();
        //        alltags.Add("Test1", "This is a test1 value");
        //        alltags.Add("Test2", "This is a test2 value");
        //        alltags.Add("CanBeAnything", "Including a long encrypted string if you choose");
        //        string secretname = "TestSecret";
        //        string secretvalue = "searchValue"; // this is what you will use to search for the item later
        //        string contentType = "SecretInfo"; // whatever you want to categorize it by; you name it

        //        SecretBundle bundle = await kvc.SetSecretAsync(BASESECRETURI, secretname, secretvalue, alltags, contentType, secretattributes);

        //        // (BASESECRETURI, TestName, TestValue, alltags, contentType, attribs);
        //        Console.WriteLine("Bundle:" + bundle.Tags["Test1"].ToString());
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        //public void testconnection()
        //{
        //    var keyclient= new KeyVaultClient((authority, resource, scope) =>
        //    {
        //        var adCredentials = new ClientCredential(CLIENTID, CLIENTSECRET);
        //        var authenticationContext = new AuthenticationContext(authority, null);
        //        return authenticationContext.AcquireToken(resource, adCredential).AccessToken;
        //    }

        private const string clientID = "a87ed707-fa2d-406a-8926-61ae57b535ba";
        private const string aadInstance = "https://login.microsoftonline.com/{0}";
        private const string tenant = "24d681be-e0bd-4d7d-8fcb-555299fbc03c";
        private const string resource = "https://crmcred.azurewebsites.net";
        private const string appKey = "IcR_6DNks_~UE.s565qrS_Hlhqo43Qp861";
        static string authority = 
            //"https://login.microsoftonline.com/24d681be-e0bd-4d7d-8fcb-555299fbc03c";
            String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

        private static HttpClient httpClient = new HttpClient();
        private static AuthenticationContext context = null;
        private static ClientCredential credential = null;
        static void Main(string[] args)
        {
            context = new AuthenticationContext(authority);
            credential = new ClientCredential(clientID, appKey);

            Task<string> token = GetToken();
            token.Wait();
            Console.WriteLine(token.Result);
            Task<string> users = GetUsers(token.Result);
            users.Wait();
            Console.ReadLine();
        }

        private static async Task<string> GetUsers(string result)
        {
            string users = null;
            return users;
        } 

        private static async Task<string> GetToken()
        {
            AuthenticationResult result = null;
            string token = null;
            result = await context.AcquireTokenAsync(resource, credential);
            token = result.AccessToken;
            return token;
        }
    }

}
