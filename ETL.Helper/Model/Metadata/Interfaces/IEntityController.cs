using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public interface IEntityController<T> where T : Entity
    {
        EntityCatalog<T> GetEntityCatalog(IMetadataRestriction metadataRestriction = null);
    }
}
