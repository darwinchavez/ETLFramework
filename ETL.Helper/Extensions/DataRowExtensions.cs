using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Extensions
{
    public static class DataRowExtensions
    {
        public static T Get<T>(this DataRow dr, int index, T defaultValue = default(T))
        {
            return dr[index].Get<T>(defaultValue);
        }

        public static T Get<T>(this DataRow dr, string columnName, T defaultValue = default(T))
        {
            return dr[columnName].Get<T>(defaultValue);
        }
        static T Get<T>(this object obj, T defaultValue) //Private method on object.. just to use internally.
        {
            if (obj.IsNull())
                return defaultValue;

            return (T)obj;
        }

        public static bool IsNull<T>(this T obj) where T : class
        {
            return (object)obj == null || obj == DBNull.Value;
        }

    }
}
