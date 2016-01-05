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
    public class PurchaseOrderDetailService : MarshalByRefObject, IPurchaseOrderDetailService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(PurchaseOrderDetail oPurchaseOrderDetail, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oPurchaseOrderDetail, new ID(oReader.GetInt32("PPID")));

            oPurchaseOrderDetail.lotNumber = oReader.GetString("lotNumber");

            oPurchaseOrderDetail.PartyID = oReader.GetString("PartyID");

            oPurchaseOrderDetail.Pro_ID = oReader.GetInt32("Pro_ID");

            oPurchaseOrderDetail.ProdName = oReader.GetString("ProdName");

            oPurchaseOrderDetail.prodUnit = oReader.GetString("prodUnit");

            oPurchaseOrderDetail.PONumber = oReader.GetString("PONumber");

            oPurchaseOrderDetail.memoNo = oReader.GetString("memoNo");

            oPurchaseOrderDetail.bagQty = oReader.GetDouble("bagQty");

            oPurchaseOrderDetail.unitPrice = oReader.GetDouble("unitPrice");

            oPurchaseOrderDetail.unitPerBag = oReader.GetDouble("unitPerBag");

            oPurchaseOrderDetail.totalQty = oReader.GetDouble("totalQty");

            oPurchaseOrderDetail.saleQtyBag = oReader.GetDouble("saleQtyBag");

            oPurchaseOrderDetail.saleQty = oReader.GetDouble("saleQty");

            oPurchaseOrderDetail.returnQty = oReader.GetDouble("returnQty");

            oPurchaseOrderDetail.wastageQty = oReader.GetDouble("wastageQty");

            oPurchaseOrderDetail.truckCost = oReader.GetDouble("truckCost");

            oPurchaseOrderDetail.labourCost = oReader.GetDouble("labourCost");

            oPurchaseOrderDetail.bagCost = oReader.GetDouble("bagCost");

            oPurchaseOrderDetail.otherCost = oReader.GetDouble("otherCost");

            oPurchaseOrderDetail.purchasePrice = oReader.GetDouble("purchasePrice");

            oPurchaseOrderDetail.comments = oReader.GetString("comments");

            BusinessObject.Factory.SetObjectState(oPurchaseOrderDetail, ObjectState.Saved);
        }
        private PurchaseOrderDetail CreateObject(NullHandler oReader)
        {
            PurchaseOrderDetail oPurchaseOrderDetail = new PurchaseOrderDetail();
            MapObject(oPurchaseOrderDetail, oReader);
            return oPurchaseOrderDetail;
        }
        private PurchaseOrderDetails CreateObjects(IDataReader oReader)
        {
            PurchaseOrderDetails oPurchaseOrderDetails = new PurchaseOrderDetails();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                PurchaseOrderDetail oItem = CreateObject(oHandler);
                oPurchaseOrderDetails.Add(oItem);
            }
            return oPurchaseOrderDetails;
        }
        #endregion

        #region Interface implementation
        public PurchaseOrderDetailService() { }
        public ID Save(PurchaseOrderDetail oPurchaseOrderDetail)
        {
            try
            {
                if (oPurchaseOrderDetail.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_PurchaseOrderDetail_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@lotNumber", SqlDbType.VarChar)).Value = oPurchaseOrderDetail.lotNumber;
                    cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.Int)).Value = oPurchaseOrderDetail.PartyID;
                    cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.Int)).Value = oPurchaseOrderDetail.Pro_ID;
                    //cmd.Parameters.Add(new SqlParameter("@PONumber", SqlDbType.VarChar)).Value = oPurchaseOrderDetail.PONumber;
                    cmd.Parameters.Add(new SqlParameter("@memoNo", SqlDbType.VarChar)).Value = oPurchaseOrderDetail.memoNo;
                    cmd.Parameters.Add(new SqlParameter("@bagQty", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.bagQty;
                    cmd.Parameters.Add(new SqlParameter("@unitPrice", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.unitPrice;
                    cmd.Parameters.Add(new SqlParameter("@unitPerBag", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.unitPerBag;
                    cmd.Parameters.Add(new SqlParameter("@totalQty", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.totalQty;
                    cmd.Parameters.Add(new SqlParameter("@saleQtyBag", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.saleQtyBag;
                    cmd.Parameters.Add(new SqlParameter("@saleQty", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.saleQty;
                    cmd.Parameters.Add(new SqlParameter("@returnQty", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.returnQty;
                    cmd.Parameters.Add(new SqlParameter("@wastageQty", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.wastageQty;
                    cmd.Parameters.Add(new SqlParameter("@truckCost", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.truckCost;
                    cmd.Parameters.Add(new SqlParameter("@labourCost", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.labourCost;
                    cmd.Parameters.Add(new SqlParameter("@bagCost", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.bagCost;
                    cmd.Parameters.Add(new SqlParameter("@otherCost", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.otherCost;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    int NewID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oPurchaseOrderDetail, new ID(NewID));
                }
                BusinessObject.Factory.SetObjectState(oPurchaseOrderDetail, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oPurchaseOrderDetail.ID;
        }
        public bool UpDatePurchaseOrderDetail(PurchaseOrderDetail oPurchaseOrderDetail, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_PurchaseOrderDetail_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@lotNumber", SqlDbType.VarChar)).Value = oPurchaseOrderDetail.lotNumber;
                cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.Int)).Value = oPurchaseOrderDetail.PartyID;
                cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.Int)).Value = oPurchaseOrderDetail.Pro_ID;
                cmd.Parameters.Add(new SqlParameter("@PONumber", SqlDbType.VarChar)).Value = oPurchaseOrderDetail.PONumber;
                cmd.Parameters.Add(new SqlParameter("@memoNo", SqlDbType.VarChar)).Value = oPurchaseOrderDetail.memoNo;
                cmd.Parameters.Add(new SqlParameter("@bagQty", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.bagQty;
                cmd.Parameters.Add(new SqlParameter("@unitPrice", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.unitPrice;
                cmd.Parameters.Add(new SqlParameter("@unitPerBag", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.unitPerBag;
                cmd.Parameters.Add(new SqlParameter("@totalQty", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.totalQty;
                cmd.Parameters.Add(new SqlParameter("@saleQtyBag", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.saleQtyBag;
                cmd.Parameters.Add(new SqlParameter("@saleQty", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.saleQty;
                cmd.Parameters.Add(new SqlParameter("@returnQty", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.returnQty;
                cmd.Parameters.Add(new SqlParameter("@wastageQty", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.wastageQty;
                cmd.Parameters.Add(new SqlParameter("@truckCost", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.truckCost;
                cmd.Parameters.Add(new SqlParameter("@labourCost", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.labourCost;
                cmd.Parameters.Add(new SqlParameter("@bagCost", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.bagCost;
                cmd.Parameters.Add(new SqlParameter("@otherCost", SqlDbType.Decimal)).Value = oPurchaseOrderDetail.otherCost;
                cmd.Parameters.Add(new SqlParameter("@DBUUID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@PPID", SqlDbType.Int)).Value = nEditID;
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
                SqlCommand cmd = new SqlCommand("SP_PurchaseOrderDetail_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PPID", SqlDbType.Int)).Value = nID;
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
        public PurchaseOrderDetail GetByID(int nID)
        {
            PurchaseOrderDetail oPurchaseOrderDetail = new PurchaseOrderDetail();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_PurchaseOrderDetail_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PPID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oPurchaseOrderDetail = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oPurchaseOrderDetail;
        }
        public PurchaseOrderDetails Gets()
        {
            PurchaseOrderDetails oPurchaseOrderDetails = new PurchaseOrderDetails();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_PurchaseOrderDetail_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oPurchaseOrderDetails = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oPurchaseOrderDetails;
        }
        public PurchaseOrderDetails GetsByID(string nID,string nlotNumber)
        {
            PurchaseOrderDetails oPurchaseOrderDetails = new PurchaseOrderDetails();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_PurchaseOrderDetail_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PONumber", SqlDbType.NVarChar)).Value = nID;
                cmd.Parameters.Add(new SqlParameter("@lotNumber", SqlDbType.NVarChar)).Value = nlotNumber;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oPurchaseOrderDetails = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oPurchaseOrderDetails;
        }
        public PurchaseOrderDetails GetsByString(string sString)
        {
            PurchaseOrderDetails oPurchaseOrderDetails = new PurchaseOrderDetails();
            try
            {
                SqlCommand cmd = new SqlCommand(sString, _conn);

                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add(new SqlParameter("@PPID", SqlDbType.VarChar)).Value = sString;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oPurchaseOrderDetails = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oPurchaseOrderDetails;
        }
        public DataTable GetsbyDT(string sSQL)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(sSQL, _conn);

                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add(new SqlParameter("@PPID", SqlDbType.VarChar)).Value = sSQL;
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
        public string insertPurchaseXml(XmlDocument _XmlBills)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Insert_PurchaseDetail", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InsertPurchase", SqlDbType.Xml));
                cmd.Parameters[0].Value = _XmlBills.InnerXml.Substring(21);
                cmd.Connection.Open();
                string Result =Convert.ToString(cmd.ExecuteScalar());
                cmd.Dispose();
                _conn.Close();
                return Result;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        public int UpdatePOD(XmlDocument _XmlBills)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateReturnWastage_Xml", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UpdateReturnWastage", SqlDbType.Xml));
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
