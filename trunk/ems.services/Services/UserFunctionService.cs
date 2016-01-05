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
    public class UserFunctionService : MarshalByRefObject, IUserFunctionService
    {
        #region ConnectionString
         SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(UserFunction oUserFunction, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oUserFunction, new ID(oReader.GetInt32("UFID")));
            oUserFunction.Is_Parent = oReader.GetBoolean("Is_Parent");
            oUserFunction.ParentID = oReader.GetInt32("ParentID");
            oUserFunction.Function_Name = oReader.GetString("Function_Name");
            oUserFunction.IsCheck = oReader.GetBoolean("IsCheck");
            
            
            BusinessObject.Factory.SetObjectState(oUserFunction, ObjectState.Saved);
        }

        private UserFunction CreateObject(NullHandler oReader)
        {
            UserFunction oUserFunction = new UserFunction();
            MapObject(oUserFunction, oReader);
            return oUserFunction;
        }

        private UserFunctions CreateObjects(IDataReader oReader)
        {
            UserFunctions oUserFunctions = new UserFunctions();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                UserFunction oItem = CreateObject(oHandler);
                oUserFunctions.Add(oItem);
            }
            return oUserFunctions;
        }
        #endregion

        #region Interface implementation
        public UserFunctionService() { }

        //public ID Save(UserFunction oUserFunction)
        //{
            
        //    try
        //    {
        //        SqlConnection conn = new SqlConnection(_connectionString);
        //        conn.Open();
                
        //        if (oUserFunction.IsNew)
        //        {
        //            BusinessObject.Factory.SetID(oUserFunction, new ID(ExecuteQueryFunctions.GetNewID(conn, "SELECT MAX(id) FROM Table")));
        //            string QueryString = "INSERT INTO User_Function_table (UFID,Function_Name,Function_AddedBy,DBSDT)"
        //                                    + "VALUES(" +
        //                                    oUserFunction.ObjectID + ",'" +
        //                                    oUserFunction.Function_Name + "'," +
        //                                    EMSGlobal._nCurrentUserID + ",'" +
        //                                    DateTime.Now + "')";
        //            ExecuteQueryFunctions.ExeNonQuery(conn, QueryString);
        //        }
        //        else
        //        {
        //            UserFunctionDA.Update(conn, oUserFunction);
        //        }

        //        conn.Close();
        //        BusinessObject.Factory.SetObjectState(oUserFunction, ObjectState.Saved);
        //    }            
        //    catch (Exception e)
        //    {               
        //        throw new ServiceException(e.Message, e);                
        //    }
        //    return oUserFunction.ID;
        //}
        //public void Delete(int oID)
        //{
            
        //    try
        //    {
        //        SqlConnection conn = new SqlConnection(_connectionString);
        //        UserFunctionDA.Delete(conn, oID);

        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ServiceException(e.Message, e);
        //    }
        //}       
        //public UserFunction Get(int id)
        //{
        //    UserFunction oUserFunction = new UserFunction();
           
        //    try
        //    {
        //        SqlConnection conn = new SqlConnection(_connectionString);

        //        IDataReader reader = UserFunctionDA.Get(conn, id);
        //        NullHandler oReader = new NullHandler(reader);
        //        if (reader.Read())
        //        {
        //            oUserFunction = CreateObject(oReader);
        //        }
        //        reader.Close();
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ServiceException(e.Message, e);
        //    }

        //    return oUserFunction;
        //}
        public UserFunctions Get()
        {
            UserFunctions oUserFunctions = null;           

            try
            {
                
                string QueryString = "SELECT * FROM User_Function_table";
                IDataReader reader = null;
                reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                oUserFunctions = CreateObjects(reader);
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }

            return oUserFunctions;
        }
        public UserFunctions Gets(int nID)
        {
            UserFunctions oUserFunctions = null;   

            try
            {
                string QueryString = "SELECT User_Function_table.*,(SELECT CAST(CASE WHEN FPT.UFID = User_Function_table.UFID and [user_id]=" + nID + " THEN 1 ELSE 0 END AS bit)) as IsCheck FROM User_Function_table Left Outer Join Function_permission_Table AS FPT ON User_Function_table.UFID=FPT.UFID and FPT.[user_id]=" + nID;
                IDataReader reader = null;
                reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                oUserFunctions = CreateObjects(reader);
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {               
                throw new ServiceException(e.Message, e);                
            }

            return oUserFunctions;
        }


        
        #endregion
    }
}


