using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public interface IEntityPropertyDataType
    {
        string Name { get; set; }
        int? NumericPrecision { get; set; }
        int? NumericScale { get; set; }
        decimal? CharacterMaxLength { get; set; }
        bool IsStringWithMaxLength { get; set; }
        DataTypeMapping DataTypeMapping { get; set; }
    }
}
