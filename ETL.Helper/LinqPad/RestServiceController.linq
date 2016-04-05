<Query Kind="Statements">
  <Reference Relative="..\bin\Debug\ETL.Helper.dll">D:\Projects\OpenSource\ETLFramework\ETL.Helper\bin\Debug\ETL.Helper.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <GACReference>Microsoft.SharePoint.Client, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.SharePoint.Client.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.SharePoint.Client.Runtime, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.SharePoint.Client.Search, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>Microsoft.SharePoint.Client.Taxonomy, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</GACReference>
  <GACReference>System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35</GACReference>
  <Namespace>ETL.Helper.Controller</Namespace>
  <Namespace>ETL.Helper.Extensions</Namespace>
  <Namespace>ETL.Helper.Model</Namespace>
  <Namespace>ETL.Helper.Model.Metadata</Namespace>
  <Namespace>Microsoft.SharePoint.Client</Namespace>
  <Namespace>Microsoft.SharePoint.Client.Publishing</Namespace>
  <Namespace>Microsoft.SharePoint.Client.Search.Query</Namespace>
  <Namespace>Microsoft.SharePoint.Client.Taxonomy</Namespace>
  <Namespace>Microsoft.SharePoint.Client.WebParts</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Web.Script.Serialization</Namespace>
</Query>

			OAuthCredential credential = new OAuthCredential()
            {
                UserName = "chavez@rdacorp.com"
                ,Password = "M@nagua1TLnUtikGUGTzrlrJb7ULFremt"
                , ApiKey = "3MVG9KI2HHAq33RzA0ERMrkC6c.nBwj0Wool3iwXCDNO4aLnRWTyjH0WaLmK4pDY9Bb5z09GblL5Xw48CHWlY"
                ,ApiSecret= "9185997188589143935"
                ,AuthenticationUrl = "https://login.salesforce.com/services/oauth2/token"
            };

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
                RequestMethod = HttpMethod.Post
                , ResourceUrl = credential.AuthenticationUrl
				, RequestContent = requestContent
            };

            RestServiceController rc = new RestServiceController();
            var newResource = rc.GetResource(resource);
			newResource.Dump();
			