using System;
using CRM.Plugins.Common;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CRM.Plugins
{
    public class PreCreateQueueItemSync : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext Context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(Context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            if (Context.InputParameters.Contains("Targets") &&
                (Context.InputParameters["Target"] is Entity entity) &&
                Context.MessageName.ToUpper() == "CREATE")
            {

                Entity queueItem = (Entity)Context.InputParameters["Target"];
                if (queueItem.LogicalName != "queueitem")
                    return;

                var taggedCase = queueItem.GetAttributeValue<EntityReference>("objectid");
                if (taggedCase == null)
                    return;


                // No related history found
                var queryHistoryXml = string.Format(Query.GetWorkHistoryByCase, taggedCase.Id.ToString());
                EntityCollection relatedHistory = service.RetrieveMultiple(new FetchExpression(queryHistoryXml));
                if (relatedHistory == null || relatedHistory.Entities.Count == 0)
                    return;

                // foreach (Entity history in relatedHistory.Entities)
                Entity history = relatedHistory.Entities[0];
                tracingService.Trace("plugin:Getting the data");
                history["new_queue"] = queueItem.GetAttributeValue<EntityReference>("queueid");
               // history["new_times"] = queueItem.GetAttributeValue<EntityReference>("new_timespendoncasequeue");
                history["new_totaltimespendoncase"] = queueItem.GetAttributeValue<EntityReference>("new_totaltimespendoncase");
                history["new_timespendbycurrentuser"] = queueItem.GetAttributeValue<EntityReference>("new_timespendbycurruser");
                history["new_timespendbycaseinqueue"] = queueItem.GetAttributeValue<EntityReference>("new_timespendoncasequeue");
                service.Update(history);


            }
        }


    }
}
