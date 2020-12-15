using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Sdk;

namespace Powert.Plugin
{
    public class Plugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                if (context.Depth < 2)
                {
                    tracing.Trace("inside contains target check");
                    Entity opp = (Entity)context.InputParameters["Target"];
                    opp["new_pluginexecutiontime"] = DateTime.UtcNow.AddHours(5.5).ToString();
                    tracing.Trace("new_pluginexecutiontime:" + opp["new_pluginexecutiontime"].ToString());
                    if (context.PreEntityImages.Contains("Preimage")) {
                        tracing.Trace("contains preimage");
                    }
                    if (context.PostEntityImages.Contains("Postimage"))
                    {
                        tracing.Trace("contains Postimage");
                    }
                    Entity preimage = (Entity)context.PreEntityImages["Preimage"];
                    Entity postimage = (Entity)context.PostEntityImages["Postimage"];
                    opp["new_preimagepluginexecutiontime"] = preimage.Attributes.Contains("new_pluginexecutiontime")? preimage["new_pluginexecutiontime"]:"no preimage value";
                    opp["new_postimage"] = postimage.Attributes.Contains("new_pluginexecutiontime") ? opp["new_pluginexecutiontime"] : "no postimage value";
                    tracing.Trace("before updating");
                    service.Update(opp);
                    tracing.Trace("after updating");
                    tracing.Trace("Exiting check");
                }
                
            }
        }
    }
}
