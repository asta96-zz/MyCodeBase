using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
namespace Count_of_words
{
    public class Class1:CodeActivity
    {
        [RequiredArgument]
        [Input("Input Text")]
        public InArgument<string> InputText { get; set; }

        [Output("Word Count")]
        public OutArgument<int> CountOfWords { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            this.CountOfWords.Set(
                context,
                this.InputText.Get<string>(context).Split(
                    new char[] { ' ', '\r', '\n' },
                    StringSplitOptions.RemoveEmptyEntries).Length);
        }
    }
}
