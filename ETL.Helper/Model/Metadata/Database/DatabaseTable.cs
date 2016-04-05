using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class DatabaseTable : Entity
    {
        #region SQL Statements
        public virtual string GetSelectStatement()
        {
            string selectStatement = "";
            return selectStatement;
        }

        public virtual string GetCreateStatement()
        {
            string createStatement = "";
            return createStatement;
        }
        #endregion
    }
}
