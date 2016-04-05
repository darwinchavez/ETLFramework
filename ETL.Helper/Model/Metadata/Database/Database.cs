using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class DatabaseCatalog : EntityCatalog<DatabaseTable>
    {
        public DatabaseCatalog()
        {
            Entities = new List<DatabaseTable>();
        }
    }
}
