using ETL.Helper.Controller;
using ETL.Helper.Model;
using ETL.Services.SFDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ETLTest
{
    class QuickstartApiSample
    {
        static void Main(string[] args)
        {
            OAuthCredential credential = new OAuthCredential()
            {
                UserName = "chavez@rdacorp.com",
                Password = "M@nagua1TLnUtikGUGTzrlrJb7ULFremt",
                ApiKey = "3MVG9KI2HHAq33RzA0ERMrkC6c.nBwj0Wool3iwXCDNO4aLnRWTyjH0WaLmK4pDY9Bb5z09GblL5Xw48CHWlY",
                ApiSecret = "9185997188589143935",
                AuthenticationUrl = "https://login.salesforce.com/services/oauth2/token"
            };

            var content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", credential.ApiKey),
                new KeyValuePair<string, string>("client_secret", credential.ApiSecret),
                new KeyValuePair<string, string>("username", credential.UserName),
                new KeyValuePair<string, string>("password", credential.Password)});

            // Get Token
            WebResource resource = new WebResource()
            {
                RequestMethod = HttpMethod.Post,
                ResourceUrl = credential.AuthenticationUrl,
                RequestContent = content
            };
            /*
            resource.RequestHeaders.Add("grant_type", "password");
            resource.RequestHeaders.Add("client_id", credential.ApiKey);
            resource.RequestHeaders.Add("client_secret", credential.ApiSecret);
            resource.RequestHeaders.Add("username", credential.UserName);
            resource.RequestHeaders.Add("password", credential.Password);
            */

            RestServiceController rc = new RestServiceController();
            var newResource = rc.GetResource(resource);

            
        }

    }
}
