using Microsoft.Azure.KeyVault;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.WebServiceClient;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
///  CRMHelper class to read keyvault and crm connection
/// </summary>
public class CRMHelper
{
    private readonly TraceWriter logger = null;

    private string crmConnection = string.Empty;
    private string clientId = string.Empty;
    private string clientSecret = string.Empty;
    private string appClientID = string.Empty;
    private string appClientSecret = string.Empty;
    private string authority = string.Empty;
    private string resourceURL = String.Empty;

    public CRMHelper(TraceWriter logger)
    {
        this.logger = logger;
    }

    //// public CrmServiceClient ServiceClient { get; set; }
    private string CRMConnection { get; set; }
    private OrganizationWebProxyClient sdkService { get; set; }
    public IOrganizationService ServiceClient { get; set; }
    /// <summary>
    /// get crm service client
    /// </summary>
    public void GetCrmServiceClient()
    {
        if (string.IsNullOrWhiteSpace(this.crmConnection))
        {
            this.ReadKeyVault();
        }

        try
        {
            var credentials = new ClientCredential(this.appClientID, this.appClientSecret);
            var authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(this.authority);
            var result = authContext.AcquireTokenAsync(this.resourceURL, credentials).Result;
            var accesToken = result.AccessToken;
            Uri serviceUrl = new Uri(this.CRMConnection);

            using (sdkService = new OrganizationWebProxyClient(serviceUrl, false))
            {
                sdkService.HeaderToken = accesToken;

                this.ServiceClient = (IOrganizationService)sdkService != null ? (IOrganizationService)sdkService : null;
            }

        }
        catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
        {
            this.logger.Error("CRM Connection Issue: ", ex);
            throw;
        }
        catch (Exception ex)
        {
            this.logger.Error("CRM Connection Issue: ", ex);
            throw;
        }
    }

    /// <summary>
    /// Read Azure KeyVault
    /// </summary>
    public void ReadKeyVault()
    {
        try
        {
            string keyVaultUrl = string.Empty;
            string crmInstanceConnection = string.Empty;
            string appId = string.Empty;
            string appKey = string.Empty;
            string authorityURL = string.Empty;
            ////Read the  values from KeyVault Synchronously
            if (ConfigurationManager.AppSettings.AllKeys.Contains(Constants.KeyVaultUrl))
            {
                keyVaultUrl = ConfigurationManager.AppSettings[Constants.KeyVaultUrl];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains(Constants.CrmInstanceConnection))
            {
                crmInstanceConnection = ConfigurationManager.AppSettings[Constants.CrmInstanceConnection];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains(Constants.KeyVaultClientId))
            {
                this.clientId = ConfigurationManager.AppSettings[Constants.KeyVaultClientId];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains(Constants.KeyVaultClientKey))
            {
                this.clientSecret = ConfigurationManager.AppSettings[Constants.KeyVaultClientKey];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains(Constants.ResourceURL))
            {
                this.resourceURL = ConfigurationManager.AppSettings[Constants.ResourceURL];
            }
            if (ConfigurationManager.AppSettings.AllKeys.Contains(Constants.AppClientId))
            {
                appId = ConfigurationManager.AppSettings[Constants.AppClientId];
            }
            if (ConfigurationManager.AppSettings.AllKeys.Contains(Constants.AppClientKey))
            {
                appKey = ConfigurationManager.AppSettings[Constants.AppClientKey];
            }
            if (ConfigurationManager.AppSettings.AllKeys.Contains(Constants.AppClientKey))
            {
                appKey = ConfigurationManager.AppSettings[Constants.AppClientKey];
            }
            if (ConfigurationManager.AppSettings.AllKeys.Contains(Constants.AuthorityURL))
            {
                authorityURL = ConfigurationManager.AppSettings[Constants.AuthorityURL];
            }
            ThrowIf.ArgumentNull(keyVaultUrl, "Key Vault can not be null");
            ThrowIf.ArgumentNull(crmInstanceConnection, "crmInstanceConnection can not be null");
            ThrowIf.ArgumentNull(this.clientSecret, "clientSecret can not be null");
            ThrowIf.ArgumentNull(this.clientId, "clientId can not be null");

            KeyVaultClient kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(this.GetToken));
            this.CRMConnection = kv.GetSecretAsync(keyVaultUrl, crmInstanceConnection, new CancellationToken()).GetAwaiter().GetResult().Value;
            this.appClientID = kv.GetSecretAsync(keyVaultUrl, appId, new CancellationToken()).GetAwaiter().GetResult().Value;
            this.appClientSecret = kv.GetSecretAsync(keyVaultUrl, appKey, new CancellationToken()).GetAwaiter().GetResult().Value;
            this.authority = kv.GetSecretAsync(keyVaultUrl, authorityURL, new CancellationToken()).GetAwaiter().GetResult().Value;
        }
        catch (Exception ex)
        {
            this.logger.Error("Exception in keyVault Read", ex);
            throw;
        }
    }

    /// <summary>
    /// Get Token to access Azure KeyVault
    /// </summary>
    /// <param name="authority">authority</param>
    /// <param name="resource">resource</param>
    /// <param name="scope">spoce</param>
    /// <returns>access token</returns>
    private async Task<string> GetToken(string authority, string resource, string scope)
    {
        AuthenticationContext authContext = new AuthenticationContext(authority);
        ClientCredential clientCred = new ClientCredential(this.clientId, this.clientSecret);
        AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

        if (result == null)
        {
            this.logger.Error("failed to obtain Jwt Token to connect with Keyvault");
            throw new InvalidOperationException("Failed to obtain the JWT token for Key Vault");
        }

        return result.AccessToken;
    }
}
}