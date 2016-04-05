using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class DatabaseTableName : EntityName
    {
        public DatabaseTableName() : base()
        {

        }

        public DatabaseTableName(string databaseName, string schemaName, string tableName, string tableAlias = "") : this()
        {
            Schema = new Schema()
            {
                Name = schemaName,
                Descriptor = databaseName
            };

            Name = tableName;
            Descriptor = tableAlias;
        }

        public Schema Schema { get; set; }

        public override string FullName
        {
            get
            {
                string alternateName = "";
                if (!string.IsNullOrEmpty(Name))
                {
                    alternateName = Schema != null && !string.IsNullOrEmpty(Schema.Name) ? Schema.Name + "." + Name : Name;
                    alternateName += !string.IsNullOrEmpty(Descriptor) ? " AS " + Descriptor : "";
                }

                return alternateName;
            }
        }
    }
}
