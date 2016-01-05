using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EMS.Core.Framework;
using EMS.Core;
using EMS.Core.Utility;
using System.Data;
using EMS.BusinessObjects;
using System.Configuration;
using System.Data.SqlClient;
using EMS.Core.DataAccess;


namespace EMS.Services
{
    [Serializable]
    public class UserSecurityService : MarshalByRefObject, IUserSecurityService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(UserSecurity oUserSecurity, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oUserSecurity, new ID(oReader.GetInt32("USID")));

            oUserSecurity.user_id = oReader.GetInt32("user_id");

            oUserSecurity.MacAddress = oReader.GetString("MacAddress");

            oUserSecurity.IsAuthorise = oReader.GetBoolean("IsAuthorise");

            oUserSecurity.AuthoriseBy = oReader.GetInt32("AuthoriseBy");

            oUserSecurity.RequestDate = oReader.GetDateTime("RequestDate");

            oUserSecurity.AuthoriseDate = oReader.GetDateTime("AuthoriseDate");

            BusinessObject.Factory.SetObjectState(oUserSecurity, ObjectState.Saved);
        }
        private UserSecurity CreateObject(NullHandler oReader)
        {
            UserSecurity oUserSecurity = new UserSecurity();
            MapObject(oUserSecurity, oReader);
            return oUserSecurity;
        }
        private UserSecuritys CreateObjects(IDataReader oReader)
        {
            UserSecuritys oUserSecuritys = new UserSecuritys();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                UserSecurity oItem = CreateObject(oHandler);
                oUserSecuritys.Add(oItem);
            }
            return oUserSecuritys;
        }
        #endregion

        #region Interface implementation
        public UserSecurityService() { }
        public ID Save(UserSecurity oUserSecurity)
        {
            try
            {
                if (oUserSecurity.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_UserSecurity_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@user_id", SqlDbType.Int)).Value = oUserSecurity.user_id;
                    cmd.Parameters.Add(new SqlParameter("@MacAddress", SqlDbType.VarChar)).Value = oUserSecurity.MacAddress;
                    cmd.Parameters.Add(new SqlParameter("@IsAuthorise", SqlDbType.Bit)).Value = oUserSecurity.IsAuthorise;
                    cmd.Parameters.Add(new SqlParameter("@AuthoriseBy", SqlDbType.Int)).Value = oUserSecurity.AuthoriseBy;
                    cmd.Parameters.Add(new SqlParameter("@RequestDate", SqlDbType.Date)).Value = oUserSecurity.RequestDate;
                    cmd.Parameters.Add(new SqlParameter("@AuthoriseDate", SqlDbType.Date)).Value = oUserSecurity.AuthoriseDate;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    int NewID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oUserSecurity, new ID(NewID));
                }
                BusinessObject.Factory.SetObjectState(oUserSecurity, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oUserSecurity.ID;
        }
        public bool UpDateUserSecurity(UserSecurity oUserSecurity, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UserSecurity_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@user_id", SqlDbType.Int)).Value = oUserSecurity.user_id;
                cmd.Parameters.Add(new SqlParameter("@MacAddress", SqlDbType.VarChar)).Value = oUserSecurity.MacAddress;
                cmd.Parameters.Add(new SqlParameter("@IsAuthorise", SqlDbType.Bit)).Value = oUserSecurity.IsAuthorise;
                cmd.Parameters.Add(new SqlParameter("@AuthoriseBy", SqlDbType.Int)).Value = oUserSecurity.AuthoriseBy;
                cmd.Parameters.Add(new SqlParameter("@RequestDate", SqlDbType.Date)).Value = oUserSecurity.RequestDate;
                cmd.Parameters.Add(new SqlParameter("@AuthoriseDate", SqlDbType.Date)).Value = oUserSecurity.AuthoriseDate;
                cmd.Parameters.Add(new SqlParameter("@UpdateUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@USID", SqlDbType.Int)).Value = nEditID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
        }
        public bool Delete(int nID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UserSecurity_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@USID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
        }
        public UserSecurity GetByID(int nID)
        {
            UserSecurity oUserSecurity = new UserSecurity();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UserSecurity_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@USID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oUserSecurity = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oUserSecurity;
        }
        public UserSecuritys Gets()
        {
            UserSecuritys oUserSecuritys = new UserSecuritys();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UserSecurity_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oUserSecuritys = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oUserSecuritys;
        }
        public UserSecuritys GetsByID(int nID)
        {
            UserSecuritys oUserSecuritys = new UserSecuritys();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UserSecurity_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@USID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oUserSecuritys = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oUserSecuritys;
        }
        public UserSecuritys GetsByString(string sString)
        {
            UserSecuritys oUserSecuritys = new UserSecuritys();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UserSecurity_GetsByString", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@USID", SqlDbType.VarChar)).Value = sString;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oUserSecuritys = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oUserSecuritys;
        }
        public DataTable GetsbyDT(string sSQL)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UserSecurity_GetsbyDT", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@USID", SqlDbType.VarChar)).Value = sSQL;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oDataSet.Load(reader, LoadOption.OverwriteChanges, new string[1]);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oDataSet.Tables[0];
        }
        #endregion

    }
}
