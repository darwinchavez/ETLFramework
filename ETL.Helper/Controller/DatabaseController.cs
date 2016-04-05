using ETL.Helper.Model.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Controller
{
    public class OleDbDatabaseController : IEntityController<DatabaseTable>
    {
        delegate void DatabaseCodeBlock();
        OleDbConnection databaseConnection;

        public OleDbDatabaseController(string connectionStringOrName)
        {
            if (string.IsNullOrEmpty(connectionStringOrName))
                throw new ArgumentNullException("connectionStringOrName");

            databaseConnection = new OleDbConnection(connectionStringOrName);
        }

        public EntityCatalog<DatabaseTable> GetEntityCatalog(IMetadataRestriction metadataRestriction = null)
        {
            string[] schemaRestrictions = metadataRestriction == null ? new string[4] : metadataRestriction.ToSchemaRestrictions();
            string[] systemObjects = new string[] { "SYSTEM TABLE", "SYSTEM VIEW" };

            DatabaseCatalog db = new DatabaseCatalog();
            List<OleDbColumnSchema> columnsSchema = null;
            List<OleDbTableSchema> tablesSchema = null;
            List<OleDbKeyInfoSchema> keysInfoSchema = null;

            RunDatabaseCodeBlock(delegate ()
            {
                DataTable columnsTable = databaseConnection.GetSchema("Columns", schemaRestrictions);
                columnsSchema = (from r in columnsTable.Rows.Cast<DataRow>()
                                 select new OleDbColumnSchema(r)).ToList();

                DataTable tablesTable = databaseConnection.GetSchema("Tables", schemaRestrictions);
                tablesSchema = (from r in tablesTable.Rows.Cast<DataRow>()
                                select new OleDbTableSchema(r)).ToList();

                var keyInfoTables = (from t in tablesSchema
                                     select GetColumnsKeyInfo(t.TableName).Rows.Cast<DataRow>()).ToList();
                keysInfoSchema = keyInfoTables.SelectMany(r => r).Select(r => new OleDbKeyInfoSchema(r)).Where(k => !string.IsNullOrEmpty(k.TableName.FullName)).ToList();
            });

            if (columnsSchema != null && tablesSchema != null)
            {
                keysInfoSchema = keysInfoSchema ?? new List<OleDbKeyInfoSchema>();

                var entities = from t in tablesSchema
                               where !systemObjects.Contains(t.TABLE_TYPE)
                               select new DatabaseTable()
                               {
                                   Name = t.TableName,
                                   Properties = (from c in columnsSchema
                                                 where t.TableName.FullName == c.TableName.FullName
                                                 join k in keysInfoSchema on new {TableName = c.TableName.FullName
                                                                                     ,ColumnName = c.COLUMN_NAME} equals new {TableName = k.TableName.FullName
                                                                                                                                ,ColumnName = k.ColumnName} into columnKeyInfo
                                                 from ck in columnKeyInfo.DefaultIfEmpty()
                                                 let databaseField = new DatabaseField(c.FieldName, c.OleDbDataType)
                                                 {
                                                     HasDefault = c.COLUMN_HASDEFAULT.HasValue ? false : c.COLUMN_HASDEFAULT.Value
                                                     , DefaultValue = c.COLUMN_DEFAULT
                                                     , IsAutoIncrement = ck == null ? false : ck.IsAutoIncrement
                                                     , IsKey = ck == null ? false : ck.IsKey
                                                     , IsNullable = c.IS_NULLABLE.HasValue ? false : c.IS_NULLABLE.Value
                                                     , IsReadOnly = ck == null ? false : ck.IsReadOnly
                                                     , IsUnique = ck == null ? false : ck.IsUnique
                                                 }
                                                 select databaseField).ToList<IEntityProperty>()
                               };

                db.Entities = entities.ToList();
            }

            return db;
        }

        private DataTable GetColumnsKeyInfo(DatabaseTableName tableName)
        {
            DataTable keysInfo = new DataTable();

            string sqlStatement = "Select * From " + tableName.FullName + " Where 1 = 0";
            RunDatabaseCodeBlock(delegate ()
            {
                using (OleDbCommand command = new OleDbCommand(sqlStatement, databaseConnection))
                {
                    OleDbDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                    keysInfo = reader.GetSchemaTable();
                }
            });

            return keysInfo;
        }

        private void RunDatabaseCodeBlock(DatabaseCodeBlock codeBlock)
        {
            ConnectionState connState = databaseConnection.State;
            try
            {
                if (connState != ConnectionState.Open)
                    databaseConnection.Open();

                codeBlock();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connState != ConnectionState.Open)
                    databaseConnection.Close();
            }
        }
    }
}
