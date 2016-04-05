using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public interface IEntityProperty
    {
        IEntity Entity { get; set; }
        IMetadataName Name { get; set; }
        IEntityPropertyDataType DataType { get; set; }
        bool HasDefault { get; set; }
        string DefaultValue { get; set; }
        bool IsNullable { get; set; }
        bool IsAutoIncrement { get; set; }
        bool IsReadOnly { get; set; }
        bool IsUnique { get; set; }
        bool IsKey { get; set; }
    }
}
