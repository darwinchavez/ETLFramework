using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public interface IMetadataRestriction
    {
        string TableCatalog { get; set; }
        string TableSchema { get; set; }
        string TableName { get; set; }
        string TableType { get; set; }
        string[] ToSchemaRestrictions();
        string[] ToForeignKeyRestrictions();
    }
}
