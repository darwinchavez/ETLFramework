using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class EntityCatalog<EntityType>
        where EntityType : IEntity
    {
        public IMetadataName Name { get; set; }
        public List<EntityType> Entities { get; set; }
    }
}
