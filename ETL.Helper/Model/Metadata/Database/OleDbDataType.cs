using ETL.Helper.Controller;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class OleDbDataType : EntityPropertyDataType
    {
        public OleDbDataType(OleDbColumnSchema columnSchema)
        {
            if (columnSchema == null)
                throw new ArgumentNullException("columnSchema");

            string typeAsString = columnSchema.DATA_TYPE.ToString();
            Name = CommonController.GetOleDbTypeAsString(typeAsString);
            CharacterMaxLength = columnSchema.CHARACTER_MAXIMUM_LENGTH;
            NumericPrecision = columnSchema.NUMERIC_PRECISION;
            NumericScale = columnSchema.NUMERIC_SCALE;
        }
    }
}
