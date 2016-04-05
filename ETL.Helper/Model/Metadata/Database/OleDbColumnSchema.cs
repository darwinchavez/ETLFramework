using ETL.Helper.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class OleDbColumnSchema
    {
        public OleDbColumnSchema() { }
        public OleDbColumnSchema(DataRow columnSchemaRow) : this()
        {
            if (columnSchemaRow == null)
                throw new ArgumentNullException("columnSchemaRow");

            TABLE_CATALOG = columnSchemaRow["TABLE_CATALOG"].ToString();
            TABLE_SCHEMA = columnSchemaRow["TABLE_SCHEMA"].ToString();
            TABLE_NAME = columnSchemaRow["TABLE_NAME"].ToString();
            COLUMN_NAME = columnSchemaRow["COLUMN_NAME"].ToString();
            SS_XML_SCHEMACOLLECTION_CATALOGNAME = columnSchemaRow["SS_XML_SCHEMACOLLECTION_CATALOGNAME"].ToString();
            SS_XML_SCHEMACOLLECTION_SCHEMANAME = columnSchemaRow["SS_XML_SCHEMACOLLECTION_SCHEMANAME"].ToString();
            SS_XML_SCHEMACOLLECTIONNAME = columnSchemaRow["SS_XML_SCHEMACOLLECTIONNAME"].ToString();
            SS_UDT_CATALOGNAME = columnSchemaRow["SS_UDT_CATALOGNAME"].ToString();
            SS_UDT_SCHEMANAME = columnSchemaRow["SS_UDT_SCHEMANAME"].ToString();
            SS_UDT_NAME = columnSchemaRow["SS_UDT_NAME"].ToString();
            COLUMN_DEFAULT = columnSchemaRow["COLUMN_DEFAULT"].ToString();
            CHARACTER_SET_CATALOG = columnSchemaRow["CHARACTER_SET_CATALOG"].ToString();
            CHARACTER_SET_SCHEMA = columnSchemaRow["CHARACTER_SET_SCHEMA"].ToString();
            CHARACTER_SET_NAME = columnSchemaRow["CHARACTER_SET_NAME"].ToString();
            COLLATION_CATALOG = columnSchemaRow["COLLATION_CATALOG"].ToString();
            COLLATION_SCHEMA = columnSchemaRow["COLLATION_SCHEMA"].ToString();
            COLLATION_NAME = columnSchemaRow["COLLATION_NAME"].ToString();
            DOMAIN_CATALOG = columnSchemaRow["DOMAIN_CATALOG"].ToString();
            DOMAIN_SCHEMA = columnSchemaRow["DOMAIN_SCHEMA"].ToString();
            DOMAIN_NAME = columnSchemaRow["DOMAIN_NAME"].ToString();
            DESCRIPTION = columnSchemaRow["DESCRIPTION"].ToString();
            SS_UDT_ASSEMBLY_TYPENAME = columnSchemaRow["SS_UDT_ASSEMBLY_TYPENAME"].ToString();

            COLUMN_LCID = columnSchemaRow.Get<Int32?>("COLUMN_LCID");
            COLUMN_COMPFLAGS = columnSchemaRow.Get<Int32?>("COLUMN_COMPFLAGS");
            COLUMN_SORTID = columnSchemaRow.Get<Int32?>("COLUMN_SORTID");
            COLUMN_FLAGS = columnSchemaRow.Get<Int64?>("COLUMN_FLAGS");
            DATA_TYPE = columnSchemaRow.Get<Int32>("DATA_TYPE");
            NUMERIC_PRECISION = columnSchemaRow.Get<Int32?>("NUMERIC_PRECISION");
            NUMERIC_SCALE = columnSchemaRow.Get<Int16?>("NUMERIC_SCALE");
            DATETIME_PRECISION = columnSchemaRow.Get<Int64?>("DATETIME_PRECISION");
            COLUMN_PROPID = columnSchemaRow.Get<Int64?>("COLUMN_PROPID");

            CHARACTER_MAXIMUM_LENGTH = columnSchemaRow.Get<Decimal?>("CHARACTER_MAXIMUM_LENGTH");
            CHARACTER_OCTET_LENGTH = columnSchemaRow.Get<Decimal?>("CHARACTER_OCTET_LENGTH");
            ORDINAL_POSITION = columnSchemaRow.Get<Decimal?>("ORDINAL_POSITION");

            IS_COMPUTED = columnSchemaRow.Get<Boolean?>("IS_COMPUTED");
            IS_NULLABLE = columnSchemaRow.Get<Boolean?>("IS_NULLABLE");
            SS_IS_SPARSE = columnSchemaRow.Get<Boolean?>("SS_IS_SPARSE");
            SS_IS_COLUMN_SET = columnSchemaRow.Get<Boolean?>("SS_IS_COLUMN_SET");
            COLUMN_HASDEFAULT = columnSchemaRow.Get<Boolean?>("COLUMN_HASDEFAULT");

            TYPE_GUID = columnSchemaRow.Get<Guid>("TYPE_GUID");
            COLUMN_GUID = columnSchemaRow.Get<Guid>("COLUMN_GUID");

            COLUMN_TDSCOLLATION = columnSchemaRow.Get<Byte[]>("COLUMN_TDSCOLLATION");

            TableName = new DatabaseTableName(TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME);
            FieldName = new DatabaseFieldName(COLUMN_NAME);
            OleDbDataType = new OleDbDataType(this);
        }

        public string TABLE_CATALOG { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }
        public string COLUMN_NAME { get; set; }
        public string COLUMN_DEFAULT { get; set; }
        public string CHARACTER_SET_CATALOG { get; set; }
        public string CHARACTER_SET_SCHEMA { get; set; }
        public string CHARACTER_SET_NAME { get; set; }
        public string COLLATION_CATALOG { get; set; }
        public string COLLATION_SCHEMA { get; set; }
        public string COLLATION_NAME { get; set; }
        public string DOMAIN_CATALOG { get; set; }
        public string DOMAIN_SCHEMA { get; set; }
        public string DOMAIN_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string SS_XML_SCHEMACOLLECTION_CATALOGNAME { get; set; }
        public string SS_XML_SCHEMACOLLECTION_SCHEMANAME { get; set; }
        public string SS_XML_SCHEMACOLLECTIONNAME { get; set; }
        public string SS_UDT_CATALOGNAME { get; set; }
        public string SS_UDT_SCHEMANAME { get; set; }
        public string SS_UDT_NAME { get; set; }
        public string SS_UDT_ASSEMBLY_TYPENAME { get; set; }
        public Int64? COLUMN_FLAGS { get; set; }
        public Int64? COLUMN_PROPID { get; set; }
        public Int32? COLUMN_LCID { get; set; }
        public Int32? COLUMN_COMPFLAGS { get; set; }
        public Int32? COLUMN_SORTID { get; set; }
        public Int32? NUMERIC_PRECISION { get; set; }
        public Int16? NUMERIC_SCALE { get; set; }
        public Int64? DATETIME_PRECISION { get; set; }
        public Int32 DATA_TYPE { get; set; }
        public decimal? ORDINAL_POSITION { get; set; }
        public decimal? CHARACTER_MAXIMUM_LENGTH { get; set; }
        public decimal? CHARACTER_OCTET_LENGTH { get; set; }
        public bool? IS_COMPUTED { get; set; }
        public bool? IS_NULLABLE { get; set; }
        public bool? COLUMN_HASDEFAULT { get; set; }
        public bool? SS_IS_SPARSE { get; set; }
        public bool? SS_IS_COLUMN_SET { get; set; }
        public Guid TYPE_GUID { get; set; }
        public Guid COLUMN_GUID { get; set; }
        public Byte[] COLUMN_TDSCOLLATION { get; set; }

        public DatabaseTableName TableName { get; set; }
        public DatabaseFieldName FieldName { get; set; }
        public OleDbDataType OleDbDataType { get; set; }
    }
}
