using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class MetadataName : IMetadataName
    {
        public MetadataName()
        {
        }

        public MetadataName(string name, string descriptor) : this()
        {
            Name = name;
            Descriptor = descriptor;
        }

        public string Name { get; set; }
        public string Descriptor { get; set; }

        public virtual string FullName
        {
            get
            {
                string fullName = "";
                if (!string.IsNullOrEmpty(Name))
                {
                    fullName = !string.IsNullOrEmpty(Descriptor) ? Descriptor + "." + Name : Name;
                }

                return fullName;
            }
        }
    }
}
