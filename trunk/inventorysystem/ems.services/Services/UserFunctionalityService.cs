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

namespace EMS.Services.Services
{
     [Serializable]
    public class UserFunctionalityService : MarshalByRefObject, IUserFunctionalityService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion
        #region Private functions and declaration
        private void MapObject(UserFunctionality oUserFunctionality, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oUserFunctionality, new ID(oReader.GetInt32("UFPID")));

            oUserFunctionality.UFID = oReader.GetInt32("UFID");
            oUserFunctionality.User_ID = oReader.GetInt32("user_id");
                        
            BusinessObject.Factory.SetObjectState(oUserFunctionality, ObjectState.Saved);
        }

        private UserFunctionality CreateObject(NullHandler oReader)
        {
            UserFunctionality oUserFunctionality = new UserFunctionality();
            MapObject(oUserFunctionality, oReader);
            return oUserFunctionality;
        }

        private UserFunctionalitys CreateObjects(IDataReader oReader)
        {
            UserFunctionalitys oUserFunctionalitys = new UserFunctionalitys();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                UserFunctionality oItem = CreateObject(oHandler);
                oUserFunctionalitys.Add(oItem);
            }
            return oUserFunctionalitys;
        }
        #endregion

        #region Interface implementation
        public UserFunctionalityService() { }

        public ID Save(UserFunctionality oUserFunctionality)
        {
            bool bIsExist = false;
            
            try
            {
                if (oUserFunctionality.IsNew)
                {
                    string QueryString = "SELECT COUNT(*) FROM Function_permission_Table WHERE UFID=" + oUserFunctionality.UFID + " AND user_id=" + oUserFunctionality.User_ID;
                    bIsExist = ExecuteQueryFunctions.ExeSclr(_conn, QueryString);
                    if (!bIsExist)
                    {
                        BusinessObject.Factory.SetID(oUserFunctionality, new ID(ExecuteQueryFunctions.GetNewID(_conn, "SELECT MAX(UFPID) FROM Function_permission_Table")));
                        string QueryString2 = "INSERT INTO Function_permission_Table (UFPID,UFID,user_id,DBUserID,DBSDT)"
                                            + "VALUES(" +
                                            oUserFunctionality.ObjectID + "," +
                                            oUserFunctionality.UFID + "," +
                                            oUserFunctionality.User_ID + "," +
                                            EMSGlobal._nCurrentUserID + ",'" +
                                            DateTime.Now + "')";
                        ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString2);
                    }
                }                
                BusinessObject.Factory.SetObjectState(oUserFunctionality, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oUserFunctionality.ID;
        }
        //public void Delete(int oID)
        //{
           
        //    try
        //    {
        //        SqlConnection conn = new SqlConnection(_connectionString);
        //        UserFunctionalityDA.Delete(conn, oID);
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ServiceException(e.Message, e);
        //    }
        //}

        public bool RemoveFunction(int nUFID,int nUserID)
        {
           
            try
            {                
                string QueryString = "DELETE Function_permission_Table WHERE UFID =" + nUFID + " AND user_id=" + nUserID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
                _conn.Close();
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
        }
        //public UserFunctionality Get(int id)
        //{
        //    UserFunctionality oUserFunctionality = new UserFunctionality();
           
        //    try
        //    {
        //        SqlConnection conn = new SqlConnection(_connectionString);

        //        IDataReader reader = UserFunctionalityDA.Get(conn, id);
        //        NullHandler oReader = new NullHandler(reader);
        //        if (reader.Read())
        //        {
        //            oUserFunctionality = CreateObject(oReader);
        //        }
        //        reader.Close();
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ServiceException(e.Message, e);
        //    }

        //    return oUserFunctionality;
        //}
        //public UserFunctionalitys Get()
        //{
        //    UserFunctionalitys oUserFunctionalitys = null;           

        //    try
        //    {
        //        SqlConnection conn = new SqlConnection(_connectionString);

        //        IDataReader reader = null;
        //        reader = UserFunctionalityDA.Gets(conn);
        //        oUserFunctionalitys = CreateObjects(reader);
        //        reader.Close();
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ServiceException(e.Message, e);
        //    }

        //    return oUserFunctionalitys;
        //}
        //public UserFunctionalitys Gets(int nID)
        //{
        //    UserFunctionalitys oUserFunctionalitys = null;           

        //    try
        //    {
        //        SqlConnection conn = new SqlConnection(_connectionString);

        //        IDataReader reader = null;
        //        reader = UserFunctionalityDA.Get(conn, nID);
        //        oUserFunctionalitys = CreateObjects(reader);
        //        reader.Close();
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ServiceException(e.Message, e);
        //    }

        //    return oUserFunctionalitys;
        //}
        
        #endregion
    }
}


