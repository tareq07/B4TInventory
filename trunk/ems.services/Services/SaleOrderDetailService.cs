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


namespace EMS.Services
{
    [Serializable]
    public class SaleOrderDetailService : MarshalByRefObject, ISaleOrderDetailService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(SaleOrderDetail oSaleOrderDetail, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oSaleOrderDetail, new ID(oReader.GetInt32("SaleID")));

            oSaleOrderDetail.SONumber = oReader.GetString("SONumber");

            oSaleOrderDetail.saleDate = oReader.GetDateTime("saleDate");

            oSaleOrderDetail.PONumber = oReader.GetString("PONumber");

            oSaleOrderDetail.printBillRef = oReader.GetString("printBillRef");

            oSaleOrderDetail.memoNo = oReader.GetString("memoNo");

            oSaleOrderDetail.Pro_ID = oReader.GetInt32("Pro_ID");

            oSaleOrderDetail.discount = oReader.GetDouble("discount");

            oSaleOrderDetail.newCus = oReader.GetString("newCus");

            oSaleOrderDetail.newCusContact = oReader.GetString("newCusContact");

            oSaleOrderDetail.PartyID = oReader.GetString("PartyID");

            oSaleOrderDetail.prodUnit = oReader.GetString("prodUnit");

            oSaleOrderDetail.bagQty = oReader.GetDouble("bagQty");

            oSaleOrderDetail.unitPrice = oReader.GetDouble("unitPrice");

            oSaleOrderDetail.unitPerBag = oReader.GetDouble("unitPerBag");

            oSaleOrderDetail.transportCost = oReader.GetDouble("transportCost");

            oSaleOrderDetail.returnQty = oReader.GetDouble("returnQty");

            oSaleOrderDetail.totalSaleQty = oReader.GetDouble("totalSaleQty");

            oSaleOrderDetail.totalAmount = oReader.GetDouble("totalAmount");

            oSaleOrderDetail.comments = oReader.GetString("comments");

