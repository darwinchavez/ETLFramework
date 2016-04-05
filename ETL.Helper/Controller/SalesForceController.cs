using ETL.Helper.Model.Metadata;
using ETL.Services.SFDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ETL.Helper.Model;
using System.Net.Http;

namespace ETL.Helper.Controller
{
    public class SalesForceController : RestServiceController, IEntityController<Entity>
    {
        public EntityCatalog<Entity> GetEntityCatalog(IMetadataRestriction metadataRestriction = null)
        {
            var loginResult = Login("chavez@rdacorp.com", "P@ssw0rd", "BQrx2UHuiMCqy0yrCGIfvqgge");
            string[] objectNames = new string[] { "Account", "Contract" };
            var objsDetail = GetObjectsDetail(objectNames);

            EntityCatalog<Entity> catalog = new EntityCatalog<Entity>();
            var entities = from obj in objsDetail
                           let entityName = new EntityName(obj.name, "")
                           select new Entity()
                           {
                               Name = entityName,
                               Properties = from f in obj.fields
                                            let fieldName = new MetadataName(f.name, "")
                                            let columnSchema = new SalesForceColumnSchema(f)
                                            let dataType = new SalesForceDataType(columnSchema)
                                            select new EntityProperty(fieldName, dataType)
                                            {
                                                HasDefault = !string.IsNullOrEmpty(f.defaultValueFormula)
                                                ,DefaultValue = f.defaultValueFormula
                                                ,IsAutoIncrement = f.autoNumber
                                                ,IsKey = (f.type.ToString() == "id") // Need to verify
                                                //,IsNullable = f.req
                                                ,IsReadOnly = !f.updateable
                                                ,IsUnique = f.unique
                                            }
                           };

            return catalog;
        }

        #region SF Rest Service

        public override AuthenticationToken GetAuthorizationToken(OAuthCredential credential)
        {
            var requestContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", credential.ApiKey),
                new KeyValuePair<string, string>("client_secret", credential.ApiSecret),
                new KeyValuePair<string, string>("username", credential.UserName),
                new KeyValuePair<string, string>("password", credential.Password)
            });

            // Get Token
            WebResource resource = new WebResource()
            {
                RequestMethod = HttpMethod.Post,
                ResourceUrl = credential.AuthenticationUrl,
                RequestContent = requestContent
            };

            var tokenResource = GetResource(resource);

            AuthenticationToken token = new AuthenticationToken();
            return token;
        }

        #endregion

        #region SF WebService

        private static SoapClient client; // for API endpoint
        private static SessionHeader header;
        private static EndpointAddress endpoint;
        private string loginUrl = @"https://login.salesforce.com/services/Soap/c/34.0";

        private void SetVersion()
        {
            PackageVersion vh = new PackageVersion();
            
        }

        private SoapClient CreateSoapClient(string endPointUrl)
        {
            if (string.IsNullOrEmpty(endPointUrl))
                throw new ArgumentNullException("endPointUrl");

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Name = "SoapBinding";
            binding.SendTimeout = new TimeSpan(0, 5, 0);
            binding.MaxReceivedMessageSize = 20000000;
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            var client = new SoapClient(binding, new EndpointAddress(endPointUrl));
            
            return client;
        }

        private DescribeSObjectResult[] GetObjectsDetail(string[] objectNames)
        {
            if (objectNames == null)
                throw new ArgumentNullException("objectNames");

            // Call describeSObjects() passing in an array with one object type name 
            DescribeSObjectResult[] dsrArray;
            client.describeSObjects(header, null, null, objectNames, out dsrArray);

            return dsrArray;
        }

        private LoginResult Login(string userName, string password, string securityToken)
        {
            SoapClient loginClient = CreateSoapClient(loginUrl);
            LoginResult lr = loginClient.login(null, userName, password + securityToken);

            // Check if the password has expired 
            if (lr.passwordExpired)
            {
                throw new Exception("An error has occurred. Your password has expired.");
            }

            // On successful login, cache session info and API endpoint info
            endpoint = new EndpointAddress(lr.serverUrl);
            header = new SessionHeader();
            header.sessionId = lr.sessionId;

            // Create and cache an API endpoint client
            client = CreateSoapClient(lr.serverUrl);
            return lr;
        }
        private void Logout()
        {
            if (client != null)
                client.logout(header);
        }

        #endregion
    }
}
