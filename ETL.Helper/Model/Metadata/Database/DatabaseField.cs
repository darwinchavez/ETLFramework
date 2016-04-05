using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class DatabaseField : EntityProperty
    {
        public DatabaseField() : base() { }
        public DatabaseField(IMetadataName propertyName, IEntityPropertyDataType propertyDataType) : base(propertyName, propertyDataType)
        {
        }
    }
}