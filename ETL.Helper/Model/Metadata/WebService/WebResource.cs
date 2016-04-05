using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model
{
    public class WebResource : Root
    {
        public WebResource() {
            RequestHeaders = new WebRequestHeaders();
        }
        public WebResource(WebResource resource)
        {
            BaseUrl = resource.BaseUrl;
            ResourceUrl = resource.ResourceUrl;
            RequestHeaders = resource.RequestHeaders;
            RequestMethod = resource.RequestMethod;
            RequestContent = resource.RequestContent;
            RequestResponse = resource.RequestResponse;
            ResponseContent = resource.ResponseContent;
            AuthenticationToken = resource.AuthenticationToken;
        }

        public string BaseUrl { get; set; }
        public string ResourceUrl { get; set; }
        public AuthenticationToken AuthenticationToken { get; set; }
        public WebRequestHeaders RequestHeaders { get; set; }
        public HttpMethod RequestMethod { get; set; }
        public HttpContent RequestContent { get; set; }
        public HttpResponseMessage RequestResponse { get; set; }
        public string ResponseContent { get; set; }
    }

    public class WebRequestHeaders : HttpHeaders
    {

    }
}
