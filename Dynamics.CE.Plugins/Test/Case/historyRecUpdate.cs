using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;

namespace CRM.Plugins.Test.Case
{
    public class historyRecUpdate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext Context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService Service = serviceFactory.CreateOrganizationService(Context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            try
            {
                Entity PstImagecase = null;
                EntityReference owner = null;
                if (Context.PostEntityImages.Contains("PImage") && Context.PostEntityImages["PImage"] != null)
                {
                    PstImagecase = (Entity)Context.PostEntityImages["PImage"];
                }
                //if (Context.PrimaryEntityName == "incident")
               // {

                    Entity CaseRecord = Service.Retrieve(Context.PrimaryEntityName, Context.PrimaryEntityId, new ColumnSet(true));
                    tracingService.Trace("getting entity recoed");
                    QueryExpression query = new QueryExpression()
                    {
                        EntityName = "new_queueworkhistory",
                        ColumnSet = new ColumnSet(true)
                    };
                    ConditionExpression cond = new ConditionExpression("new_case", ConditionOperator.Equal, Context.PrimaryEntityId);
                    FilterExpression filter = new FilterExpression();
                    filter.AddCondition(cond);
                    query.Criteria = filter;
                    EntityCollection results = Service.RetrieveMultiple(query);
                    tracingService.Trace("recoeds");
                    if (results != null && results.Entities.Count > 0)
                    {
                        //update history entity
                        Entity ent = new Entity()
                        {
                            LogicalName = "new_queueworkhistory",
                            Id = results[0].Id

                        };
                        ent.Attributes["dev_timespentbycurrentuse"] = results[0].Contains("new_timespendbycurrentuser") ? results[0].GetAttributeValue<Decimal>("new_timespendbycurrentuser") :new Decimal(0);
                        ent.Attributes["dev_totaltimespendoncase"] = results[0].Contains("new_totaltimespendoncase") ? results[0].GetAttributeValue<Decimal>("new_totaltimespendoncase") : new Decimal(0);
                        ent.Attributes["dev_timespent"] = results[0].Contains("new_timespendbycaseinqueue") ? results[0].GetAttributeValue<Decimal>("new_timespendbycaseinqueue") : new Decimal(0);
                        ent.Attributes["statuscode"] = new OptionSetValue(2);
                        ent.Attributes["statecode"] = new OptionSetValue(1);
                        tracingService.Trace("Records Updated");
                        Service.Update(ent);

      
                        //Create the new history record
                        Entity NewHistory = new Entity()
                        {
                            LogicalName = "new_queueworkhistory"
                        };
                        NewHistory["new_case"] = new EntityReference(Context.PrimaryEntityName, Context.PrimaryEntityId);
                        
                        if (PstImagecase.Attributes.Contains("ownerid") && PstImagecase.Attributes["ownerid"] != null)
                        {
                           owner = (EntityReference)PstImagecase.Attributes["ownerid"];
                            tracingService.Trace(owner.Name);
                        }
                       
                        Guid NewHistoryID=Service.Create(NewHistory);
             
                        tracingService.Trace("Record Created"+NewHistoryID);
                        Entity enthistory = new Entity()
                        {
                            LogicalName = "new_queueworkhistory",
                            Id = NewHistoryID

                        };
                        enthistory["ownerid"] = new EntityReference(owner.LogicalName, owner.Id);
                        Service.Update(enthistory);
                    }
               // }
                //else if (Context.PrimaryEntityName == "queueitem")
                    
                //{
                  //  Entity qItem = new Entity()
                    //{
                      //  LogicalName = "new_queueworkhistory"
                    //};
                    //if (qItem.Attributes["enteredon"] != null) 
                   // {
                     //   qItem.Attributes["enteredon"] = qItem.Contains("enteredon") ? qItem.GetAttributeValue<DateTime>("enteredon") :DateTime.Now;
                       // qItem.Attributes["new_queue"] = qItem.Contains("queueid") ?qItem.GetAttributeValue<EntityReference>("queueid").Id : Guid.Empty;
                        //tracingService.Trace("queue item record created");
                        ////Service.Create(qItem);
                   // }

               // }

            
                
            }
            catch (Exception ex)
            {

                throw new InvalidPluginExecutionException("error", ex);
            }

        }

    }
}