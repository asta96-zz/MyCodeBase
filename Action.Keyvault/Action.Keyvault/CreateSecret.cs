using System.Collections.Generic;

namespace Action.Keyvault
{
    public class CreateSecret
    {
        public IDictionary<string, string> KeyValue { get; set; }
        public string SecretName;
        public string ContenType;
        public string SecretValue;
    }
}