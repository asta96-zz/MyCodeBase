using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CRM_playField
{
   public static class ExecuteMultipleRequest_singleTransaction
    {
        private static IOrganizationService _orgService;

        public static void method()
        {// Create an ExecuteTransactionRequest object.
            crmService obj = new crmService();
            CrmServiceClient conn = obj.CrmConnector();
            _orgService = (IOrganizationService)conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;


            var requestToCreateRecords = new ExecuteTransactionRequest()
            {
                // Create an empty organization request collection.
                Requests = new OrganizationRequestCollection(),
                ReturnResponses = true
            };

            // Create several (local, in memory) entities in a collection. 
            Entity o1 = new Entity("salesorder");
            o1.Attributes["name"] = "test1 order";
            Entity opp1 = new Entity("opportunity");
            opp1.Attributes["name"] = "test1 opp";
            EntityCollection collection = new EntityCollection();
            collection.Entities.Add(opp1);
            collection.Entities.Add(o1);


            // Add a CreateRequest for each entity to the request collection.
            foreach (var entity in collection.Entities)
            {
                CreateRequest createRequest = new CreateRequest { Target = entity };
                requestToCreateRecords.Requests.Add(createRequest);
            }

            // Execute all the requests in the request collection using a single web method call.
            try
            {
                crmService service = new crmService();
                var responseForCreateRecords =
                    (ExecuteTransactionResponse)_orgService.Execute(requestToCreateRecords);

                int i = 0;
                // Display the results returned in the responses.
                foreach (var responseItem in responseForCreateRecords.Responses)
                {
                    if (responseItem != null)
                        Console.WriteLine("Created " + ((EntityReference)requestToCreateRecords.Requests[i].Parameters["Target"]).Name
                            + " with record id as " + responseItem.Results["id"].ToString());
                    i++;
                }
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine("Create request failed for the account{0} and the reason being: {1}",
                    ((ExecuteTransactionFault)(ex.Detail)).FaultedRequestIndex + 1, ex.Detail.Message);
                throw;
            }
        }
    }
}
