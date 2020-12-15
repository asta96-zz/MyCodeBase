using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;

namespace Retro.CustomWorkflow
{
    public class UpdateName : CodeActivity
    {
        [RequiredArgument]
        [Input("Opportunity Name")]
        public InArgument<string> OppName { get; set; }

        [Output("Name")]
        public OutArgument<string> Name { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string finalName = this.OppName.Get(context) + " updated by Custom workflow" + DateTime.Now.ToString();
            this.Name.Set(context, finalName);          
        }
    }
}