            BusinessObject.Factory.SetObjectState(oSaleOrderDetail, ObjectState.Saved);
        }
        private SaleOrderDetail CreateObject(NullHandler oReader)
        {
            SaleOrderDetail oSaleOrderDetail = new SaleOrderDetail();
            MapObject(oSaleOrderDetail, oReader);
            return oSaleOrderDetail;
        }
        private SaleOrderDetails CreateObjects(IDataReader oReader)
        {
            SaleOrderDetails oSaleOrderDetails = new SaleOrderDetails();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                SaleOrderDetail oItem = CreateObject(oHandler);
                oSaleOrderDetails.Add(oItem);
            }
            return oSaleOrderDetails;
        }
        #endregion

        #region Interface implementation
        public SaleOrderDetailService() { }
        public ID Save(SaleOrderDetail oSaleOrderDetail)
        {
            try
            {
                if (oSaleOrderDetail.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_SaleOrderDetail_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SONumber", SqlDbType.VarChar)).Value = oSaleOrderDetail.SONumber;
                    cmd.Parameters.Add(new SqlParameter("@saleDate", SqlDbType.Date)).Value = oSaleOrderDetail.saleDate;
                    cmd.Parameters.Add(new SqlParameter("@PONumber", SqlDbType.VarChar)).Value = oSaleOrderDetail.PONumber;
                    cmd.Parameters.Add(new SqlParameter("@printBillRef", SqlDbType.VarChar)).Value = oSaleOrderDetail.printBillRef;
                    cmd.Parameters.Add(new SqlParameter("@memoNo", SqlDbType.VarChar)).Value = oSaleOrderDetail.memoNo;
                    cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.Int)).Value = oSaleOrderDetail.Pro_ID;
                    cmd.Parameters.Add(new SqlParameter("@discount", SqlDbType.Decimal)).Value = oSaleOrderDetail.discount;
                    cmd.Parameters.Add(new SqlParameter("@newCus", SqlDbType.VarChar)).Value = oSaleOrderDetail.newCus;
                    cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.VarChar)).Value = oSaleOrderDetail.PartyID;
                    cmd.Parameters.Add(new SqlParameter("@prodUnit", SqlDbType.VarChar)).Value = oSaleOrderDetail.prodUnit;
                    cmd.Parameters.Add(new SqlParameter("@bagQty", SqlDbType.Decimal)).Value = oSaleOrderDetail.bagQty;
                    cmd.Parameters.Add(new SqlParameter("@unitPrice", SqlDbType.Decimal)).Value = oSaleOrderDetail.unitPrice;
                    cmd.Parameters.Add(new SqlParameter("@unitPerBag", SqlDbType.Decimal)).Value = oSaleOrderDetail.unitPerBag;
                    cmd.Parameters.Add(new SqlParameter("@transportCost", SqlDbType.Decimal)).Value = oSaleOrderDetail.transportCost;
                    cmd.Parameters.Add(new SqlParameter("@returnQty", SqlDbType.Decimal)).Value = oSaleOrderDetail.returnQty;
                    cmd.Parameters.Add(new SqlParameter("@totalSaleQty", SqlDbType.Decimal)).Value = oSaleOrderDetail.totalSaleQty;
                    cmd.Parameters.Add(new SqlParameter("@totalAmount", SqlDbType.Decimal)).Value = oSaleOrderDetail.totalAmount;
                    cmd.Parameters.Add(new SqlParameter("@comments", SqlDbType.VarChar)).Value = oSaleOrderDetail.comments;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    int NewID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oSaleOrderDetail, new ID(NewID));
                }
                BusinessObject.Factory.SetObjectState(oSaleOrderDetail, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oSaleOrderDetail.ID;
        }
        public bool UpDateSaleOrderDetail(SaleOrderDetail oSaleOrderDetail, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SaleOrderDetail_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SONumber", SqlDbType.VarChar)).Value = oSaleOrderDetail.SONumber;
                cmd.Parameters.Add(new SqlParameter("@saleDate", SqlDbType.Date)).Value = oSaleOrderDetail.saleDate;
                cmd.Parameters.Add(new SqlParameter("@PONumber", SqlDbType.VarChar)).Value = oSaleOrderDetail.PONumber;
                cmd.Parameters.Add(new SqlParameter("@printBillRef", SqlDbType.VarChar)).Value = oSaleOrderDetail.printBillRef;
                cmd.Parameters.Add(new SqlParameter("@memoNo", SqlDbType.VarChar)).Value = oSaleOrderDetail.memoNo;
                cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.Int)).Value = oSaleOrderDetail.Pro_ID;
                cmd.Parameters.Add(new SqlParameter("@discount", SqlDbType.Decimal)).Value = oSaleOrderDetail.discount;
                cmd.Parameters.Add(new SqlParameter("@newCus", SqlDbType.VarChar)).Value = oSaleOrderDetail.newCus;
                cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.VarChar)).Value = oSaleOrderDetail.PartyID;
                cmd.Parameters.Add(new SqlParameter("@prodUnit", SqlDbType.VarChar)).Value = oSaleOrderDetail.prodUnit;
                cmd.Parameters.Add(new SqlParameter("@bagQty", SqlDbType.Decimal)).Value = oSaleOrderDetail.bagQty;
                cmd.Parameters.Add(new SqlParameter("@unitPrice", SqlDbType.Decimal)).Value = oSaleOrderDetail.unitPrice;
                cmd.Parameters.Add(new SqlParameter("@unitPerBag", SqlDbType.Decimal)).Value = oSaleOrderDetail.unitPerBag;
                cmd.Parameters.Add(new SqlParameter("@transportCost", SqlDbType.Decimal)).Value = oSaleOrderDetail.transportCost;
                cmd.Parameters.Add(new SqlParameter("@returnQty", SqlDbType.Decimal)).Value = oSaleOrderDetail.returnQty;
                cmd.Parameters.Add(new SqlParameter("@totalSaleQty", SqlDbType.Decimal)).Value = oSaleOrderDetail.totalSaleQty;
                cmd.Parameters.Add(new SqlParameter("@totalAmount", SqlDbType.Decimal)).Value = oSaleOrderDetail.totalAmount;
                cmd.Parameters.Add(new SqlParameter("@comments", SqlDbType.VarChar)).Value = oSaleOrderDetail.comments;
                cmd.Parameters.Add(new SqlParameter("@DBUUID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@SaleID", SqlDbType.Int)).Value = nEditID;
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
                SqlCommand cmd = new SqlCommand("SP_SaleOrderDetail_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SaleID", SqlDbType.Int)).Value = nID;
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
        public SaleOrderDetail GetByID(int nID)
        {
            SaleOrderDetail oSaleOrderDetail = new SaleOrderDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SaleOrderDetail_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SaleID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oSaleOrderDetail = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oSaleOrderDetail;
        }
        public SaleOrderDetails Gets()
        {
            SaleOrderDetails oSaleOrderDetails = new SaleOrderDetails();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SaleOrderDetail_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oSaleOrderDetails = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oSaleOrderDetails;
        }
        public SaleOrderDetails GetsByID(int nID)
        {
            SaleOrderDetails oSaleOrderDetails = new SaleOrderDetails();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SaleOrderDetail_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SaleID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oSaleOrderDetails = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oSaleOrderDetails;
        }
        public SaleOrderDetails GetsByString(string sString)
        {
            SaleOrderDetails oSaleOrderDetails = new SaleOrderDetails();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SaleOrderDetail_GetsByString", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SaleID", SqlDbType.VarChar)).Value = sString;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oSaleOrderDetails = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oSaleOrderDetails;
        }
        public DataTable GetsbyDT(string sSQL)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(sSQL, _conn);

                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add(new SqlParameter("@SaleID", SqlDbType.VarChar)).Value = sSQL;
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
        public string InsertWholeSaleXml(XmlDocument _XmlBills)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Insert_WholeSale", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InsertWholeSale", SqlDbType.Xml));
                cmd.Parameters[0].Value = _XmlBills.InnerXml.Substring(21);
                cmd.Connection.Open();
                string Result = Convert.ToString(cmd.ExecuteScalar());
                cmd.Dispose();
                _conn.Close();
                return Result;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        public int UpdateSOD(XmlDocument _XmlBills)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SaleReturn_Xml", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SaleReturn", SqlDbType.Xml));
                cmd.Parameters[0].Value = _XmlBills.InnerXml.Substring(21);
                cmd.Connection.Open();
                int Result = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                _conn.Close();
                return Result;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        #endregion

    }
}
