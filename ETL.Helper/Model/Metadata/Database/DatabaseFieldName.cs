using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class DatabaseFieldName : EntityPropertyName
    {
        public DatabaseFieldName() : base()
        {
        }

        public DatabaseFieldName(string fieldName, string fieldAlias = "") : this()
        {
            Name = fieldName;
            Descriptor = fieldAlias;
        }

        public override string FullName
        {
            get
            {
                string alternateName = "";
                if (!string.IsNullOrEmpty(Name))
                    alternateName = !string.IsNullOrEmpty(Descriptor) ? Name + " AS " + Descriptor : Name;

                return alternateName;
            }
        }
    }
}
