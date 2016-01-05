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
    public class PartyService : MarshalByRefObject, IPartyService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(Party oParty, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oParty, new ID(oReader.GetInt32("PartyID")));

            oParty.PartyType = (EnumParty)oReader.GetInt32("PartyType");

            oParty.AccountCode = oReader.GetString("AccountCode");

            oParty.PartyName = oReader.GetString("PartyName");

            oParty.Address = oReader.GetString("Address");

            oParty.Mobile = oReader.GetString("Mobile");

            oParty.Phone = oReader.GetString("Phone");

            oParty.Email = oReader.GetString("Email");

            oParty.IsActive = oReader.GetBoolean("IsActive");

            BusinessObject.Factory.SetObjectState(oParty, ObjectState.Saved);
        }
        private Party CreateObject(NullHandler oReader)
        {
            Party oParty = new Party();
            MapObject(oParty, oReader);
            return oParty;
        }
        private Partys CreateObjects(IDataReader oReader)
        {
            Partys oPartys = new Partys();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Party oItem = CreateObject(oHandler);
                oPartys.Add(oItem);
            }
            return oPartys;
        }
        #endregion

        #region Interface implementation
        public PartyService() { }
        public ID Save(Party oParty)
        {
            try
            {
                if (oParty.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_Party_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PartyType", SqlDbType.Int)).Value = (int)oParty.PartyType;
                    cmd.Parameters.Add(new SqlParameter("@AccountCode", SqlDbType.VarChar)).Value = oParty.AccountCode;
                    cmd.Parameters.Add(new SqlParameter("@PartyName", SqlDbType.VarChar)).Value = oParty.PartyName;
                    cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar)).Value = oParty.Address;
                    cmd.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.VarChar)).Value = oParty.Mobile;
                    cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar)).Value = oParty.Phone;
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar)).Value = oParty.Email;
                    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit)).Value = oParty.IsActive;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    string NewID = (string)cmd.ExecuteScalar();
                    oParty.AccountCode = NewID;
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oParty, new ID(1));
                }
                BusinessObject.Factory.SetObjectState(oParty, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oParty.ID;
        }
        public bool UpDateParty(Party oParty, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Party_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PartyType", SqlDbType.Int)).Value = (int)oParty.PartyType;
                cmd.Parameters.Add(new SqlParameter("@AccountCode", SqlDbType.VarChar)).Value = oParty.AccountCode;
                cmd.Parameters.Add(new SqlParameter("@PartyName", SqlDbType.VarChar)).Value = oParty.PartyName;
                cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.VarChar)).Value = oParty.Address;
                cmd.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.VarChar)).Value = oParty.Mobile;
                cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar)).Value = oParty.Phone;
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar)).Value = oParty.Email;
                cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit)).Value = oParty.IsActive;
                cmd.Parameters.Add(new SqlParameter("@DBUUID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.Int)).Value = nEditID;
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
        public bool Delete(string sAcCode)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Party_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@sAcCode", SqlDbType.NChar)).Value = sAcCode;
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
        public Party GetByID(int nID)
        {
            Party oParty = new Party();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Party_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oParty = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oParty;
        }
        public Partys Gets()
        {
            Partys oPartys = new Partys();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Party_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oPartys = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oPartys;
        }
        public Partys GetsByID(int nID)
        {
            Partys oPartys = new Partys();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Party_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oPartys = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oPartys;
        }
        public Partys GetsByString(string sString)
        {
            Partys oPartys = new Partys();
            try
            {
                SqlCommand cmd = new SqlCommand(sString, _conn);

                cmd.CommandType = CommandType.Text;

                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oPartys = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oPartys;
        }
        public DataTable GetsbyDT(string sSQL)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Party_GetsbyDT", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.VarChar)).Value = sSQL;
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
