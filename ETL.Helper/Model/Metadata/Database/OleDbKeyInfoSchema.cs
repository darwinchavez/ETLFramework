using ETL.Helper.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class OleDbKeyInfoSchema
    {
        public OleDbKeyInfoSchema() { }
        public OleDbKeyInfoSchema(DataRow keyInfoSchemaRow): this()
        {
            ColumnName = keyInfoSchemaRow["ColumnName"].ToString();
            ColumnOrdinal = keyInfoSchemaRow.Get<Int32>("ColumnOrdinal");
            ColumnSize = keyInfoSchemaRow.Get<Int32>("ColumnSize");
            NumericPrecision = keyInfoSchemaRow.Get<Int16>("NumericPrecision");
            NumericScale = keyInfoSchemaRow.Get<Int16>("NumericScale");
            DataType = keyInfoSchemaRow.Get<Type>("DataType");
            ProviderType = keyInfoSchemaRow.Get<Int32>("ProviderType");
            IsLong = keyInfoSchemaRow.Get<Boolean>("IsLong");
            AllowDBNull = keyInfoSchemaRow.Get<Boolean>("AllowDBNull");
            IsReadOnly = keyInfoSchemaRow.Get<Boolean>("IsReadOnly");
            IsRowVersion = keyInfoSchemaRow.Get<Boolean>("IsRowVersion");
            IsUnique = keyInfoSchemaRow.Get<Boolean>("IsUnique");
            IsKey = keyInfoSchemaRow.Get<Boolean>("IsKey");
            IsAutoIncrement = keyInfoSchemaRow.Get<Boolean>("IsAutoIncrement");
            BaseSchemaName = keyInfoSchemaRow["BaseSchemaName"].ToString();
            BaseCatalogName = keyInfoSchemaRow["BaseCatalogName"].ToString();
            BaseTableName = keyInfoSchemaRow["BaseTableName"].ToString();
            BaseColumnName = keyInfoSchemaRow["BaseColumnName"].ToString();

            TableName = new DatabaseTableName(BaseCatalogName, BaseSchemaName, BaseTableName);
            FieldName = new DatabaseFieldName(ColumnName);
        }

        public String ColumnName { get; set; }
        public Int32 ColumnOrdinal { get; set; }
        public Int32 ColumnSize { get; set; }
        public Int16 NumericPrecision { get; set; }
        public Int16 NumericScale { get; set; }
        public Type DataType { get; set; }
        public Int32 ProviderType { get; set; }
        public Boolean IsLong { get; set; }
        public Boolean AllowDBNull { get; set; }
        public Boolean IsReadOnly { get; set; }
        public Boolean IsRowVersion { get; set; }
        public Boolean IsUnique { get; set; }
        public Boolean IsKey { get; set; }
        public Boolean IsAutoIncrement { get; set; }
        public String BaseSchemaName { get; set; }
        public String BaseCatalogName { get; set; }
        public String BaseTableName { get; set; }
        public String BaseColumnName { get; set; }

        public DatabaseTableName TableName { get; set; }
        public DatabaseFieldName FieldName { get; set; }
    }
}
