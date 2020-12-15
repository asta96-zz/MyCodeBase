using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
namespace azure
{

    class AuthAzure
    {

        const string CLIENTSECRET = "QsiDE2_F~2~6yNwv3u_082xCX5Zr6~~KV-";
        const string CLIENTID = "c1e865b6-c86e-4a75-91b0-f28791735c76";
        const string BASESECRETURI =
            "https://retro-keyvault-001.vault.azure.net"; // available from the Key Vault resource page

        static KeyVaultClient kvc = null;

        static void Main(string[] args)
        {
            DoVault();

            Console.ReadLine();
        }
        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(CLIENTID, CLIENTSECRET);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }
        private static void DoVault()
        {
            kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));

             //write
           // writeKeyVault();
            Console.WriteLine("Press enter after seeing the bundle value show up");
            Console.ReadLine();
            string SECRETNAME = "Eyrites";

            SecretBundle secret = Task.Run(() => kvc.GetSecretAsync(BASESECRETURI +
                @"/secrets/" + SECRETNAME)).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(secret.SecretIdentifier.Name+":"+ secret.Value); 

            Console.ReadLine();

        }
        private static async void writeKeyVault()// string szPFX, string szCER, string szPassword)
        {
            SecretAttributes attribs = new SecretAttributes
            {
                Enabled = true//,
                              //Expires = DateTime.UtcNow.AddYears(2), // if you want to expire the info
                              //NotBefore = DateTime.UtcNow.AddDays(1) // if you want the info to 
                              // start being available later
            };

            IDictionary<string, string> alltags = new Dictionary<string, string>();
            alltags.Add("Test1", "This is a test1 value");
            alltags.Add("Test2", "This is a test2 value");
            alltags.Add("CanBeAnything", "Including a long encrypted string if you choose");
            string TestName = "TestSecret";
            string TestValue = "searchValue"; // this is what you will use to search for the item later
            string contentType = "SecretInfo"; // whatever you want to categorize it by; you name it

            SecretBundle bundle = await kvc.SetSecretAsync
               (BASESECRETURI, TestName, TestValue, alltags, contentType, attribs);
            Console.WriteLine("Bundle:" + bundle.Tags["Test1"].ToString());
        }
    }
}