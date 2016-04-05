using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class OleDbMetadataRestriction : IMetadataRestriction
    {
        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string TableType { get; set; }

        public string[] ToSchemaRestrictions()
        {
            string[] restrictions = new string[4];

            if (!string.IsNullOrEmpty(TableCatalog))
                restrictions[0] = TableCatalog;

            if (!string.IsNullOrEmpty(TableSchema))
                restrictions[1] = TableSchema;

            if (!string.IsNullOrEmpty(TableName))
                restrictions[2] = TableName;

            if (!string.IsNullOrEmpty(TableType))
                restrictions[3] = TableType;

            return restrictions;
        }

        public string[] ToForeignKeyRestrictions()
        {
            string[] restrictions = new string[6];
            if (!string.IsNullOrEmpty(TableSchema))
                restrictions[4] = TableSchema;

            if (!string.IsNullOrEmpty(TableName))
                restrictions[5] = TableName;

            return restrictions;
        }
    }
}
