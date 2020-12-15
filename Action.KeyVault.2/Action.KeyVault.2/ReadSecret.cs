using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk.Workflow.Activities;
using System.Activities;
using System.Net;

namespace Action.KeyVault._2
{
    public class ReadSecret : CodeActivity
    {
        
        [Input("Secret Name")]
        public InArgument<string> SecretName { get; set; }
        [RequiredArgument]
        [Output("Secret Value")]
        public OutArgument<string> SecretValue { get; set; }



        //[RequiredArgument]
        //[Output("Published Version")]
        //public OutArgument<string> PublishedVersion { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                
                AzureProcessor obj = new AzureProcessor();
                string secretName = string.Empty;
                secretName = string.IsNullOrEmpty(SecretName.Get(context))? "sec2" : SecretName.Get(context);
                string secretValue =  obj.GetTokenHttpRequest();
                SecretValue.Set(context, secretValue);
            }
            catch (Exception ex)
            {
                SecretValue.Set(context, ex.Message);
                throw new InvalidWorkflowException("exception in action" + ex.Message, ex.InnerException);
            }
        }
    }
}

