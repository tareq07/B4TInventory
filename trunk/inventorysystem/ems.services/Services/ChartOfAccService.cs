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
    public class ChartOfAccService : MarshalByRefObject, IChartOfAccService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(ChartOfAcc oChartOfAcc, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oChartOfAcc, new ID(oReader.GetInt32("Coa_id")));

            oChartOfAcc.Coa_type = oReader.GetString("Coa_type");

            oChartOfAcc.Coa_parent_id = oReader.GetInt32("Coa_parent_id");

            oChartOfAcc.Coa_is_perent = oReader.GetBoolean("Coa_is_perent");

            oChartOfAcc.Coa_title = oReader.GetString("Coa_title");

            oChartOfAcc.Coa_level_code = oReader.GetString("Coa_level_code");

            oChartOfAcc.Coa_account_code = oReader.GetString("Coa_account_code");

            oChartOfAcc.Coa_legacy_code = oReader.GetString("Coa_legacy_code");

            oChartOfAcc.Coa_is_ledgerhead = oReader.GetBoolean("Coa_is_ledgerhead");

            BusinessObject.Factory.SetObjectState(oChartOfAcc, ObjectState.Saved);
        }
        private ChartOfAcc CreateObject(NullHandler oReader)
        {
            ChartOfAcc oChartOfAcc = new ChartOfAcc();
            MapObject(oChartOfAcc, oReader);
            return oChartOfAcc;
        }
        private ChartOfAccs CreateObjects(IDataReader oReader)
        {
            ChartOfAccs oChartOfAccs = new ChartOfAccs();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ChartOfAcc oItem = CreateObject(oHandler);
                oChartOfAccs.Add(oItem);
            }
            return oChartOfAccs;
        }
        #endregion

        #region Interface implementation
        public ChartOfAccService() { }
        public ID Save(ChartOfAcc oChartOfAcc)
        {
            try
            {
                if (oChartOfAcc.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_ChartOfAcc_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Coa_parent_id", SqlDbType.Int)).Value = oChartOfAcc.Coa_parent_id;
                    cmd.Parameters.Add(new SqlParameter("@Coa_is_perent", SqlDbType.Bit)).Value = oChartOfAcc.Coa_is_perent;
                    cmd.Parameters.Add(new SqlParameter("@Coa_type", SqlDbType.VarChar)).Value = oChartOfAcc.Coa_type;
                    cmd.Parameters.Add(new SqlParameter("@Coa_title", SqlDbType.VarChar)).Value = oChartOfAcc.Coa_title;
                    cmd.Parameters.Add(new SqlParameter("@Coa_level_code", SqlDbType.VarChar)).Value = oChartOfAcc.Coa_level_code;
                    cmd.Parameters.Add(new SqlParameter("@Coa_account_code", SqlDbType.VarChar)).Value = oChartOfAcc.Coa_account_code;
                    cmd.Parameters.Add(new SqlParameter("@Coa_legacy_code", SqlDbType.VarChar)).Value = oChartOfAcc.Coa_legacy_code;
                    cmd.Parameters.Add(new SqlParameter("@Coa_is_ledgerhead", SqlDbType.Bit)).Value = oChartOfAcc.Coa_is_ledgerhead;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    int NewID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oChartOfAcc, new ID(NewID));
                }
                BusinessObject.Factory.SetObjectState(oChartOfAcc, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oChartOfAcc.ID;
        }
        public bool UpDateChartOfAcc(ChartOfAcc oChartOfAcc, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ChartOfAcc_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Coa_parent_id", SqlDbType.Int)).Value = oChartOfAcc.Coa_parent_id;
                cmd.Parameters.Add(new SqlParameter("@Coa_is_perent", SqlDbType.Bit)).Value = oChartOfAcc.Coa_is_perent;
                cmd.Parameters.Add(new SqlParameter("@Coa_type", SqlDbType.VarChar)).Value = oChartOfAcc.Coa_type;
                cmd.Parameters.Add(new SqlParameter("@Coa_title", SqlDbType.VarChar)).Value = oChartOfAcc.Coa_title;
                cmd.Parameters.Add(new SqlParameter("@Coa_level_code", SqlDbType.VarChar)).Value = oChartOfAcc.Coa_level_code;
                cmd.Parameters.Add(new SqlParameter("@Coa_account_code", SqlDbType.VarChar)).Value = oChartOfAcc.Coa_account_code;
                cmd.Parameters.Add(new SqlParameter("@Coa_legacy_code", SqlDbType.VarChar)).Value = oChartOfAcc.Coa_legacy_code;
                cmd.Parameters.Add(new SqlParameter("@Coa_is_ledgerhead", SqlDbType.Bit)).Value = oChartOfAcc.Coa_is_ledgerhead;
                cmd.Parameters.Add(new SqlParameter("@UpdateUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@Coa_id", SqlDbType.Int)).Value = nEditID;
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
                SqlCommand cmd = new SqlCommand("SP_ChartOfAcc_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Coa_id", SqlDbType.Int)).Value = nID;
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
        public ChartOfAcc GetByID(int nID)
        {
            ChartOfAcc oChartOfAcc = new ChartOfAcc();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ChartOfAcc_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Coa_id", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oChartOfAcc = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oChartOfAcc;
        }
        public ChartOfAccs Gets()
        {
            ChartOfAccs oChartOfAccs = new ChartOfAccs();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ChartOfAcc_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oChartOfAccs = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oChartOfAccs;
        }
        public ChartOfAccs GetsByID(int nID)
        {
            ChartOfAccs oChartOfAccs = new ChartOfAccs();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ChartOfAcc_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Coa_id", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oChartOfAccs = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oChartOfAccs;
        }
        public ChartOfAccs GetsByString(string sString)
        {
            ChartOfAccs oChartOfAccs = new ChartOfAccs();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ChartOfAcc_GetsByString", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Coa_id", SqlDbType.VarChar)).Value = sString;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oChartOfAccs = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oChartOfAccs;
        }
        public DataTable GetsbyDT(string sSQL)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ChartOfAcc_GetsbyDT", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Coa_id", SqlDbType.VarChar)).Value = sSQL;
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
        public DataTable GetGLDetailbyDT()
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Func_GetGLDetails()", _conn);

                cmd.CommandType = CommandType.Text;
                
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
        public DataTable GetPeriodicGLbyDT(string sFrom, string sTO)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Func_GetPeriodicGL]('" + sFrom + "','" + sTO + "')", _conn);

                cmd.CommandType = CommandType.Text;

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
        public DataTable GetPLDetailbyDT(string sFrom, string sTO)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Func_GetIncomeStatement]('" + sFrom + "','" + sTO + "')", _conn);

                cmd.CommandType = CommandType.Text;

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
