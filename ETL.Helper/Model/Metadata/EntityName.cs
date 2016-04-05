using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class EntityName : MetadataName, IEntityName
    {
        public EntityName() : base()
        {

        }

        public EntityName(string name, string descriptor) : base(name, descriptor)
        {
        }
    }
}
