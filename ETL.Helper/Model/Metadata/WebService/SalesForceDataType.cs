using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class SalesForceDataType : EntityPropertyDataType
    {
        public SalesForceDataType(SalesForceColumnSchema columnSchema)
        {
            if (columnSchema == null)
                throw new ArgumentNullException("columnSchema");

            Name = columnSchema.type.ToString();
            CharacterMaxLength = columnSchema.length;
            NumericPrecision = columnSchema.precision;
            NumericScale = columnSchema.digits;
        }
    }
}
