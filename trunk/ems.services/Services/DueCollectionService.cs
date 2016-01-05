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
using System.Xml;


namespace EMS.Services.Services
{
    [Serializable]
    public class DueCollectionService : MarshalByRefObject, IDueCollectionService
    {
         #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(DueCollection oDueCollection, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oDueCollection, new ID(oReader.GetInt32("TrnID")));

            oDueCollection.TrnRef = oReader.GetString("TrnRef");

            oDueCollection.TranRefPrint = oReader.GetString("TranRefPrint");

            oDueCollection.LegacyRef = oReader.GetString("LegacyRef");

            oDueCollection.PartyType = (EnumParty)oReader.GetInt32("PartyType");

            oDueCollection.AccountCode = oReader.GetString("AccountCode");

            oDueCollection.PartyName = oReader.GetString("PartyName");

            oDueCollection.TranLastDate = oReader.GetDateTime("TranLastDate");

            oDueCollection.OutStdBalance = oReader.GetDouble("OutStdBalance");

            oDueCollection.TotalBill = oReader.GetDouble("TotalBill");

            oDueCollection.ReceivedBalance = oReader.GetDouble("PaidAmnt");

            BusinessObject.Factory.SetObjectState(oDueCollection, ObjectState.Saved);
        }
        private DueCollection CreateObject(NullHandler oReader)
        {
            DueCollection oDueCollection = new DueCollection();
            MapObject(oDueCollection, oReader);
            return oDueCollection;
        }
        private DueCollections CreateObjects(IDataReader oReader)
        {
            DueCollections oDueCollections = new DueCollections();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                DueCollection oItem = CreateObject(oHandler);
                oDueCollections.Add(oItem);
            }
            return oDueCollections;
        }
        #endregion

        #region Interface implementation
        public DueCollectionService() { }
        public ID Save(DueCollection oDueCollection)
        {
            try
            {
                if (oDueCollection.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_DueCollection_Transaction_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LegacyRef", SqlDbType.VarChar)).Value = oDueCollection.LegacyRef;
                    cmd.Parameters.Add(new SqlParameter("@AccountCode", SqlDbType.VarChar)).Value = oDueCollection.AccountCode;
                    //cmd.Parameters.Add(new SqlParameter("@DueCollectionOn", SqlDbType.VarChar)).Value = oDueCollection.DueCollectionOn;
                    cmd.Parameters.Add(new SqlParameter("@TotalBill", SqlDbType.Decimal)).Value = oDueCollection.TotalBill;
                    cmd.Parameters.Add(new SqlParameter("@ReceivedBalance", SqlDbType.Decimal)).Value = oDueCollection.ReceivedBalance;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    int NewID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oDueCollection, new ID(NewID));
                }
                BusinessObject.Factory.SetObjectState(oDueCollection, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oDueCollection.ID;
        }
        public bool DueBillCollection(DueCollection oDueCollection)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_DueCollection_OnDue_Insert", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TrnRef", SqlDbType.VarChar)).Value = oDueCollection.TrnRef;
                cmd.Parameters.Add(new SqlParameter("@paidType", SqlDbType.Int)).Value = oDueCollection.paidType;
                cmd.Parameters.Add(new SqlParameter("@bankAcId", SqlDbType.Int)).Value = oDueCollection.bankAcId;
                cmd.Parameters.Add(new SqlParameter("@ReceivedBalance", SqlDbType.Decimal)).Value = oDueCollection.ReceivedBalance;
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
        public bool UpDateDueCollection(DueCollection oDueCollection, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_DueCollection_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AccountCode", SqlDbType.VarChar)).Value = oDueCollection.AccountCode;
                //cmd.Parameters.Add(new SqlParameter("@DueCollectionOn", SqlDbType.VarChar)).Value = oDueCollection.DueCollectionOn;
                cmd.Parameters.Add(new SqlParameter("@OutStdBalance", SqlDbType.Decimal)).Value = oDueCollection.OutStdBalance;
                cmd.Parameters.Add(new SqlParameter("@UpdateUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@StuAcId", SqlDbType.Int)).Value = nEditID;
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
                SqlCommand cmd = new SqlCommand("SP_DueCollection_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StuAcId", SqlDbType.Int)).Value = nID;
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
        public bool ReverseDueBillCollection(string sBillRef)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_DueCollection_OnDue_Reverse", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@voucherNo", SqlDbType.VarChar)).Value = sBillRef;
               // cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
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
        public DueCollection GetByID(int nID)
        {
            DueCollection oDueCollection = new DueCollection();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_DueCollection_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StuAcId", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oDueCollection = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oDueCollection;
        }
        public DueCollections Gets()
        {
            DueCollections oDueCollections = new DueCollections();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_DueCollection_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oDueCollections = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oDueCollections;
        }
        public DueCollections GetsByID(int nID)
        {
            DueCollections oDueCollections = new DueCollections();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_DueCollection_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StuAcId", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oDueCollections = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oDueCollections;
        }
        public DueCollections GetsByString(string sString)
        {
            DueCollections oDueCollections = new DueCollections();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_DueCollection_GetsByString", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StuAcId", SqlDbType.VarChar)).Value = sString;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oDueCollections = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oDueCollections;
        }
        public DataTable GetsbyDT(string sSQL)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(sSQL, _conn);

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
        public DataTable GetsbyDT2(int nPartyType)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Func_GetDueBills("+nPartyType+")", _conn);

                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add(new SqlParameter("@PartyType", SqlDbType.Int)).Value = nPartyType;
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
        public DataTable GetsDueDetails(string sPartyAccountCode)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetDueBillsDetails", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@partyAccountCode", SqlDbType.VarChar)).Value = sPartyAccountCode;
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
        public bool InsertFeeCollectinXml(XmlDocument _XmlDoc)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_DueCollection_Insert_Details", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@XmlFeeCollect", SqlDbType.Xml));
                cmd.Parameters[0].Value = _XmlDoc.InnerXml.Substring(21);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                _conn.Close();
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        #endregion
    }
}
