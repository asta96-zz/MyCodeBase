using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace Retro.CustomWorkflow
{
    public class CreateAnOrderforOpp : CodeActivity
    {

        public IOrganizationService _service;
        public IOrganizationServiceFactory _serviceFactory;
        public IWorkflowContext _context;

        [RequiredArgument]
        [ReferenceTarget("opportunity")]
        [Input("OppId")]
        public InArgument<EntityReference> OppId { get; set; }

        [ReferenceTarget("salesorder")]
        [Input("Order Lookup")]
        public InArgument<EntityReference> OrderInput { get; set; }

        [ReferenceTarget("salesorder")]
        [Output("Order")]
        public OutArgument<EntityReference> OrderId { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            _context = context.GetExtension<IWorkflowContext>();
            _serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            _service = _serviceFactory.CreateOrganizationService(_context.UserId);
            Entity order = new Entity("salesorder");
            #region Creating Salesorder
            if (_context.MessageName.Equals("create", StringComparison.OrdinalIgnoreCase))
            {
                if (_context.Depth > 1)
                {
                    return;
                }
                else
                {

                    order["name"] = "Auto Order created via CW" + DateTime.Now.ToString();
                    order["opportunityid"] = new EntityReference("opportunity", OppId.Get(context).Id);
                    Guid orderGuid = _service.Create(order);
                }
            }
            else if (_context.MessageName.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                if (_context.Depth > 1)
                {
                    return;
                }
                else
                {

                    order = _service.Retrieve("salesorder", OrderInput.Get(context).Id, new ColumnSet("name"));
                    order["name"] = "Auto Order updated via CW" + DateTime.Now.ToString();
                    _service.Update(order);

                }
            }
            this.OrderId.Set(context, new EntityReference("salesorder", order.Id));
            #endregion

        }
    }
}
