using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class DataTypeMapping
    {
        public string OleDb { get; set; }
        public string SQLServer { get; set; }
        public string Oracle { get; set; }
        public string BIML { get; set; }
        public string SSIS { get; set; }
        public string DotNet { get; set; }
        public string SalesForce { get; set; }
    }
}
