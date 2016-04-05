using ETL.Helper.Extensions;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class EntityPropertyDataType : IEntityPropertyDataType
    {
        public string Name { get; set; }
        public decimal? CharacterMaxLength { get; set; }
        public int? NumericPrecision { get; set; }
        public int? NumericScale { get; set; }
        public bool IsStringWithMaxLength { get; set; }
        public DataTypeMapping DataTypeMapping { get; set; }
        public virtual string PrecisionAsString()
        {
            string precisionStr = "";
            if (NumericScale.HasValue && NumericScale.Value > 0)
                precisionStr = NumericPrecision.Value.ToString() + "," + NumericScale.Value.ToString();

            return precisionStr;
        }

        public virtual string CharacterMaxAsString()
        {
            string characterMaxStr = "";
            if (CharacterMaxLength.HasValue)
                characterMaxStr = (CharacterMaxLength.Value == 0 || IsStringWithMaxLength) ? "max" : CharacterMaxLength.Value.ToString();

            return characterMaxStr;
        }

        public virtual string ToString(DataTypes dataType)
        {
            /// TODO: Refactor this switch statement to use a pattern (ie, Strategy)
            string dataTypeName = Name;
            switch (dataType)
            {
                case DataTypes.OleDb:
                    dataTypeName = DataTypeMapping.OleDb;
                    break;
                case DataTypes.SQLServer:
                    dataTypeName = DataTypeMapping.SQLServer;
                    break;
                case DataTypes.Oracle:
                    dataTypeName = DataTypeMapping.Oracle;
                    break;
                case DataTypes.BIML:
                    dataTypeName = DataTypeMapping.BIML;
                    break;
                case DataTypes.SSIS:
                    dataTypeName = DataTypeMapping.SSIS;
                    break;
                case DataTypes.DotNet:
                    dataTypeName = DataTypeMapping.DotNet;
                    break;
                case DataTypes.SalesForce:
                    dataTypeName = DataTypeMapping.SalesForce;
                    break;
                default:
                    break;
            }

            string dataTypeStr = "";
            if (!string.IsNullOrEmpty(dataTypeName))
            {
                string precisionStr = PrecisionAsString();
                string characterMaxStr = CharacterMaxAsString();
                dataTypeStr = dataTypeName.EncloseInBrackets()
                                + (string.IsNullOrEmpty(precisionStr) ? "" : precisionStr.EncloseInParenthesis())
                                + (string.IsNullOrEmpty(characterMaxStr) ? "" : characterMaxStr.EncloseInParenthesis());
            }

            return dataTypeStr;
        }
    }
}
