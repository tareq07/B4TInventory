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
    public class MenuPermissionService : MarshalByRefObject, IMenuPermissionService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion
        #region Private functions and declaration
        private void MapObject(MenuPermission oMenuPermission, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oMenuPermission, new ID(oReader.GetInt32("UPID")));

            oMenuPermission.MenuID = oReader.GetInt32("MenuID");
            oMenuPermission.UserID = oReader.GetInt32("user_id");


            BusinessObject.Factory.SetObjectState(oMenuPermission, ObjectState.Saved);
        }

        private MenuPermission CreateObject(NullHandler oReader)
        {
            MenuPermission oMenuPermission = new MenuPermission();
            MapObject(oMenuPermission, oReader);
            return oMenuPermission;
        }

        private MenuPermissions CreateObjects(IDataReader oReader)
        {
            MenuPermissions oMenuPermissions = new MenuPermissions();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                MenuPermission oItem = CreateObject(oHandler);
                oMenuPermissions.Add(oItem);
            }
            return oMenuPermissions;
        }
        #endregion

        #region Interface implementation
        public MenuPermissionService() { }

        public ID Save(MenuPermission oMenuPermission)
        {
            bool bIsExist = false;
            try
            {
                if (oMenuPermission.IsNew)
                {
                    string QueryString = "SELECT COUNT(*) FROM Menu_Permission_Table WHERE MenuID=" + oMenuPermission.MenuID + " AND user_id=" + oMenuPermission.UserID;
                    bIsExist = ExecuteQueryFunctions.ExeSclr(_conn, QueryString);
                    if (!bIsExist)
                    {
                        BusinessObject.Factory.SetID(oMenuPermission, new ID(ExecuteQueryFunctions.GetNewID(_conn, "SELECT MAX(UPID) FROM Menu_Permission_Table")));
                        string QueryString2 = "INSERT INTO Menu_Permission_Table (UPID,MenuID,user_id,DBUserID,DBSDT)"
                                            + "VALUES(" +
                                            oMenuPermission.ObjectID + "," +
                                            oMenuPermission.MenuID + "," +
                                            oMenuPermission.UserID + "," +
                                            EMSGlobal._nCurrentUserID + ",'" +
                                            DateTime.Now + "')";
                        ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString2);
                    }
                }
                BusinessObject.Factory.SetObjectState(oMenuPermission, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
            return oMenuPermission.ID;
        }

        public void DeletePermission(int nMenuID, int nUserID)
        {

            try
            {
                string QueryString = "DELETE FROM Menu_Permission_Table WHERE MenuID=" + nMenuID + " AND user_id=" + nUserID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        public void DeletePermissions(int nID)
        {
            try
            {

                string QueryString = "DELETE FROM Menu_Permission_Table WHERE user_id=" + nID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        //public MenuPermission Get(int id)
        //{
        //    MenuPermission oMenuPermission = new MenuPermission();

        //    try
        //    {
        //       

        //        IDataReader reader = MenuPermissionDA.Get(_conn, id);
        //        NullHandler oReader = new NullHandler(reader);
        //        if (reader.Read())
        //        {
        //            oMenuPermission = CreateObject(oReader);
        //        }
        //        reader.Close();
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ServiceException(e.Message);
        //    }

        //    return oMenuPermission;
        //}
        //public MenuPermissions Get()
        //{
        //    MenuPermissions oMenuPermissions = null;           

        //    try
        //    {
        //        

        //        IDataReader reader = null;
        //        reader = MenuPermissionDA.Gets(_conn);
        //        oMenuPermissions = CreateObjects(reader);
        //        reader.Close();
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ServiceException(e.Message);
        //    }

        //    return oMenuPermissions;
        //}
        //public MenuPermissions Gets(int nUserID)
        //{
        //    MenuPermissions oMenuPermissions = null;           

        //    try
        //    {
        //        

        //        IDataReader reader = null;
        //        reader = MenuPermissionDA.GetByUserID(_conn, nUserID);
        //        oMenuPermissions = CreateObjects(reader);
        //        reader.Close();
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ServiceException(e.Message);
        //    }

        //    return oMenuPermissions;
        //}
        //public DataSet GetsTree(int nUserID)
        //{           
        //    DataSet oDataSet = new DataSet();
        //    try
        //    {
        //       

        //        IDataReader reader = MenuPermissionDA.GetsTree(_conn, nUserID);
        //        oDataSet.Load(reader, LoadOption.OverwriteChanges, new string[1]);
        //        reader.Close();
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ServiceException(e.Message);
        //    }
        //    return oDataSet;
        //}

        #endregion
    }
}

