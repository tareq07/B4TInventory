using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.Core.DataAccess
{
    public class SecurityIssue
    {
        public static bool _IsSecuried = false;
        public static bool IsSecuried(string sMachineID)
        {
            string _sMachineID = string.Empty;
            _sMachineID = sMachineID.Replace("-", "");

            _IsSecuried = ExecuteQueryFunctions.ExeIsExist("SELECT * FROM tbl_Security s WHERE s.ssMachineID='" + _sMachineID + "'");
            
            return _IsSecuried;
        }
    }
}
