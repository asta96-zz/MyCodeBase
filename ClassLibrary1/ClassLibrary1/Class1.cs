using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk;
using System.Activities;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace ClassLibrary1
{
    public class CreateOrderOPP : CodeActivity
    {
        [RequiredArgument]
        [Input("Base Order")]
        public InArgument<Guid> BaseOrder { get; set; }
        [Output("IsSucces")]
        public OutArgument<bool> IsSuccess { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                ITracingService tracingService = context.GetExtension<ITracingService>();
                IWorkflowContext workflow = context.GetExtension<IWorkflowContext>();
                IOrganizationServiceFactory serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
                IOrganizationService service = serviceFactory.CreateOrganizationService(workflow.UserId);
                tracingService.Trace("assembly invoked");
                //string fetchXml = $@"<fetch top='50' >
                //                  <entity name='salesorder' >
                //                    <attribute name='opportunityidname' />
                //                    <attribute name='opportunityid' />
                //                    <filter type='and' >
                //                      <condition attribute='salesorderid' operator='eq' value='{BaseOrder.ToString()}' />
                //                    </filter>
                //                  </entity>
                //                </fetch>";
                //EntityCollection collection = service.RetrieveMultiple(new FetchExpression(fetchXml));
                //create opp
                Entity opp = new Entity("opportunity");
                opp["name"] = "Dinesh Test OPP";
                Guid OppId = service.Create(opp);
                tracingService.Trace("Opportunity created with ID:" + OppId);
                //create order
                Entity ord = new Entity("salesorder");
                ord["name"] = "Dinesh Test Ord";
                ord["opportunityid"] = new EntityReference("opportunity", OppId);
                Guid ordId = service.Create(ord);
                tracingService.Trace("Order created with ID:" + ordId);
                tracingService.Trace("exiting assemble");
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }



        }
    }
}
