using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
//using System.Text.Json;
using System.Threading.Tasks;
namespace Azure_key_restapi
{
    class AzureProcessor
    {

        //const string CLIENTSECRET = "_-IcUrkh6Mz2317-4F_ryDw3_8.VKUJzjg";
        //const string CLIENTID = "d0cb4b3d-b227-4d47-b3f9-ce5b9258b037";
        //const string BASESECRETURI =
        //    "https://dev-kv-001.vault.azure.net";

        private static HttpClient client = new HttpClient();
        private static string LoginToken = string.Empty;
      //  private static readonly JsonSerializerOptions Options = new JsonSerializerOptions();
        public string GetTokenAsync()
        {
            string _token = string.Empty;
            client.DefaultRequestHeaders.Accept.Clear();
            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            // client.DefaultRequestHeaders.
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("grant_type", "client_credentials");
            //client.DefaultRequestHeaders.Add("client_id", "QsiDE2_F~2~6yNwv3u_082xCX5Zr6~~KV-");
            //client.DefaultRequestHeaders.Add("client_secret", "c1e865b6-c86e-4a75-91b0-f28791735c76");
            //client.DefaultRequestHeaders.Add("scope", "https://vault.azure.net/.default");

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
                token =  Deserialize<TokenResponse>(response);
            }
            return token.access_token;
        }

        public  string GetSecretAsync()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            LoginToken =  GetTokenAsync();
            string secretUrl = "https://dev-kv-001.vault.azure.net/secrets/sec2?api-version=2016-10-01";
            string authToken = LoginToken;//@"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCIsImtpZCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiJodHRwczovL3ZhdWx0LmF6dXJlLm5ldCIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzFmMzI5ZThiLWY4M2ItNDA5My1iNTQyLWQ0OTMzOGUxYWQ1Zi8iLCJpYXQiOjE2MDYyMDkxMTgsIm5iZiI6MTYwNjIwOTExOCwiZXhwIjoxNjA2MjEzMDE4LCJhaW8iOiJFMlJnWU9DNnZ1YVg3cDBzazdaYzRibW42NmN3QWdBPSIsImFwcGlkIjoiZDBjYjRiM2QtYjIyNy00ZDQ3LWIzZjktY2U1YjkyNThiMDM3IiwiYXBwaWRhY3IiOiIxIiwiaWRwIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvMWYzMjllOGItZjgzYi00MDkzLWI1NDItZDQ5MzM4ZTFhZDVmLyIsIm9pZCI6ImU1OTcwM2U3LTc1ZmItNDRlYS04ZWU1LWNiYjI5NGI5NTQyOCIsInJoIjoiMC5BQUFBaTU0eUh6djRrMEMxUXRTVE9PR3RYejFMeTlBbnNrZE5zX25PVzVKWXNEZHhBSGMuIiwic3ViIjoiZTU5NzAzZTctNzVmYi00NGVhLThlZTUtY2JiMjk0Yjk1NDI4IiwidGlkIjoiMWYzMjllOGItZjgzYi00MDkzLWI1NDItZDQ5MzM4ZTFhZDVmIiwidXRpIjoiNVYzVmNBSW5wVVN2WFhoWF9UUjRBQSIsInZlciI6IjEuMCJ9.s1oukrTGodTwyBajugoSAFVBMAO3jqCiAX3k07Dw_FiOj370G5EYBxT-vmoYSWjANrLykgt9Y3FPDA6BIGh4Dqr2UDo3qN5UJL-mhMp-EURyaqZ7K_haL7iaErVEpM_gYpOAABj7gTv0LI0keGI28B5RTouPnPpRBhGzHSSB8M-Ms3-Rzn_8WSmzBfSPV5_RpC0TcWr3CdJF4fE4DUmoT0EL1_QbhVadF-7PG8bNsWzouPX1BoriZRf8bqdaSptvX6DB68PvpkVipO46xzMKKvd-zPARJ7xa8PK9-exiq5K3yPLheFhoTbHF5G1zE5sAbIn3YTbsD3S-qg_7V3SeQw";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(secretUrl).Result;
            SecretResponse secretResponse = null;
            if (response.IsSuccessStatusCode)
            {
                secretResponse =  Deserialize<SecretResponse>(response); //JsonSerializer.Deserialize<SecretResponse>(jsonResponse);
            }
            System.Console.WriteLine(secretResponse.id);
            System.Console.WriteLine(secretResponse.value);
            return secretResponse.value;
        }

        //private static T Deserialize<T>(HttpResponseMessage response)
        //{
        //    var contentStream = response.Content.ReadAsStreamAsync();
        //    var result = JsonSerializer.DeserializeAsync<T>(contentStream.Result, Options).Result;
        //    return result;
        //}
        private static T Deserialize<T>(HttpResponseMessage response)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            var contentStream = response.Content.ReadAsStreamAsync().Result;
            var result = (T)serializer.ReadObject(contentStream);//JsonSerializer.DeserializeAsync<T>(contentStream.Result, Options).Result;
            return result;
        }
    }

    [DataContract]
    public class TokenResponse
    {
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public int expires_in { get; set; }
        [DataMember]
        public int ext_expires_in { get; set; }
        [DataMember]
        public string access_token { get; set; }
    }

    [DataContract]
    public class SecretResponse
    {
        [DataMember]
        public string value { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public Attributes attributes { get; set; }
    }

    [DataContract]
    public class Attributes
    {
        [DataMember]
        public bool enabled { get; set; }
        [DataMember]
        public int created { get; set; }
        [DataMember]
        public int updated { get; set; }
        [DataMember]
        public string recoveryLevel { get; set; }
    }
}
