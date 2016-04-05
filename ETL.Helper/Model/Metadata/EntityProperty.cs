using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class EntityProperty : IEntityProperty
    {
        public EntityProperty() { }

        public EntityProperty(IMetadataName propertyName, IEntityPropertyDataType propertyDataType) : this()
        {
            Name = propertyName;
            DataType = propertyDataType;
        }

        public IEntity Entity { get; set; }
        public IEntityPropertyDataType DataType { get; set; }
        public IMetadataName Name { get; set; }
        public bool HasDefault { get; set; }
        public string DefaultValue { get; set; }
        public bool IsNullable { get; set; }
        public bool IsAutoIncrement { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsUnique { get; set; }
        public bool IsKey { get; set; }
    }
}
