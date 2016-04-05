using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model
{
    public class AuthenticationToken : Root
    {
        public string Id { get; set; }
        public string IssuesAt { get; set; }
        public string RefreshToken { get; set; }
        public string InstanceUrl { get; set; }
        public string Signature { get; set; }
        public string AccessToken { get; set; }
    }
}
