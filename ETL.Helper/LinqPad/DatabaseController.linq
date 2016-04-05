<Query Kind="Statements">
  <Reference Relative="..\bin\Debug\ETL.Helper.dll">D:\Projects\OpenSource\ETLFramework\ETL.Helper\bin\Debug\ETL.Helper.dll</Reference>
  <Reference Relative="..\..\ETL.Services\bin\Debug\ETL.Services.dll">D:\Projects\OpenSource\ETLFramework\ETL.Services\bin\Debug\ETL.Services.dll</Reference>
  <GACReference>System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</GACReference>
  <GACReference>System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</GACReference>
  <GACReference>System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35</GACReference>
  <Namespace>ETL.Helper.Extensions</Namespace>
  <Namespace>ETL.Helper.Model</Namespace>
  <Namespace>ETL.Helper.Model.Metadata</Namespace>
  <Namespace>ETL.Services</Namespace>
  <Namespace>ETL.Services.SFDC</Namespace>
  <Namespace>System.Data.OleDb</Namespace>
  <Namespace>System.ServiceModel</Namespace>
  <Namespace>System.Web.Script.Serialization</Namespace>
</Query>

string connectionString = @"Data Source=M4700\MSSQLSERVER14;Initial Catalog=AdventureWorks2008R2;Provider=SQLNCLI11.1;Integrated Security=SSPI;Auto Translate=False;";
DatabaseController dc = new DatabaseController(connectionString);
var db = dc.GetEntityCatalog();


SalesForceController sc = new SalesForceController();
var loginResult = sc.Login("chavez@rdacorp.com", "P@ssw0rd", "BQrx2UHuiMCqy0yrCGIfvqgge");
//var globalObjects = sc.GetGlobalObjects();
string[] objectNames = new string[] {"Account"};
var objsDetail = sc.GetObjectsDetail(objectNames);
//objsDetail.Dump();
//var dataTypes = objsDetail.SelectMany(o => o.fields).Select(f => f.type).Distinct();
//dataTypes.OrderBy(f => f.ToString()).Dump();
var dataTypes = objsDetail.SelectMany(o => o.fields).Where(f => f.type.ToString() == "int").Dump();

sc.Logout();

var entities = from obj in objsDetail
                           let entityName = new EntityName(obj.name, "")
                           select new Entity()
                           {
                               Name = entityName,
                               Properties = from f in obj.fields
                                            let fieldName = new MetadataName(f.name, "")
                                            let columnSchema = new SalesForceColumnSchema(f)
                                            let dataType = new SalesForceDataType(columnSchema)
                                            select new EntityProperty(fieldName, dataType)
                                            {
												HasDefault = !string.IsNullOrEmpty(f.defaultValueFormula)
												,DefaultValue = f.defaultValueFormula
                                                     ,IsAutoIncrement = f.autoNumber
                                                     ,IsKey = (f.type.ToString() == "id")
                                                     //,IsNullable = f.req
                                                     ,IsReadOnly = !f.updateable
                                                     ,IsUnique = f.unique
                                            }
                           };
//entities.Dump();

}

public class SalesForceController
{
    private static SoapClient client; // for API endpoint
    private static SessionHeader header;
    private static EndpointAddress endpoint;
    private string loginUrl = @"https://login.salesforce.com/services/Soap/c/34.0";

	public LoginResult Login(string userName, string password, string securityToken)
	{
		SoapClient loginClient = CreateWebServiceInstance(loginUrl);
        LoginResult lr = loginClient.login(null, userName, password + securityToken);

        // Check if the password has expired 
        if (lr.passwordExpired)
        {
			throw new Exception("An error has occurred. Your password has expired.");
		}

        // On successful login, cache session info and API endpoint info
        endpoint = new EndpointAddress(lr.serverUrl);
        header = new SessionHeader();
        header.sessionId = lr.sessionId;

        // Create and cache an API endpoint client
        client = CreateWebServiceInstance(lr.serverUrl);
        return lr;
	}
	
	public void Logout()
    {
		if (client != null)
    		client.logout(header);
	}
	
	public DescribeGlobalResult GetGlobalObjects()
    {
        DescribeGlobalResult dgr;
        LimitInfo[] infos = client.describeGlobal(header, null, out dgr);
		return dgr;
    }
	
	public DescribeSObjectResult[] GetObjectsDetail(string[] objectNames)
    {
		if (objectNames == null)
			throw new ArgumentNullException("objectNames");
			
    	// Call describeSObjects() passing in an array with one object type name 
        DescribeSObjectResult[] dsrArray;
        client.describeSObjects(header, null, null, objectNames, out dsrArray);
		
		return dsrArray;
	}
		
	private SoapClient CreateWebServiceInstance(string endPointUrl)
    {
    	if (string.IsNullOrEmpty(endPointUrl))
        	throw new ArgumentNullException("endPointUrl");

        BasicHttpBinding binding = new BasicHttpBinding();
        binding.Name = "SoapBinding";
        binding.SendTimeout = new TimeSpan(0, 5, 0);
        binding.MaxReceivedMessageSize = 20000000;
        binding.Security.Mode = BasicHttpSecurityMode.Transport;
        var client = new SoapClient(binding, new EndpointAddress(endPointUrl));
        return client;
	}
}

public class DatabaseController
    {
        delegate void DatabaseCodeBlock();
        OleDbConnection databaseConnection;

        public DatabaseController(string connectionStringOrName)
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

                DataTable tables = databaseConnection.GetSchema("Tables", schemaRestrictions);
                tablesSchema = (from r in tables.Rows.Cast<DataRow>()
								where !systemObjects.Contains(r["TABLE_TYPE"].ToString())
                                select new OleDbTableSchema(r)).ToList();
								
				var keyInfoTables = (from t in tablesSchema
                                     select GetColumnsKeyInfo(t.TableName).Rows.Cast<DataRow>()).ToList();

				keysInfoSchema = keyInfoTables.SelectMany(r => r).Select(r => new OleDbKeyInfoSchema(r)).Where(k => !string.IsNullOrEmpty(k.TableName.FullName)).ToList();
				
            });

            if (columnsSchema != null && tablesSchema != null)
            {
				keysInfoSchema = keysInfoSchema ?? new List<OleDbKeyInfoSchema>();
                var entities = from t in tablesSchema
                               let databaseTableName = new DatabaseTableName(t.TABLE_CATALOG, t.TABLE_SCHEMA, t.TABLE_NAME)
                               select new DatabaseTable()
                               {
                                   Name = databaseTableName,
                                   Properties = (from c in columnsSchema
                                                 where t.TableName.FullName == c.TableName.FullName
												 join k in keysInfoSchema on new {TableName = c.TableName.FullName
												 									, ColumnName = c.COLUMN_NAME} equals new {TableName = k.TableName.FullName
																																, ColumnName = k.ColumnName} into columnKeyInfo
												 from ck in columnKeyInfo.DefaultIfEmpty()
                                                 let databaseField = new DatabaseField(c.FieldName, c.OleDbDataType){
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

        public DataTable GetColumnsKeyInfo(DatabaseTableName tableName)
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
			//"Block Started".Dump();
            ConnectionState connState = databaseConnection.State;
            try
            {
                if (connState != ConnectionState.Open)
				{
                    databaseConnection.Open();
					//"Connection Opened".Dump();
				}
				
                codeBlock();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connState != ConnectionState.Open)
				{
                    databaseConnection.Close();
					//"Connection Closed".Dump();
				}
				
				//"Block Ended".Dump();
            }
        }
