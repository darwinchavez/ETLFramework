using ETL.Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Controller
{
    public class RestServiceController
    {
        public async Task<WebResource> GetResource(WebResource resource)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");

            var webRequest = new HttpRequestMessage()
            {
                Method = resource.RequestMethod,
                RequestUri = new Uri(resource.ResourceUrl),
                Content = resource.RequestContent
            };

            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(resource.BaseUrl))
                    client.BaseAddress = new Uri(resource.BaseUrl);

                if (resource.RequestHeaders.Count() > 0)
                {
                    client.DefaultRequestHeaders.Clear();
                    foreach (var header in resource.RequestHeaders)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                HttpResponseMessage response = await client.SendAsync(webRequest).ConfigureAwait(false);
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                WebResource newResource = new WebResource(resource);
                newResource.RequestResponse = response;
                newResource.ResponseContent = responseContent;

                return newResource;
            }
        }

        public virtual AuthenticationToken GetAuthorizationToken(OAuthCredential credential)
        {
            return null;
        }
    }
}
