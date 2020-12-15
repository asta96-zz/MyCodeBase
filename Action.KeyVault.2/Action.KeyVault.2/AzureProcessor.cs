using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Action.KeyVault._2
{
    class AzureProcessor
    {

        const string CLIENTSECRET = "_-IcUrkh6Mz2317-4F_ryDw3_8.VKUJzjg";
        const string CLIENTID = "d0cb4b3d-b227-4d47-b3f9-ce5b9258b037";
        const string BASESECRETURI ="https://dev-kv-001.vault.azure.net";
        const string Scope = "https://vault.azure.net/.default";
        const string GrantType = "client_credentials";
        const string TenantId = "1f329e8b-f83b-4093-b542-d49338e1ad5f";

      //  private static HttpClient client = new HttpClient();
        private static string LoginToken = string.Empty;
        //private static readonly JsonSerializerOptions Options = new JsonSerializerOptions();
        /*public async static Task<AzureAccessToken> CreateOAuthAuthorizationToken(string cId, string cs, string resourceId, string tenantId)
        {
            try
            {
                AzureAccessToken token = new AzureAccessToken();
                string oauthUrl = string.Format("https://login.microsoftonline.com/15025a36-9c10-4300-8462-4fe1970f0a64/oauth2/token", tenantId);
                string reqBody = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&resource={2}", Uri.EscapeDataString(cId), Uri.EscapeDataString(cs), Uri.EscapeDataString(resourceId));
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(reqBody);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (HttpResponseMessage response = await client.PostAsync(oauthUrl, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AzureAccessToken));
                        Stream json = await response.Content.ReadAsStreamAsync();
                        token = (AzureAccessToken)serializer.ReadObject(json);
                    }
                    return token;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }*/
        /*
        public static HttpWebResponse GetWebResponsPatch(IOrganizationService service, Object ob, string url, DataContractJsonSerializer js, string resID)
        {
            System.IO.MemoryStream msObj = new System.IO.MemoryStream();
            js.WriteObject(msObj, ob);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);
            string json = sr.ReadToEnd();
            json = json.Replace("odataEtag", "@odata.etag");

            sr.Close();
            msObj.Close();
            string resourceId = resID;
            string tenantId = "cc102217-8c68-4e3a-9ed1-b1582f03ecd6";
            string cId = "7fe719e1-4354-419e-bc33-fad6429a0d3f";
            string cs = "r8FzfG+WMmMGHnd80YlVsoJd5zdBx5fp8dHLoLtCrHg=";
            string authority = "https://login.microsoftonline.com/" + tenantId;

            //Manually create the auth token          
            Task<AzureAccessToken> token = CreateOAuthAuthorizationToken(
            cId,
            cs,
            resourceId,
            tenantId);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            try
            {
                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(json);
                //Set HttpWebRequest properties
                httpWebRequest.Method = "PATCH";
                httpWebRequest.ContentLength = bytes.Length;
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + token.Result.access_token);
                httpWebRequest.KeepAlive = false;
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    //Writes a sequence of bytes to the current stream 
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();//Close stream
                }
                //Sends the HttpWebRequest, and waits for a response.
                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //  throw new InvalidPluginExecutionException("Kunal");
                return httpWebResponse;
            }
            catch (WebException ex)
            {
                //Entity integrationLog = new Entity("edu_integrationlog");
                //integrationLog["edu_enquiry"] = new EntityReference("lead", leadObj.Id);
                //integrationLog["edu_message"] = "Error in F&O Integration." + ex.Message + ".Request Object:" + json + ".Token:" + token.Result.access_token;
                //integrationLog["edu_reopenkitsellingrequestfail"] = true;
                //service.Create(integrationLog);
                
                var response = ex.Response as HttpWebResponse;
                if ((int)response.StatusCode != 500)
                {
                    using (Stream responseStream = (ex.Response).GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream);
                        var errorresponse = reader.ReadToEnd();
                        var ms = new MemoryStream(Encoding.Unicode.GetBytes(errorresponse));
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<ExceptionResponse>));
                        List<ExceptionResponse> exresponse = (List<ExceptionResponse>)serializer.ReadObject(ms);
                        throw new InvalidPluginExecutionException("Error in  Integration." + errorresponse.ToString());
                    }
                }
                else
                {
                    throw new InvalidPluginExecutionException("Error in  Integration." + ex.Message + ".Request Object:" + json + ".Token:" + token.Result.access_token);
                }
            }



        }
        */


        public string GetTokenHttpRequest()
        {
            string token = "";
            StringBuilder sbPostData = new StringBuilder();

            sbPostData.AppendFormat("client_id={0}", CLIENTID);
            sbPostData.AppendFormat("&scope={0}", "https://graph.microsoft.com/.default");
            sbPostData.AppendFormat("&client_secret={0}", CLIENTSECRET);
            sbPostData.AppendFormat("&grant_type=client_credentials");
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://login.microsoftonline.com/1f329e8b-f83b-4093-b542-d49338e1ad5f/oauth2/v2.0/token?");
            req.ContentType = "application/x-www-form-urlencoded";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(sbPostData.ToString());
            req.ContentLength = data.Length;
            req.Method = "POST";

            using (Stream stream = req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            if (res.StatusCode == HttpStatusCode.OK || res.StatusCode == HttpStatusCode.Accepted)
            {
                Stream rStream = res.GetResponseStream();
                TokenResponse objTokenResponse = new TokenResponse();
                token = Deserialize<TokenResponse>(rStream).access_token;

                using (StreamReader sr = new StreamReader(rStream))
                {
                    string line = sr.ReadToEnd();
                }
            }


            return token;
        }
        private static T Deserialize<T>(Stream response)
        {

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            var result = (T)serializer.ReadObject(response);//JsonSerializer.DeserializeAsync<T>(contentStream.Result, Options).Result;
            return result;
        }












        /*
                public string GetTokenAsync()
                {
                    string _token = string.Empty;
                    client.DefaultRequestHeaders.Accept.Clear();
                    var formcontext = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string,string>("grant_type", "client_credentials"),
                        new KeyValuePair<string,string>("client_id", "d0cb4b3d-b227-4d47-b3f9-ce5b9258b037"),
                        new KeyValuePair<string,string>("client_secret", "_-IcUrkh6Mz2317-4F_ryDw3_8.VKUJzjg"),
                        new KeyValuePair<string,string>("scope", "https://vault.azure.net/.default")
                    });
                    HttpResponseMessage response = client.PostAsync("https://login.microsoftonline.com/1f329e8b-f83b-4093-b542-d49338e1ad5f/oauth2/v2.0/token", formcontext).Result;
                    var x = response.Content.ReadAsStringAsync();
                    TokenResponse token = null;
                    if (response.IsSuccessStatusCode)
                    {
                        token = Deserialize<TokenResponse>(response);
                    }
                    return token.access_token;
                }*/
        /* public string GetSecretAsync(string SecretName)
         {
             try
             {
                 client = new HttpClient();
                 client.DefaultRequestHeaders.Accept.Clear();
                 LoginToken = GetTokenHttpRequest();

                // Task<AzureAccessToken> token = CreateOAuthAuthorizationToken(CLIENTID, CLIENTSECRET, Scope, TenantId);
                 string secretUrl = string.Format("https://dev-kv-001.vault.azure.net/secrets/{0}?api-version=2016-10-01", SecretName);
                 string authToken = @"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCIsImtpZCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiJodHRwczovL3ZhdWx0LmF6dXJlLm5ldCIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzFmMzI5ZThiLWY4M2ItNDA5My1iNTQyLWQ0OTMzOGUxYWQ1Zi8iLCJpYXQiOjE2MDYyOTkzODUsIm5iZiI6MTYwNjI5OTM4NSwiZXhwIjoxNjA2MzAzMjg1LCJhaW8iOiJFMlJnWUxqNDJhTlk5L0RmZ3EvL3Y0dmN0MXBzQ3dBPSIsImFwcGlkIjoiZDBjYjRiM2QtYjIyNy00ZDQ3LWIzZjktY2U1YjkyNThiMDM3IiwiYXBwaWRhY3IiOiIxIiwiaWRwIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvMWYzMjllOGItZjgzYi00MDkzLWI1NDItZDQ5MzM4ZTFhZDVmLyIsIm9pZCI6ImU1OTcwM2U3LTc1ZmItNDRlYS04ZWU1LWNiYjI5NGI5NTQyOCIsInJoIjoiMC5BQUFBaTU0eUh6djRrMEMxUXRTVE9PR3RYejFMeTlBbnNrZE5zX25PVzVKWXNEZHhBSGMuIiwic3ViIjoiZTU5NzAzZTctNzVmYi00NGVhLThlZTUtY2JiMjk0Yjk1NDI4IiwidGlkIjoiMWYzMjllOGItZjgzYi00MDkzLWI1NDItZDQ5MzM4ZTFhZDVmIiwidXRpIjoiQXF6V0JhbzBKVXFwZEs5elBXLTVBQSIsInZlciI6IjEuMCJ9.fKZfq4MbNlQctUTqHBKCKJMNdSN9XWvCe1ibkIBj9ZP9Y9oOwuJtrO4xe-LbqIytfmT5Rf1moqGiRRXXaiPE0WXhFdWceq-VmMMqkLs7EDsOvCLdhK1lZGFxxLUtWk1e4c_BPElVPs9b-XGMg45kS9Dg8KLQnGMyPyEFeObqHRtTp44og-R-jd506bQg6DNW9_NR-rDpyv4EC20If8s8gCwnoDQx1e9a1sG6aAzcNGd_hjjgJ8F4L47d31JjNVLTJ2hpXHr35EMQphSD46xuljKampmjSu2sM8Mp0YN7DCTRCBdwWOpVDaVNSdUphhO8h8LZauErY--0vaOcABwVvw";
                 client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                 client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                 ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                 HttpResponseMessage response = client.GetAsync(secretUrl).Result;
                 SecretResponse secretResponse = null;
                 if (response.IsSuccessStatusCode)
                 {
                     secretResponse = Deserialize<SecretResponse>(response); //JsonSerializer.Deserialize<SecretResponse>(jsonResponse);
                 }
                 else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                 {
                     secretResponse = new SecretResponse() { value = response.Content.ReadAsStringAsync().ToString() };
                 }
                 System.Console.WriteLine(secretResponse.id);
                 System.Console.WriteLine(secretResponse.value);
                 return secretResponse.value;
             }
             catch (Exception ex)
             {

                 throw ex;
             }
         }

         private static T Deserialize<T>(HttpResponseMessage response)
         {
             DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
             var contentStream = response.Content.ReadAsStreamAsync().Result;
             var result = (T)serializer.ReadObject(contentStream);//JsonSerializer.DeserializeAsync<T>(contentStream.Result, Options).Result;
             return result;
         }*/
    }
}
