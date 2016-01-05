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
    public class ManuInterfaceService : MarshalByRefObject, IManuInterfaceService
    {
        #region Connection String
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(ManuInterface oManuInterface, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oManuInterface, new ID(oReader.GetInt32("Menu_id")));

            oManuInterface.Parent_id = oReader.GetInt32("Parent_id");
            oManuInterface.Is_parent = oReader.GetBoolean("Is_parent");
            oManuInterface.Sub_parent = oReader.GetBoolean("Sub_parent");
            oManuInterface.Menu_title = oReader.GetString("Menu_title");
            oManuInterface.IsCheck = oReader.GetBoolean("IsCheck");
            oManuInterface.NevigetUrl = oReader.GetString("Nav_url");

            BusinessObject.Factory.SetObjectState(oManuInterface, ObjectState.Saved);
        }

        private ManuInterface CreateObject(NullHandler oReader)
        {
            ManuInterface oManuInterface = new ManuInterface();
            MapObject(oManuInterface, oReader);
            return oManuInterface;
        }

        private ManuInterfaces CreateObjects(IDataReader oReader)
        {
            ManuInterfaces oManuInterfaces = new ManuInterfaces();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ManuInterface oItem = CreateObject(oHandler);
                oManuInterfaces.Add(oItem);
            }
            return oManuInterfaces;
        }
        #endregion

        #region Interface implementation
        public ManuInterfaceService() { }

        public ID Save(ManuInterface oManuInterface)
        {
            try
            {

                if (oManuInterface.IsNew)
                {
                    BusinessObject.Factory.SetID(oManuInterface, new ID(ExecuteQueryFunctions.GetNewID(_conn, "SELECT MAX(Menu_id) FROM Menu_Table")));
                    //ManuInterfaceDA.Insert(conn, oManuInterface);
                }
                //else
                //{
                //    ManuInterfaceDA.Update(conn, oManuInterface);
                //}
                //conn.Close();
                BusinessObject.Factory.SetObjectState(oManuInterface, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
            return oManuInterface.ID;
        }
        public void Delete(int oID)
        {
            try
            {

            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        public void UpdateUser_IsLock(int oID, bool LockUnlock)
        {
            try
            {
                SqlConnection conn = new SqlConnection(EMSConFunc.ConString());

                string QueryString = "UPDATE User_Table SET user_lock='" + LockUnlock + "' WHERE user_id=" + oID;

                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }



        public void UserStatus(int CurrentUserID, int _nUStatus)
        {
            try
            {
                string QueryString = "UPDATE User_Table SET user_status=" + _nUStatus + " WHERE user_id=" + CurrentUserID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        public void LogOut(int CurrentUserID, bool bLogOut)
        {
            try
            {
                string QueryString = "UPDATE User_Table SET user_islogon=0 WHERE [user_id]=" + CurrentUserID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);

            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        public ManuInterface Get(int id)
        {
            ManuInterface oManuInterface = new ManuInterface();
            try
            {
                string QueryString = "SELECT Menu_Table.*,(SELECT CAST(CASE WHEN Menu_id=1 THEN 1 ELSE 0 END AS bit)) as IsCheck FROM Menu_Table Where Menu_id=" + id + " ORDER BY Menu_id";

                IDataReader reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oManuInterface = CreateObject(oReader);
                }
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }

            return oManuInterface;
        }

        public ManuInterfaces Gets(int nUserID)
        {
            ManuInterfaces oManuInterfaces = null;

            try
            {
                string QueryString = "SELECT Menu_Table.*,(SELECT CAST(CASE WHEN UPT.MenuID = Menu_Table.Menu_id and user_id=" + nUserID + " THEN 1 ELSE 0 END AS bit)) as IsCheck FROM Menu_Table Left Outer Join Menu_Permission_Table AS UPT ON Menu_Table.Menu_id=UPT.MenuID and UPT.user_id=" + nUserID + " ORDER BY Menu_id";

                IDataReader reader = null;
                reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                oManuInterfaces = CreateObjects(reader);
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }

            return oManuInterfaces;
        }

        #endregion
    }
}
