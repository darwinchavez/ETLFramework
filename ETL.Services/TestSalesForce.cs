using ETL.Services.SFDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Services
{
    public class TestSalesForce
    {
        public static SoapClient CreateWebServiceInstance(string loginUrl)
        {
            if (string.IsNullOrEmpty(loginUrl))
                loginUrl = "https://login.salesforce.com/services/Soap/c/34.0";

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Name = "SoapBinding";
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            var client = new SoapClient(binding, new EndpointAddress(loginUrl));
            return client;
        }

        public void Login()
        {
            using (SoapClient loginClient = new SoapClient("Soap"))
            {
                //set account password and account token variables
                string sfdcPassword = "P@ssw0rd";
                string sfdcToken = "<token>";

                //set to Force.com user account that has API access enabled
                string sfdcUserName = "chavez@rdacorp.com";

                //create login password value which combines password and token
                string loginPassword = sfdcPassword + sfdcToken;

                //call Login operation from Enterprise WSDL
                LoginResult result =
                    loginClient.login(
                    null,           //LoginScopeHeader
                    sfdcUserName,   //username
                    loginPassword); //password

                //get response values
                var sessionId = result.sessionId;
                var serverUrl = result.serverUrl;

                //print response values
                Console.WriteLine(string.Format("The session ID is {0} and server URL is {1}", sessionId, serverUrl));
                Console.WriteLine("");
                Console.WriteLine("Press [Enter] to continue ...");
                Console.ReadLine();
            }

        }
    }
}
