using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Action.KeyVault._2
{
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
