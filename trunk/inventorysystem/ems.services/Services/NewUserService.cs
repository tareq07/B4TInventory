using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using EMS.Core.Framework;
using EMS.Core.DataAccess;
using EMS.Core;
using EMS.Core.Utility;
using System.Configuration;
using System.Data.SqlClient;
using EMS.BusinessObjects;

namespace EMS.Services.Services
{
    
    [Serializable]
    public class NewUserService : MarshalByRefObject, INewUserService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion
        #region Private functions and declaration
        private void MapObject(NewUser oNewUser, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oNewUser, new ID(oReader.GetInt32("user_id")));

            //oNewUser.brn_id = oReader.GetInt32("brn_id");
            //oNewUser.BrName = oReader.GetString("BrName");
            oNewUser.user_fst_name = oReader.GetString("user_fst_name");
            oNewUser.user_lst_name = oReader.GetString("user_lst_name");
            oNewUser.user_code = oReader.GetString("user_code");
            oNewUser.user_name_ini = oReader.GetString("user_name_ini");
            oNewUser.user_name = oReader.GetString("user_name");
            oNewUser.user_pass = oReader.GetString("user_pass");
            oNewUser.user_type = (EnumUserType)oReader.GetInt32("user_type");
            oNewUser.user_islogon = oReader.GetBoolean("user_islogon");
            oNewUser.user_lock = oReader.GetBoolean("user_lock");
            oNewUser.user_status = (EnumUserStatus)oReader.GetInt32("user_status");
            oNewUser.OwnerName = oReader.GetString("OwnerName");
            BusinessObject.Factory.SetObjectState(oNewUser, ObjectState.Saved);
        }

        private NewUser CreateObject(NullHandler oReader)
        {
            NewUser oNewUser = new NewUser();
            MapObject(oNewUser, oReader);
            return oNewUser;
        }

        private NewUsers CreateObjects(IDataReader oReader)
        {
            NewUsers oNewUsers = new NewUsers();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                NewUser oItem = CreateObject(oHandler);
                oNewUsers.Add(oItem);
            }
            return oNewUsers;
        }
        #endregion

        #region Interface implementation
        public NewUserService() { }

        public ID Save(NewUser oNewUser)
        {
            try
            {

                if (oNewUser.IsNew)
                {
                    BusinessObject.Factory.SetID(oNewUser, new ID(ExecuteQueryFunctions.GetNewID(_conn, "SELECT MAX(user_id) FROM User_Table")));
                    string sSN = "0000";
                    sSN = sSN.Substring(0, sSN.Length - oNewUser.ObjectID.ToString().Length) + oNewUser.ObjectID;
                    oNewUser.user_name = "User" + sSN;
                    oNewUser.user_pass = "a1234567";
                    oNewUser.user_status = EnumUserStatus.Active;
                    oNewUser.user_code = (int)oNewUser.user_type + oNewUser.ObjectID.ToString();
                    string QueryString2 = "INSERT INTO User_Table ([user_id],[brn_id],[user_code],[user_fst_name],[user_lst_name],[user_name_ini],[user_name],[user_pass],[user_type],[user_islogon],[user_lock],[user_status],[OwnerName],[OwnerID],[DBSDT])"
                                            + "VALUES(" +
                                            oNewUser.ObjectID + "," +
                                             "0,'" +
                                            oNewUser.user_code + "','" +
                                            oNewUser.user_fst_name + "','" +
                                            oNewUser.user_lst_name + "','" +
                                            oNewUser.user_name + "','" +
                                            oNewUser.user_name + "','" +
                                            oNewUser.user_pass + "'," +
                                            (int)oNewUser.user_type + ",'" +
                                            oNewUser.user_islogon + "','" +
                                            oNewUser.user_lock + "'," +
                                            (int)oNewUser.user_status + ",'" +
                                            EMSGlobal._sCurrenUserName + "'," +
                                            EMSGlobal._nCurrentUserID + ",'" +
                                            DateTime.Now + "')";
                    ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString2);
                }

                BusinessObject.Factory.SetObjectState(oNewUser, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oNewUser.ID;
        }
        public bool UpdateUser(NewUser oNewUser, int nEditID)
        {
            try
            {
                string QueryString = "UPDATE User_Table SET [user_fst_name]='" + oNewUser.user_fst_name + "', [user_lst_name]='" + oNewUser.user_lst_name + "',[user_type]=" + (int)oNewUser.user_type + ", [UpdateBy]=" + EMSGlobal._nCurrentUserID + "  WHERE user_id=" + nEditID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);

                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
        }


        public bool UpdateUser_IsLock(int nSelectedUserID, bool LockUnlock)
        {
            try
            {
                string QueryString = "UPDATE User_Table SET user_lock='" + LockUnlock + "' WHERE user_id=" + nSelectedUserID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
        }
        public bool UserStatus(int nSelectedUserID, int _nUStatus)
        {
            try
            {
                string QueryString = "UPDATE User_Table SET user_status=" + _nUStatus + " WHERE user_id=" + nSelectedUserID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        public bool UserDelete(int nUserID)
        {
            try
            {
                string QueryString = "DELETE FROM User_Table WHERE user_id=" + nUserID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
                string QueryString2 = "DELETE FROM Menu_Permission_Table WHERE user_id=" + nUserID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString2);
                string QueryString3 = "DELETE FROM [Function_permission_Table] WHERE user_id=" + nUserID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString3);
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
        }

        public NewUser Get(int nID)
        {
            NewUser oNewUser = new NewUser();

            try
            {
                IDataReader reader = ExecuteQueryFunctions.ExeReader(_conn, "SELECT * FROM User_Table AS ut WHERE user_id=" + nID);
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oNewUser = CreateObject(oReader);
                }
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }

            return oNewUser;
        }
        public NewUsers Get()
        {
            NewUsers oNewUsers = null;

            try
            {
                IDataReader reader = null;
                reader = ExecuteQueryFunctions.ExeReader(_conn, "SELECT * FROM User_Table AS ut WHERE user_id !=1");
                oNewUsers = CreateObjects(reader);
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }

            return oNewUsers;
        }
        public NewUsers GetsByeString(string sStr)
        {
            NewUsers oNewUsers = null;

            try
            {
                IDataReader reader = null;
                string QueryString = "SELECT * FROM User_Table WHERE user_id !=1 AND " + sStr;
                reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                oNewUsers = CreateObjects(reader);
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }

            return oNewUsers;
        }
        public bool ChangePass(string _sNewUN, string sUserPass, string sOldUN)
        {
            bool _bReturTF = false;
            try
            {
                bool _bNewUNIsExist = false;
                _bNewUNIsExist = ExecuteQueryFunctions.ExeSclr(_conn, "SELECT * FROM User_Table AS ut WHERE user_name='" + _sNewUN + "'");
                if (_bNewUNIsExist)
                {
                    throw new Exception("This user name already exist. Try another please.");
                }
                else
                {
                    string QueryString = "UPDATE User_Table SET user_name='" + _sNewUN + "',user_pass='" + sUserPass + "',UpdateBy=" + EMSGlobal._nCurrentUserID + "  WHERE user_name='" + sOldUN + "' AND user_id=" + EMSGlobal._nCurrentUserID;

                    ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
                    _bReturTF = true;
                }
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return _bReturTF;

        }
        public bool PWReset(int nUserID)
        {
            bool _bReturTF = false;
            try
            {
                NewUser oNewUser = new NewUser();
                IDataReader reader = ExecuteQueryFunctions.ExeReader(_conn, "SELECT * FROM User_Table AS ut WHERE [user_id]=" + nUserID);
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oNewUser = CreateObject(oReader);
                }
                reader.Close();
                _conn.Close();

                string QueryString = "UPDATE User_Table SET user_name='" + oNewUser.user_name_ini + "',user_pass='a1234567',UpdateBy=" + EMSGlobal._nCurrentUserID + "  WHERE user_id=" + nUserID;

                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
                _bReturTF = true;

            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return _bReturTF;
        }
        public bool UpdateUser_IsLogon(int nUserID, bool LogUnlog)
        {
            try
            {
                string QueryString = "UPDATE User_Table SET user_islogon='" + LogUnlog + "' WHERE user_id=" + nUserID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
        }
        #endregion
    }
}
