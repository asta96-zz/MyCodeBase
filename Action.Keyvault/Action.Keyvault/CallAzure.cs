using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Action.Keyvault
{
    public class CallAzure : CodeActivity
    {
        [RequiredArgument]
        [Input("Secret Name")]
        public InArgument<string> SecretName { get; set; }
        [RequiredArgument]
        [Input("Secret Value")]
        public InArgument<string> SecretValue { get; set; }



        [RequiredArgument]
        [Output("Published Version")]
        public OutArgument<string> PublishedVersion { get; set; }

        //class level variables
        const string CLIENTSECRET = "_-IcUrkh6Mz2317-4F_ryDw3_8.VKUJzjg";
        const string CLIENTID = "d0cb4b3d-b227-4d47-b3f9-ce5b9258b037";
        const string BASESECRETURI =
            "https://dev-kv-001.vault.azure.net"; // available from the Key Vault resource page
        static KeyVaultClient kvc = null;
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                CreateSecret obj = new CreateSecret();
                obj.SecretName = SecretName.Get(context);
                obj.SecretValue = SecretValue.Get(context);
                obj.ContenType = "dev-Action-secret";
                string output = CreateSecretinAzure(obj);
                PublishedVersion.Set(context, output);

            }
            catch (Exception ex)
            {

                throw new InvalidWorkflowException("exception in action" + ex.Message, ex.InnerException);
            }

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
        public string CreateSecretinAzure(CreateSecret Obj)
        {
            kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));

            //write
            Task<SecretBundle> bundle = Task.Run(() => WriteKeyVault(Obj));
            return ("published version:" + bundle.ConfigureAwait(true).GetAwaiter().GetResult().SecretIdentifier.Version);
        }
        private static async Task<SecretBundle> WriteKeyVault(CreateSecret Obj)// string szPFX, string szCER, string szPassword)
        {
            SecretBundle bundle = null;
            SecretAttributes attribs = new SecretAttributes
            {
                Enabled = true//,
                              //Expires = DateTime.UtcNow.AddYears(2), // if you want to expire the info
                              //NotBefore = DateTime.UtcNow.AddDays(1) // if you want the info to 
                              // start being available later
            };

            /*  IDictionary<string, string> alltags = new Dictionary<string, string>();
              alltags.Add("Test1", "This is a test1 value");
              alltags.Add("Test2", "This is a test2 value");
              alltags.Add("CanBeAnything", "Including a long encrypted string if you choose");
              string TestName = "TestSecret";
              string TestValue = "searchValue"; // this is what you will use to search for the item later
              string contentType = "SecretInfo"; // whatever you want to categorize it by; you name it
              */

            IDictionary<string, string> alltags = Obj.KeyValue;
            string TestName = Obj.SecretName;
            string TestValue = Obj.SecretValue; // this is what you will use to search for the item later
            string contentType = Obj.ContenType;
            bundle = await kvc.SetSecretAsync
              (BASESECRETURI, TestName, TestValue, alltags, contentType, attribs);
            return bundle;

            //kvc.GetKeyAsync(BASESECRETURI, "");
            // Console.WriteLine("Bundle:" + bundle.Tags["Test1"].ToString());
        }
    }
}
