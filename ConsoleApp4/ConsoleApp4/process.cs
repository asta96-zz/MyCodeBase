using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class process
    {/// <summary>
     /// Uses the global discovery service to return environment instances
     /// </summary>
     /// <param name="username">The user name</param>
     /// <param name="password">The password</param>
     /// <returns>A list of Instances</returns>
     
     private static Version _ADALAsmVersion;
        public static string clientId = "51f81489-12ee-4a9e-aaae-a2591f45987d";
        public static string redirectUrl = "app://58145B91-0C36-4500-8554-080854F2AC97";
        static List<Instance> GetInstances(string username, string password)
        {

            string GlobalDiscoUrl = "https://globaldisco.crm.dynamics.com/";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", GetAccessToken(username, password,
                new Uri("https://disco.crm.dynamics.com/api/discovery/")));
            client.Timeout = new TimeSpan(0, 2, 0);
            client.BaseAddress = new Uri(GlobalDiscoUrl);

            HttpResponseMessage response =
                client.GetAsync("api/discovery/v2.0/Instances", HttpCompletionOption.ResponseHeadersRead).Result;

            if (response.IsSuccessStatusCode)
            {
                //Get the response content and parse it.
                string result = response.Content.ReadAsStringAsync().Result;
                JObject body = JObject.Parse(result);
                JArray values = (JArray)body.GetValue("value");

                if (!values.HasValues)
                {
                    return new List<Instance>();
                }

                return JsonConvert.DeserializeObject<List<Instance>>(values.ToString());
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
        private static AuthenticationParameters GetAuthorityFromTargetService(Uri targetServiceUrl)
        {
            try
            {
                // if using ADAL > 4.x  return.. // else remove oauth2/authorize from the authority
                if (_ADALAsmVersion == null)
                {
                    // initial setup to get the ADAL version 
                    var AdalAsm = System.Reflection.Assembly.GetAssembly(typeof(IPlatformParameters));
                    if (AdalAsm != null)
                        _ADALAsmVersion = AdalAsm.GetName().Version;
                }

                AuthenticationParameters foundAuthority;
                if (_ADALAsmVersion != null && _ADALAsmVersion >= Version.Parse("5.0.0.0"))
                {
                    foundAuthority = CreateFromUrlAsync(targetServiceUrl);
                }
                else
                {
                    foundAuthority = CreateFromResourceUrlAsync(targetServiceUrl);
                }

                if (_ADALAsmVersion != null && _ADALAsmVersion > Version.Parse("4.0.0.0"))
                {
                    foundAuthority.Authority = foundAuthority.Authority.Replace("oauth2/authorize", "");
                }

                return foundAuthority;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private static AuthenticationParameters CreateFromUrlAsync(Uri targetServiceUrl)
        {
            var result = (Task<AuthenticationParameters>)typeof(AuthenticationParameters)
                .GetMethod("CreateFromUrlAsync").Invoke(null, new[] { targetServiceUrl });

            return result.Result;
        }
        public static string GetParameterValueFromConnectionString(string connectionString, string parameter)
        {
            try
            {
                return connectionString.Split(';').Where(s => s.Trim().StartsWith(parameter)).FirstOrDefault().Split('=')[1];
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
        private static AuthenticationParameters CreateFromResourceUrlAsync(Uri targetServiceUrl)
        {
            var result = (Task<AuthenticationParameters>)typeof(AuthenticationParameters)
                .GetMethod("CreateFromResourceUrlAsync").Invoke(null, new[] { targetServiceUrl });
            return result.Result;
        }
        private static UriBuilder GetUriBuilderWithVersion(Uri discoveryServiceUri)
        {
            UriBuilder webUrlBuilder = new UriBuilder(discoveryServiceUri);
            string webPath = "web";

            if (!discoveryServiceUri.AbsolutePath.EndsWith(webPath))
            {
                if (discoveryServiceUri.AbsolutePath.EndsWith("/"))
                    webUrlBuilder.Path = string.Concat(webUrlBuilder.Path, webPath);
                else
                    webUrlBuilder.Path = string.Concat(webUrlBuilder.Path, "/", webPath);
            }

            UriBuilder versionTaggedUriBuilder = new UriBuilder(webUrlBuilder.Uri);
            return versionTaggedUriBuilder;
        }
        public static string GetAccessToken(string userName, string password, Uri serviceRoot)
        {
            var targetServiceUrl = GetUriBuilderWithVersion(serviceRoot);
            // Obtain the Azure Active Directory Authentication Library (ADAL) authentication context.
            AuthenticationParameters ap = GetAuthorityFromTargetService(targetServiceUrl.Uri);
            AuthenticationContext authContext = new AuthenticationContext(ap.Authority, false);
            //Note that an Azure AD access token has finite lifetime, default expiration is 60 minutes.
            AuthenticationResult authResult;

            if (userName != string.Empty && password != string.Empty)
            {

                UserPasswordCredential cred = new UserPasswordCredential(userName, password);
                authResult = authContext.AcquireTokenAsync(ap.Resource, clientId, cred).Result;
            }
            else
            {
                // Note that PromptBehavior.Always is why the UserID is aways prompted when this path is executed
                // Lookup PromptBehavior's to understand what other options exist. 
                PlatformParameters platformParameters = new PlatformParameters(PromptBehavior.Always);
                authResult = authContext.AcquireTokenAsync(ap.Resource, clientId, new Uri(redirectUrl), platformParameters).Result;
            }

            return authResult.AccessToken;
        }
    }/// <summary>
     /// Object returned from the Discovery Service.
     /// </summary>
    class Instance
    {
        public string Id { get; set; }
        public string UniqueName { get; set; }
        public string UrlName { get; set; }
        public string FriendlyName { get; set; }
        public int State { get; set; }
        public string Version { get; set; }
        public string Url { get; set; }
        public string ApiUrl { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}