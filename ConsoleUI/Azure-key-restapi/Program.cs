using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure_key_restapi
{
    class Program
    {
        static void Main(string[] args)
        {
            //AzureProcessor obj = new AzureProcessor();
            // obj.GetSecretAsync();
            ////Console.WriteLine(obj.GetSecretAsync());
            //Console.Read();

            StringBuilder builder = new StringBuilder();
            builder.Append(@"\");
            builder.Append("/");
            string s = builder.ToString();

            string a = @"\/";
            string c = a;
            Console.WriteLine(c);
            Console.WriteLine(a);
        }
    }
}
