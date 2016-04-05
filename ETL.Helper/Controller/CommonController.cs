using ETL.Helper.Model.Metadata;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Controller
{
    public static class CommonController
    {
        #region OleDb Helpers
        public static OleDbType? GetOleDbTypeFromNumber(string oleDbTypeNumberAsString)
        {
            if (string.IsNullOrEmpty(oleDbTypeNumberAsString))
                throw new ArgumentNullException("oleDbTypeNumberAsString");

            OleDbType? oleDbType = null;
            OleDbType _oleDbType;
            if (Enum.TryParse<OleDbType>(oleDbTypeNumberAsString, true, out _oleDbType))
            {
                oleDbType = _oleDbType;
            }

            return oleDbType;
        }

        public static string GetOleDbTypeAsString(string oleDbTypeNumberAsString)
        {
            if (string.IsNullOrEmpty(oleDbTypeNumberAsString))
                throw new ArgumentNullException("oleDbTypeNumberAsString");

            string oleDbTypeAsString = "";
            OleDbType? oleDbType = GetOleDbTypeFromNumber(oleDbTypeNumberAsString);
            if (oleDbType != null)
            {
                oleDbTypeAsString = oleDbType.ToString();
            }
            else
            {
                switch (oleDbTypeNumberAsString)
                {
                    case "145":
                        oleDbTypeAsString = "DBTime2";
                        break;
                    case "146":
                        oleDbTypeAsString = "DBTimeStampOffset";
                        break;
                    default:
                        break;
                }
            }

            return oleDbTypeAsString;
        }
        #endregion

        #region Data Mappings

        public static List<DataTypeMapping> GetDataMappings()
        {
            List<DataTypeMapping> mappings = new List<DataTypeMapping>();
            mappings.Add(new DataTypeMapping(){OleDb = "BigInt",SQLServer = "bigint",SSIS = "DT_18",BIML = "Int64"});
            mappings.Add(new DataTypeMapping(){OleDb = "Binary",SQLServer = "binary",SSIS = "DT_BYTES",BIML = "Binary"});
            mappings.Add(new DataTypeMapping(){OleDb = "Boolean",SQLServer = "bit",SSIS = "DT_BOOL",BIML = "Boolean", SalesForce = "boolean"});
            mappings.Add(new DataTypeMapping(){SQLServer = "char",SSIS = "DT_STR",BIML = "AnsiStringFixedLength"});
            mappings.Add(new DataTypeMapping(){OleDb = "DBDate", SQLServer = "date", SSIS = "DT_DBDATE", BIML = "Date", SalesForce = "date" });
            mappings.Add(new DataTypeMapping(){OleDb = "Date", SQLServer = "date", SSIS = "DT_DBDATE", BIML = "Date", SalesForce = "date" });
            mappings.Add(new DataTypeMapping(){OleDb = "DBTimeStamp",SQLServer = "datetime",SSIS = "DT_DBTIMESTAMP",BIML = "DateTime", SalesForce = "datetime"});
            mappings.Add(new DataTypeMapping(){SQLServer = "datetime2",SSIS = "DT_DBTIMESTAMP2",BIML = "DateTime2"});
            mappings.Add(new DataTypeMapping(){OleDb = "DBTimeStampOffset",SQLServer = "datetimeoffset",SSIS = "DT_DBTIMESTAMPOFFSET",BIML = "DateTimeOffset"});
            mappings.Add(new DataTypeMapping(){OleDb = "Numeric",SQLServer = "decimal",SSIS = "DT_DECIMAL",BIML = "Decimal"});
            mappings.Add(new DataTypeMapping(){ OleDb = "Double", SQLServer = "float", SSIS = "DT_R8", BIML = "Double", SalesForce = "double" });
            mappings.Add(new DataTypeMapping(){SQLServer = "image",SSIS = "DT_IMAGE",BIML = "Binary"});

            mappings.Add(new DataTypeMapping(){OleDb = "Integer",SQLServer = "int",SSIS = "DT_I4",BIML = "Int32", SalesForce = "int"});
            mappings.Add(new DataTypeMapping(){OleDb = "Currency", SQLServer = "money", SSIS = "DT_CY", BIML = "Currency", SalesForce = "currency" });
            mappings.Add(new DataTypeMapping(){SQLServer = "nchar",SSIS = "DT_WSTR",BIML = "StringFixedLength"});
            mappings.Add(new DataTypeMapping(){SQLServer = "ntext", SSIS = "DT_NTEXT", BIML = "String" });

            mappings.Add(new DataTypeMapping(){SQLServer = "numeric",SSIS = "DT_NUMERIC",BIML = "Decimal"});

            mappings.Add(new DataTypeMapping(){OleDb = "wChar", SQLServer = "nvarchar", SSIS = "DT_WSTR", BIML = "String", SalesForce = "string" });
            mappings.Add(new DataTypeMapping(){OleDb = "Single", SQLServer = "real", SSIS = "DT_R4", BIML = "Single" });
            mappings.Add(new DataTypeMapping(){SQLServer = "smalldatetime",SSIS = "DT_DBTIMESTAMP",BIML = "DateTime"});
            mappings.Add(new DataTypeMapping(){OleDb = "SmallInt",SQLServer = "smallint",SSIS = "DT_I2",BIML = "Int16"});
            mappings.Add(new DataTypeMapping(){OleDb = "UnsignedSmallInt",SQLServer = "smallint",SSIS = "DT_I2",BIML = "Int16"});
            mappings.Add(new DataTypeMapping(){OleDb = "Currency", SQLServer = "smallmoney", SSIS = "DT_CY", BIML = "Currency" });
            mappings.Add(new DataTypeMapping(){OleDb = "Variant",SQLServer = "sql_variant",SSIS = "DT_WSTR",BIML = "Object"});
            mappings.Add(new DataTypeMapping(){SQLServer = "text",SSIS = "DT_TEXT",BIML = "AnsiString"});

            mappings.Add(new DataTypeMapping(){OleDb = "DBTime2", SQLServer = "time", SSIS = "DT_DBTIME2", BIML = "Time" });
            mappings.Add(new DataTypeMapping(){OleDb = "UnsignedTinyInt",SQLServer = "tinyint",SSIS = "DT_UI1",BIML = "Byte"});
            mappings.Add(new DataTypeMapping(){OleDb = "Guid",SQLServer = "uniqueidentifier",SSIS = "DT_GUID",BIML = "Guid"});
            mappings.Add(new DataTypeMapping(){SQLServer = "varbinary",SSIS = "DT_BYTES",BIML = "Binary"});

            mappings.Add(new DataTypeMapping(){OleDb = "Char", SQLServer = "varchar",SSIS = "DT_STR",BIML = "AnsiString", SalesForce = "textarea"});
            mappings.Add(new DataTypeMapping(){SQLServer = "xml",SSIS = "DT_NSTR",BIML = "Xml"});

            return mappings;
        }
        #endregion

        #region Reflection
        public static void CopyProperties(this object source, object destination, List<string> listOfProperties)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");

            bool allsourceProperties = (listOfProperties == null);
            listOfProperties = listOfProperties ?? new List<string>();

            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();

            List<PropertyInfo> destProps = typeDest.GetProperties().Where(p => p.GetSetMethod(true) != null
                                                                            && !p.GetSetMethod(true).IsPrivate
                                                                            && (p.GetSetMethod().Attributes & MethodAttributes.Static) == 0).ToList();

            List<PropertyInfo> srcProps = typeSrc.GetProperties().Where(p => (allsourceProperties || listOfProperties.Contains(p.Name))
                                                                                && p.CanRead).ToList();

            var matchingProperties = from srcProp in srcProps
                                     join destProp in destProps on srcProp.Name equals destProp.Name
                                     where destProp.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                                     select new { sourceProperty = srcProp, targetProperty = destProp };

            //Update the properties
            foreach (var props in matchingProperties)
            {
                props.targetProperty.SetValue(destination, props.sourceProperty.GetValue(source, null), null);
            }
        }

        #endregion
    }
}
