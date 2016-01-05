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
    public class TransactionService : MarshalByRefObject, ITransactionService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion


        #region Private functions and declaration
        private void MapObject(Transaction oTransaction, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oTransaction, new ID(oReader.GetInt32("TrnID")));

            oTransaction.TranRef = oReader.GetString("TranRef");

            oTransaction.LegacyRef = oReader.GetString("LegacyRef");

            oTransaction.TranHead = oReader.GetString("TranHead");

            oTransaction.CurrBalance = oReader.GetDouble("CurrBalance");

            oTransaction.DrAmount = oReader.GetDouble("DrAmount");

            oTransaction.CrAmount = oReader.GetDouble("CrAmount");

            oTransaction.TranDate = oReader.GetDateTime("TranDate");

            oTransaction.Perticular = oReader.GetString("Perticular");

            BusinessObject.Factory.SetObjectState(oTransaction, ObjectState.Saved);
        }
        private Transaction CreateObject(NullHandler oReader)
        {
            Transaction oTransaction = new Transaction();
            MapObject(oTransaction, oReader);
            return oTransaction;
        }
        private Transactions CreateObjects(IDataReader oReader)
        {
            Transactions oTransactions = new Transactions();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Transaction oItem = CreateObject(oHandler);
                oTransactions.Add(oItem);
            }
            return oTransactions;
        }
        #endregion

        #region Interface implementation
        public TransactionService() { }
        public ID Save(Transaction oTransaction)
        {
            try
            {
                if (oTransaction.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_Transaction_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LegacyRef", SqlDbType.VarChar)).Value = oTransaction.LegacyRef;
                    cmd.Parameters.Add(new SqlParameter("@TranDate", SqlDbType.Date)).Value = oTransaction.TranDate;
                    cmd.Parameters.Add(new SqlParameter("@DebitAcc", SqlDbType.VarChar)).Value = oTransaction.DebitAcc;
                    cmd.Parameters.Add(new SqlParameter("@CreditAcc", SqlDbType.VarChar)).Value = oTransaction.CreditAcc;
                    cmd.Parameters.Add(new SqlParameter("@DrAmount", SqlDbType.Decimal)).Value = oTransaction.DrAmount;
                    cmd.Parameters.Add(new SqlParameter("@CrAmount", SqlDbType.Decimal)).Value = oTransaction.CrAmount;                   
                    cmd.Parameters.Add(new SqlParameter("@Perticular", SqlDbType.VarChar)).Value = oTransaction.Perticular;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    int NewID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oTransaction, new ID(NewID));
                }
                BusinessObject.Factory.SetObjectState(oTransaction, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oTransaction.ID;
        }
        public string InsertTransaction(Transaction oTransaction)
        {
            string Result = string.Empty;
            try
            {
                
                if (oTransaction.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_Transaction_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@transactionType", SqlDbType.Int)).Value = 3; //Transaction Type. GL account
                    cmd.Parameters.Add(new SqlParameter("@tranRef", SqlDbType.VarChar)).Value = '3'; //for check GL transaction or other
                    cmd.Parameters.Add(new SqlParameter("@tranRefPrint", SqlDbType.VarChar)).Value = '3'; //for check GL transaction or other
                    cmd.Parameters.Add(new SqlParameter("@LegacyRef", SqlDbType.VarChar)).Value = oTransaction.LegacyRef;
                    cmd.Parameters.Add(new SqlParameter("@TranDate", SqlDbType.Date)).Value = oTransaction.TranDate;
                    cmd.Parameters.Add(new SqlParameter("@DebitAcc", SqlDbType.VarChar)).Value = oTransaction.DebitAcc;
                    cmd.Parameters.Add(new SqlParameter("@CreditAcc", SqlDbType.VarChar)).Value = oTransaction.CreditAcc;
                    cmd.Parameters.Add(new SqlParameter("@DrAmount", SqlDbType.Decimal)).Value = oTransaction.DrAmount;
                    cmd.Parameters.Add(new SqlParameter("@CrAmount", SqlDbType.Decimal)).Value = oTransaction.CrAmount;
                    cmd.Parameters.Add(new SqlParameter("@Perticular", SqlDbType.VarChar)).Value = oTransaction.Perticular;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    Result = (string)cmd.ExecuteScalar();
                    cmd.Dispose();
                    cmd.Connection.Close();
                   
                }
                
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return Result;
        }
        public bool UpDateTransaction(Transaction oTransaction, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Transaction_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LegacyRef", SqlDbType.VarChar)).Value = oTransaction.LegacyRef;
                cmd.Parameters.Add(new SqlParameter("@DebitAcc", SqlDbType.VarChar)).Value = oTransaction.DebitAcc;
                cmd.Parameters.Add(new SqlParameter("@CreditAcc", SqlDbType.VarChar)).Value = oTransaction.CreditAcc;
                cmd.Parameters.Add(new SqlParameter("@DrAmount", SqlDbType.Decimal)).Value = oTransaction.DrAmount;
                cmd.Parameters.Add(new SqlParameter("@CrAmount", SqlDbType.Decimal)).Value = oTransaction.CrAmount;
                cmd.Parameters.Add(new SqlParameter("@TranDate", SqlDbType.Date)).Value = oTransaction.TranDate;
                cmd.Parameters.Add(new SqlParameter("@Perticular", SqlDbType.VarChar)).Value = oTransaction.Perticular;
                cmd.Parameters.Add(new SqlParameter("@UpdateUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@TrnID", SqlDbType.Int)).Value = nEditID;
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
                SqlCommand cmd = new SqlCommand("SP_Transaction_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TrnID", SqlDbType.Int)).Value = nID;
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
        public Transaction GetByID(int nID)
        {
            Transaction oTransaction = new Transaction();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Transaction_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TrnID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oTransaction = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oTransaction;
        }
        public Transactions Gets()
        {
            Transactions oTransactions = new Transactions();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Transaction_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oTransactions = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oTransactions;
        }
        public Transactions GetsByID(int nID)
        {
            Transactions oTransactions = new Transactions();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Transaction_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TrnID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oTransactions = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oTransactions;
        }
        public Transactions GetsByString(string sString)
        {
            Transactions oTransactions = new Transactions();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Transaction_GetsByString", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TrnID", SqlDbType.VarChar)).Value = sString;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oTransactions = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oTransactions;
        }
        public DataTable GetsbyDT(string sDateFrom, string sDateTo)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Transaction_GetsbyDT", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.VarChar)).Value = sDateFrom;
                cmd.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.VarChar)).Value = sDateTo;
                //cmd.Parameters.Add(new SqlParameter("@TranRef", SqlDbType.VarChar)).Value = sTranRef;
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
        public DataTable GetsbyDT2(string sAccountNo,string sDateFrom, string sDateTo)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetAccountStatement", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AccountNo", SqlDbType.VarChar)).Value = sAccountNo;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.VarChar)).Value = sDateFrom;
                cmd.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.VarChar)).Value = sDateTo;
               
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
        public string GetTrnRef()
        {
            int _nVGMID = 0;
            string sRetStr = "";
            string _flag = "000";
            try
            {
                string QueryString = "SELECT CONVERT(VARCHAR,ISNULL(MAX(dt.TranRef),0)) FROM tbl_DailyTransactions dt WHERE dt.DBSDT='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                sRetStr = ExecuteQueryFunctions.ExeRetStr(_conn, QueryString);
                if (sRetStr == "0")
                { sRetStr = "001"; }
                else
                {
                    _nVGMID = Convert.ToInt32(sRetStr.Substring(sRetStr.Length - 3, 3)) + 1;
                    _flag = _flag.Substring(0, _flag.Length - _nVGMID.ToString().Length);
                    sRetStr = _flag + _nVGMID;
                }
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return sRetStr;
        }
        public bool ReverseTransaction(string sTranReff)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Transaction_Reverse", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TranReff", SqlDbType.VarChar)).Value = sTranReff;
                cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
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
        #endregion

    }
}
