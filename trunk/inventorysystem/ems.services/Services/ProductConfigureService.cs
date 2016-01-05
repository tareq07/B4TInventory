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
    public class ProductConfigureService : MarshalByRefObject, IProductConfigureService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(ProductConfigure oProductConfigure, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oProductConfigure, new ID(oReader.GetInt32("PCID")));

            oProductConfigure.prodId = oReader.GetInt32("prodId");

            oProductConfigure.Pro_Name = oReader.GetString("Pro_Name");

            oProductConfigure.unitQty = oReader.GetDouble("unitQty");

            oProductConfigure.unitType = (EnumUnitType)oReader.GetInt32("unitType");

            BusinessObject.Factory.SetObjectState(oProductConfigure, ObjectState.Saved);
        }
        private ProductConfigure CreateObject(NullHandler oReader)
        {
            ProductConfigure oProductConfigure = new ProductConfigure();
            MapObject(oProductConfigure, oReader);
            return oProductConfigure;
        }
        private ProductConfigures CreateObjects(IDataReader oReader)
        {
            ProductConfigures oProductConfigures = new ProductConfigures();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ProductConfigure oItem = CreateObject(oHandler);
                oProductConfigures.Add(oItem);
            }
            return oProductConfigures;
        }
        #endregion

        #region Interface implementation
        public ProductConfigureService() { }
        public ID Save(ProductConfigure oProductConfigure)
        {
            try
            {
                if (oProductConfigure.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_ProductConfigure_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@prodId", SqlDbType.Int)).Value = oProductConfigure.prodId;
                    cmd.Parameters.Add(new SqlParameter("@unitQty", SqlDbType.Decimal)).Value = oProductConfigure.unitQty;
                    cmd.Parameters.Add(new SqlParameter("@unitType", SqlDbType.Int)).Value = (int)oProductConfigure.unitType;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    int NewID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oProductConfigure, new ID(NewID));
                }
                BusinessObject.Factory.SetObjectState(oProductConfigure, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProductConfigure.ID;
        }
        public bool UpDateProductConfigure(ProductConfigure oProductConfigure, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProductConfigure_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@prodId", SqlDbType.Int)).Value = oProductConfigure.prodId;
                cmd.Parameters.Add(new SqlParameter("@unitQty", SqlDbType.Decimal)).Value = oProductConfigure.unitQty;
                cmd.Parameters.Add(new SqlParameter("@unitType", SqlDbType.Int)).Value = (int)oProductConfigure.unitType;
                cmd.Parameters.Add(new SqlParameter("@DBUUID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@PCID", SqlDbType.Int)).Value = nEditID;
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
                SqlCommand cmd = new SqlCommand("SP_ProductConfigure_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PCID", SqlDbType.Int)).Value = nID;
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
        public ProductConfigure GetByID(int nID)
        {
            ProductConfigure oProductConfigure = new ProductConfigure();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProductConfigure_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PCID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oProductConfigure = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProductConfigure;
        }
        public ProductConfigures Gets()
        {
            ProductConfigures oProductConfigures = new ProductConfigures();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProductConfigure_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProductConfigures = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProductConfigures;
        }
        public ProductConfigures GetsByID(int nID)
        {
            ProductConfigures oProductConfigures = new ProductConfigures();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProductConfigure_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PCID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProductConfigures = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProductConfigures;
        }
        public ProductConfigures GetsByString(string sString)
        {
            ProductConfigures oProductConfigures = new ProductConfigures();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProductConfigure_GetsByString", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PCID", SqlDbType.VarChar)).Value = sString;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProductConfigures = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProductConfigures;
        }
        public DataTable GetsbyDT(string sSQL)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProductConfigure_GetsbyDT", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PCID", SqlDbType.VarChar)).Value = sSQL;
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
