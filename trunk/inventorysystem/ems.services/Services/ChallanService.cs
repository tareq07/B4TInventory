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
    public class ChallanService : MarshalByRefObject, IChallanService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(Challan oChallan, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oChallan, new ID(oReader.GetInt32("ChallanID")));

            oChallan.ChallanType = oReader.GetString("ChallanType");

            oChallan.ChallanNo = oReader.GetString("ChallanNo");

            oChallan.Description = oReader.GetString("Description");

            BusinessObject.Factory.SetObjectState(oChallan, ObjectState.Saved);
        }
        private Challan CreateObject(NullHandler oReader)
        {
            Challan oChallan = new Challan();
            MapObject(oChallan, oReader);
            return oChallan;
        }
        private Challans CreateObjects(IDataReader oReader)
        {
            Challans oChallans = new Challans();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Challan oItem = CreateObject(oHandler);
                oChallans.Add(oItem);
            }
            return oChallans;
        }
        #endregion

        #region Interface implementation
        public ChallanService() { }
        public ID Save(Challan oChallan)
        {
            try
            {
                if (oChallan.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_Challan_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ChallanType", SqlDbType.VarChar)).Value = oChallan.ChallanType;
                    cmd.Parameters.Add(new SqlParameter("@ChallanNo", SqlDbType.VarChar)).Value = oChallan.ChallanNo;
                    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar)).Value = oChallan.Description;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    int NewID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oChallan, new ID(NewID));
                }
                BusinessObject.Factory.SetObjectState(oChallan, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oChallan.ID;
        }
        public bool UpDateChallan(Challan oChallan, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Challan_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ChallanType", SqlDbType.VarChar)).Value = oChallan.ChallanType;
                cmd.Parameters.Add(new SqlParameter("@ChallanNo", SqlDbType.VarChar)).Value = oChallan.ChallanNo;
                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar)).Value = oChallan.Description;
                cmd.Parameters.Add(new SqlParameter("@DBUUID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@ChallanID", SqlDbType.Int)).Value = nEditID;
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
                SqlCommand cmd = new SqlCommand("SP_Challan_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ChallanID", SqlDbType.Int)).Value = nID;
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
        public Challan GetByID(int nID)
        {
            Challan oChallan = new Challan();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Challan_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ChallanID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oChallan = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oChallan;
        }
        public Challans Gets()
        {
            Challans oChallans = new Challans();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Challan_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oChallans = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oChallans;
        }
        public Challans GetsByID(int nID)
        {
            Challans oChallans = new Challans();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Challan_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ChallanID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oChallans = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oChallans;
        }
        public Challans GetsByString(string sString)
        {
            Challans oChallans = new Challans();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Challan_GetsByString", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ChallanID", SqlDbType.VarChar)).Value = sString;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oChallans = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oChallans;
        }
        public DataTable GetsbyDT(string sSQL)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Challan_GetsbyDT", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ChallanID", SqlDbType.VarChar)).Value = sSQL;
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
