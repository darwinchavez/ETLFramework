using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model
{
    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class OAuthCredential : Credential
    {
        public string AuthenticationUrl { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
