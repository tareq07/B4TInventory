using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace EMS.Core.DataAccess
{
    public class EMSConFunc
    {
        #region ConnectionString
        private static string _connectionString = ConfigurationManager.ConnectionStrings["EMSConnectionString"].ConnectionString;       
        #endregion

        public static string ConString()
        {
            return _connectionString;
        }
    }
}
