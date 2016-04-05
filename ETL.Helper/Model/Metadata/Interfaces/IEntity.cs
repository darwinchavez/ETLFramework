using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public interface IEntity<EntityPropertyType>
        where EntityPropertyType : IEntityProperty, new()
    {
        IEntityName Name { get; set; }
        IEnumerable<EntityPropertyType> PrimaryKeys { get; set; }
        IEnumerable<EntityPropertyType> NaturalKeys { get; set; }
        IEnumerable<EntityPropertyType> Properties { get; set; }
        IEnumerable<IEntity<EntityPropertyType>> Associations { get; set; }
    }

    public interface IEntity
    {
        IEntityName Name { get; set; }
        IEnumerable<IEntityProperty> PrimaryKeys { get; set; }
        IEnumerable<IEntityProperty> NaturalKeys { get; set; }
        IEnumerable<IEntityProperty> Properties { get; set; }
        IEnumerable<IEntity> Associations { get; set; }
    }

}
