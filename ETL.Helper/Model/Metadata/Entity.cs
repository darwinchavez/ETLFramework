using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class Entity : IEntity
    {
        public virtual IEntityName Name { get; set; }

        public IEnumerable<IEntityProperty> NaturalKeys { get; set; }

        public IEnumerable<IEntityProperty> PrimaryKeys { get; set; }

        public IEnumerable<IEntityProperty> Properties { get; set; }

        public IEnumerable<IEntity> Associations { get; set; }
    }
}

