using ETL.Helper.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class OleDbTableSchema
    {
        public OleDbTableSchema(DataRow tableSchemaRow)
        {
            TABLE_CATALOG = tableSchemaRow["TABLE_CATALOG"].ToString();
            TABLE_SCHEMA = tableSchemaRow["TABLE_SCHEMA"].ToString();
            TABLE_NAME = tableSchemaRow["TABLE_NAME"].ToString();
            TABLE_TYPE = tableSchemaRow["TABLE_TYPE"].ToString();
            DESCRIPTION = tableSchemaRow["DESCRIPTION"].ToString();
            TABLE_PROPID = tableSchemaRow.Get<Int64?>("TABLE_PROPID");
            DATE_CREATED = tableSchemaRow.Get<DateTime?>("DATE_CREATED");
            DATE_MODIFIED = tableSchemaRow.Get<DateTime?>("DATE_MODIFIED");
            TABLE_GUID = tableSchemaRow.Get<Guid>("TABLE_GUID");

            TableName = new DatabaseTableName(TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME);
        }
        public String TABLE_CATALOG { get; set; }
        public String TABLE_SCHEMA { get; set; }
        public String TABLE_NAME { get; set; }
        public String TABLE_TYPE { get; set; }
        public String DESCRIPTION { get; set; }
        public Int64? TABLE_PROPID { get; set; }
        public DateTime? DATE_CREATED { get; set; }
        public DateTime? DATE_MODIFIED { get; set; }
        public Guid TABLE_GUID { get; set; }

        public DatabaseTableName TableName { get; set; }
    }
}
