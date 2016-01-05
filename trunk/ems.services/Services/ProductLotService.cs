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
    public class ProductLotService : MarshalByRefObject, IProductLotService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(ProductLot oProductLot, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oProductLot, new ID(oReader.GetInt32("Lot_ID")));

            oProductLot.LotNo = oReader.GetString("LotNo");

            oProductLot.Pro_ID = oReader.GetInt32("Pro_ID");

            oProductLot.barCode = oReader.GetString("barCode");

            oProductLot.Total_Qty = oReader.GetDouble("Total_Qty");

            oProductLot.Bag_Qty = oReader.GetDouble("Bag_Qty");

            oProductLot.Sale_Qty = oReader.GetDouble("Sale_Qty");

            oProductLot.Ret_Qty = oReader.GetDouble("Ret_Qty");

            oProductLot.purchesPrice = oReader.GetDouble("purchesPrice");

            oProductLot.salePrice = oReader.GetDouble("salePrice");

            BusinessObject.Factory.SetObjectState(oProductLot, ObjectState.Saved);
        }
        private ProductLot CreateObject(NullHandler oReader)
        {
            ProductLot oProductLot = new ProductLot();
            MapObject(oProductLot, oReader);
            return oProductLot;
        }
        private ProductLots CreateObjects(IDataReader oReader)
        {
            ProductLots oProductLots = new ProductLots();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ProductLot oItem = CreateObject(oHandler);
                oProductLots.Add(oItem);
            }
            return oProductLots;
        }
        #endregion

        #region Interface implementation
        public ProductLotService() { }
        public ID Save(ProductLot oProductLot)
        {
            try
            {
                if (oProductLot.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_ProductLot_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@LotNo", SqlDbType.VarChar)).Value = oProductLot.LotNo;
                    cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.Int)).Value = oProductLot.Pro_ID;
                    //cmd.Parameters.Add(new SqlParameter("@barCode", SqlDbType.VarChar)).Value = oProductLot.barCode;
                    cmd.Parameters.Add(new SqlParameter("@Total_Qty", SqlDbType.Decimal)).Value = oProductLot.Total_Qty;
                    cmd.Parameters.Add(new SqlParameter("@Bag_Qty", SqlDbType.Decimal)).Value = oProductLot.Bag_Qty;
                    //cmd.Parameters.Add(new SqlParameter("@Sale_Qty", SqlDbType.Decimal)).Value = oProductLot.Sale_Qty;
                    //cmd.Parameters.Add(new SqlParameter("@Ret_Qty", SqlDbType.Decimal)).Value = oProductLot.Ret_Qty;
                    cmd.Parameters.Add(new SqlParameter("@purchesPrice", SqlDbType.Decimal)).Value = oProductLot.purchesPrice;
                    cmd.Parameters.Add(new SqlParameter("@salePrice", SqlDbType.Decimal)).Value = oProductLot.salePrice;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    string NewID = (string)cmd.ExecuteScalar();
                    oProductLot.LotNo = NewID;
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oProductLot, new ID(1));
                }
                BusinessObject.Factory.SetObjectState(oProductLot, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProductLot.ID;
        }
        public bool UpDateProductLot(ProductLot oProductLot, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProductLot_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@LotNo", SqlDbType.VarChar)).Value = oProductLot.LotNo;
                //cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.Int)).Value = oProductLot.Pro_ID;
                //cmd.Parameters.Add(new SqlParameter("@barCode", SqlDbType.VarChar)).Value = oProductLot.barCode;
                cmd.Parameters.Add(new SqlParameter("@Total_Qty", SqlDbType.Decimal)).Value = oProductLot.Total_Qty;
                cmd.Parameters.Add(new SqlParameter("@Bag_Qty", SqlDbType.Decimal)).Value = oProductLot.Bag_Qty;
                cmd.Parameters.Add(new SqlParameter("@Sale_Qty", SqlDbType.Decimal)).Value = oProductLot.Sale_Qty;
                cmd.Parameters.Add(new SqlParameter("@Ret_Qty", SqlDbType.Decimal)).Value = oProductLot.Ret_Qty;
                cmd.Parameters.Add(new SqlParameter("@purchesPrice", SqlDbType.Decimal)).Value = oProductLot.purchesPrice;
                cmd.Parameters.Add(new SqlParameter("@salePrice", SqlDbType.Decimal)).Value = oProductLot.salePrice;
                cmd.Parameters.Add(new SqlParameter("@DBUUID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@Lot_ID", SqlDbType.Int)).Value = nEditID;
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
                SqlCommand cmd = new SqlCommand("SP_ProductLot_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Lot_ID", SqlDbType.Int)).Value = nID;
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
        public ProductLot GetByID(int nID)
        {
            ProductLot oProductLot = new ProductLot();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProductLot_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Lot_ID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oProductLot = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProductLot;
        }
        public ProductLots Gets()
        {
            ProductLots oProductLots = new ProductLots();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProductLot_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProductLots = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProductLots;
        }
        public ProductLots GetsByID(int nID)
        {
            ProductLots oProductLots = new ProductLots();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProductLot_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProductLots = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProductLots;
        }
        public ProductLots GetsByString(string sString)
        {
            ProductLots oProductLots = new ProductLots();
            try
            {
                SqlCommand cmd = new SqlCommand(sString, _conn);

                cmd.CommandType = CommandType.Text;
                
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProductLots = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProductLots;
        }
        public DataTable GetsbyDT(string sSQL)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProductLot_GetsbyDT", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Lot_ID", SqlDbType.VarChar)).Value = sSQL;
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
