using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRM_playField
{
    class Program
    {
        private static IOrganizationService _orgService;
        static void Main(string[] args)
        {
           // retrieveForms();
           // createForm();
            ExecuteMultipleRequest_singleTransaction.method();
        }

        private static void createForm()
        {
            try
            {
                crmService obj = new crmService();
                CrmServiceClient conn = obj.CrmConnector();
                _orgService = (IOrganizationService)conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;

                Entity form1 = new Entity("systemform");
                form1.Attributes["name"] = "test_form";
                
                Guid formid = _orgService.Create(form1);
            }
            catch(Exception ex)
            {

            }
        }

        public static void retrieveForms()
        {
            try
            {
                crmService obj = new crmService();
                CrmServiceClient conn = obj.CrmConnector();
                _orgService = (IOrganizationService)conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;
                EntityCollection forms = new EntityCollection();

                //Query Expression Object

                QueryExpression queryExp = null;

                queryExp = new QueryExpression

                {

                    EntityName = "systemform",

                    ColumnSet = new ColumnSet("formid", "type", "formxml", "name")

                };

                FilterExpression conditions = new FilterExpression(LogicalOperator.And);

                conditions.Conditions.Add(new ConditionExpression("name", ConditionOperator.Equal, "opportunity"));

                queryExp.Criteria.AddFilter(conditions);

                //Retrieve form collection

                forms = _orgService.RetrieveMultiple(queryExp);

                //Create formXML(XDocument) and DisplayCondition(XElement) objects
                //parse formXML into XDocument

                XDocument formXML = XDocument.Parse(forms.Entities[0].Attributes["formxml"].ToString());
                XElement displayCondition = formXML.Descendants("DisplayConditions").FirstOrDefault();
                XElement displayConditionAttribute;

            }
            catch(Exception ex)
            {
                Console.Write("\n Exception" + ex);
            }
        }

    }
}
