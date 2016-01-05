using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMS.BusinessObjects
{
    public class HasFunctionality
    {
        public static bool FunctionPermission(int nIsSuperUser, UserFunctions UFs, int nFID)
        {
            bool _bIsPermitted = false;
            //User function security
            if (nIsSuperUser == 1)
            {
                _bIsPermitted= true;
            }
            else
            {
                foreach (UserFunction Oitem in UFs)
                {
                    if (Oitem.ObjectID == nFID & Oitem.IsCheck == true)
                    {
                        _bIsPermitted = true;
                    }
                }
            }
            return _bIsPermitted;
        }
    }
}
