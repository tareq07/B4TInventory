using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using EMS.Core.Framework;
using EMS.Core.DataAccess;
using EMS.Core;
using EMS.Core.Utility;

using EMS.BusinessObjects;

using System.Configuration;
using System.Data.SqlClient;
using System.Management;

namespace EMS.Services.Services
{
    [Serializable]
    public class LoginInterfaceService : MarshalByRefObject, ILoginInterfaceService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion
        #region Private functions and declaration
        private void MapObject(LoginInterface oLoginInterface, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oLoginInterface, new ID(oReader.GetInt32("user_id")));
            oLoginInterface.user_code = oReader.GetInt32("user_code");
            oLoginInterface.user_fst_name = oReader.GetString("user_fst_name");
            oLoginInterface.user_lst_name = oReader.GetString("user_lst_name");
            oLoginInterface.user_name = oReader.GetString("user_name");
            oLoginInterface.user_pass = oReader.GetString("user_pass");
            oLoginInterface.user_type = (EnumUserType)oReader.GetInt32("user_type");
            oLoginInterface.user_islogon = oReader.GetBoolean("user_islogon");
            oLoginInterface.user_lock = oReader.GetBoolean("user_lock");
            oLoginInterface.user_status = (EnumUserStatus)oReader.GetInt32("user_status");
            //oLoginInterface.IsAuthorise = oReader.GetBoolean("IsAuthorise");
           
            BusinessObject.Factory.SetObjectState(oLoginInterface, ObjectState.Saved);
        }

        private LoginInterface CreateObject(NullHandler oReader)
        {
            LoginInterface oLoginInterface = new LoginInterface();
            MapObject(oLoginInterface, oReader);
            return oLoginInterface;
        }

       
        #endregion

        #region Interface implementation
        public LoginInterfaceService() { }

        public LoginInterface Get(LoginInterface oLoginInterface)
        {          
            
            try
            {
               
               bool result = true;
               string sUN = "";
               string sUP = "";
               //int nUserType = (int) oLoginInterface.user_type;
               sUN = oLoginInterface.user_name;
               //sUP = EMSGlobal.Encrypt(oLoginInterface.Password);
               sUP = oLoginInterface.user_pass;

               //SqlCommand cmd = new SqlCommand("SP_GetUserLoginInfo", _conn);

               //cmd.CommandType = CommandType.StoredProcedure;
               //cmd.Parameters.Add(new SqlParameter("@user_name", SqlDbType.VarChar)).Value = oLoginInterface.user_name;
               //cmd.Parameters.Add(new SqlParameter("@user_pass", SqlDbType.VarChar)).Value = oLoginInterface.user_pass;
               //cmd.Parameters.Add(new SqlParameter("@MacAddres", SqlDbType.VarChar)).Value = oLoginInterface.MacAddres;
               //if (_conn.State == ConnectionState.Open) { }
               //else { cmd.Connection.Open(); }
               //IDataReader reader = cmd.ExecuteReader();
               //NullHandler oReader = new NullHandler(reader);
               //if (reader.Read())
               //{
               //    oLoginInterface = CreateObject(oReader);
               //}
               //cmd.Dispose();
               //cmd.Connection.Close();

               //if (oLoginInterface.ID.ToInt32 > 0)
               //{
               //    if (oLoginInterface.ObjectID == 1)
               //    { }
               //    else
               //    {
               //        if (oLoginInterface.MacAddres == string.Empty)
               //        {
               //            throw new Exception("Unauthorised Machin. Please contact with admin.");
               //        }
               //        if (oLoginInterface.IsAuthorise == false)
               //        {
               //            throw new Exception("Unauthorised Machin. Please contact with admin.");
               //        }
               //        if (oLoginInterface.user_lock == true)
               //        {
               //            throw new Exception("Account is Locked. Please contact with admin.");
               //        }
               //        if (oLoginInterface.user_status == EnumUserStatus.Suspend)
               //        {
               //            throw new Exception("Account has been suspended. Please contact with admin.");
               //        }
               //        if (oLoginInterface.user_islogon == true)
               //        {
               //            throw new Exception("Someone using this ID. Please Try another ID.");
               //        }
               //        string QueryString3 = "UPDATE User_Table SET user_islogon=1 WHERE user_id=" + oLoginInterface.ObjectID;
               //        ExecuteQueryFunctions.ExeSclr(_conn, QueryString3);
               //    }
               //}
               //else
               //{
               //    throw new Exception("Incorrect User ID. Please type a correct User ID.");
               //}







               string QueryString = "SELECT COUNT(*) FROM User_Table WHERE user_name ='" + sUN + "' AND user_pass ='" + sUP + "'";
               result = ExecuteQueryFunctions.ExeSclr(_conn, QueryString);
               if (result)
               {


                   string QueryString2 = "SELECT * FROM User_Table WHERE user_name ='" + sUN + "' AND user_pass ='" + sUP + "'";
                   IDataReader reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString2);
                   NullHandler oReader = new NullHandler(reader);
                   if (reader.Read())
                   {
                       oLoginInterface = CreateObject(oReader);
                   }
                   reader.Close();

                   //string QueryString3 = "SELECT * FROM tbl_UserSecurity AS TUS WHERE TUS.user_id=" + oLoginInterface.ObjectID;

                   _conn.Close();

                   if (oLoginInterface.ObjectID == 1)
                   { }
                   else
                   {
                       //if ((int)oLoginInterface.user_type != nUserType)
                       //{
                       //    throw new Exception("Yor are not authenticated in this type of user. Please select your area.");
                       //}
                       if (oLoginInterface.user_lock == true)
                       {
                           throw new Exception("Account is Locked. Please contact with admin.");
                       }
                       if (oLoginInterface.user_status == EnumUserStatus.Suspend)
                       {
                           throw new Exception("Account has been suspended. Please contact with admin.");
                       }
                       if (oLoginInterface.user_islogon == true)
                       {
                           throw new Exception("Someone using this ID. Please Try another ID.");
                       }
                       //string QueryString3 = "UPDATE User_Table SET user_islogon=1 WHERE user_id=" + oLoginInterface.ObjectID;
                       //ExecuteQueryFunctions.ExeSclr(_conn, QueryString3);
                   }
                   //ExecuteQueryFunctions.ExeNonQuery(_conn, "EXEC dbo.SP_UpdateCelcInst");

               }
               else
               {
                   throw new Exception("Incorrect User ID. Please type a correct User ID.");
               }
              
            }
            catch (Exception e)
            {               
                throw new ServiceException(e.Message);                
            }
            
            return oLoginInterface;
        }

             

      
        #endregion
    }
}
