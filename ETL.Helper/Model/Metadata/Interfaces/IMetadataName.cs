using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public interface IMetadataName
    {
        string Name { get; set; }
        string Descriptor { get; set; }
        string FullName
        {
            get;
        }
    }
}
