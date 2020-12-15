using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;
using System.IO;

public class CRM_core : BusinessLogic
{
    public override void UseMultipleRequest()
    {
        var service = CrmConnector();
        string ManagedSolutionLocation = @"C:\Users\KM485TT\Downloads\MetadataBrowser_3_0_0_4_managed.zip";

        byte[] fileBytes = File.ReadAllBytes(ManagedSolutionLocation);

        ImportSolutionRequest impSolReq = new ImportSolutionRequest()
        {
            CustomizationFile = fileBytes,
        };
        
        //ExecuteAsyncRequest asyncReq = new ExecuteAsyncRequest()
        //{
        //    Request = impSolReq
        //};

        var asyncResp = (ImportSolutionResponse)service.Execute(impSolReq);

        //Guid asyncOperationId = asyncResp.AsyncJobId;




    }

  

    public override void pagingCookie()
    {
        throw new System.NotImplementedException();
    }
}