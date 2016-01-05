using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EMS.Core.Framework;
using EMS.Core;
using EMS.Core.Utility;
using System.Data;

namespace EMS.BusinessObjects
{

    #region LoginInterface
    public class LoginInterface : BusinessObject
    {

        #region  Constructor
        public LoginInterface()
        {

        }
        #endregion

        #region Properties
        public int user_code { get; set; }

        public string user_fst_name { get; set; }

        public string user_lst_name { get; set; }

        public string user_name { get; set; }

        public string user_pass { get; set; }

        public string MacAddres { get; set; }

        public EnumUserType user_type { get; set; }

        public bool user_islogon { get; set; }

        public bool user_lock { get; set; }

        public EnumUserStatus user_status { get; set; }

        public bool IsAuthorise { get; set; }
        #endregion

        #region Functions
        public LoginInterface Get(LoginInterface oLoginInterface)
        {
            return LoginInterface.Service.Get(oLoginInterface);
        }
        #endregion

        #region ServiceFactory
        internal static ILoginInterfaceService Service
        {
            get { return (ILoginInterfaceService)Services.Factory.CreateService(typeof(ILoginInterfaceService)); }
        }
        #endregion
    }
    #endregion


    #region ILoginInterface interface
    public interface ILoginInterfaceService
    {
        LoginInterface Get(LoginInterface oLoginInterface);
    }
    #endregion
}
