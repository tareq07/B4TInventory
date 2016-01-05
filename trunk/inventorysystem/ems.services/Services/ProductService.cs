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
    public class ProductService : MarshalByRefObject, IProductService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(Product oProduct, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oProduct, new ID(oReader.GetInt32("Pro_ID")));

            oProduct.Pro_Name = oReader.GetString("Pro_Name");

            oProduct.Pro_Cat_ID = oReader.GetInt32("Pro_Cat_ID");

            oProduct.catName = oReader.GetString("catName");

            //oProduct.modelNumber = oReader.GetString("modelNumber");



            BusinessObject.Factory.SetObjectState(oProduct, ObjectState.Saved);
        }
        private Product CreateObject(NullHandler oReader)
        {
            Product oProduct = new Product();
            MapObject(oProduct, oReader);
            return oProduct;
        }
        private Products CreateObjects(IDataReader oReader)
        {
            Products oProducts = new Products();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Product oItem = CreateObject(oHandler);
                oProducts.Add(oItem);
            }
            return oProducts;
        }
        #endregion

        #region Interface implementation
        public ProductService() { }
        public ID Save(Product oProduct)
        {
            try
            {
                if (oProduct.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_Product_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Pro_Name", SqlDbType.VarChar)).Value = oProduct.Pro_Name;
                    cmd.Parameters.Add(new SqlParameter("@Pro_Cat_ID", SqlDbType.Int)).Value = oProduct.Pro_Cat_ID;
                    cmd.Parameters.Add(new SqlParameter("@modelNumber", SqlDbType.VarChar)).Value = string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    int NewID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oProduct, new ID(NewID));
                }
                BusinessObject.Factory.SetObjectState(oProduct, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProduct.ID;
        }
        public bool UpDateProduct(Product oProduct, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Product_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Pro_Name", SqlDbType.VarChar)).Value = oProduct.Pro_Name;
                cmd.Parameters.Add(new SqlParameter("@Pro_Cat_ID", SqlDbType.Int)).Value = oProduct.Pro_Cat_ID;
                cmd.Parameters.Add(new SqlParameter("@modelNumber", SqlDbType.VarChar)).Value = string.Empty;
                cmd.Parameters.Add(new SqlParameter("@DBUUID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.Int)).Value = nEditID;
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
                SqlCommand cmd = new SqlCommand("SP_Product_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.Int)).Value = nID;
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
        public Product GetByID(int nID)
        {
            Product oProduct = new Product();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Product_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oProduct = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProduct;
        }
        public Products Gets()
        {
            Products oProducts = new Products();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Product_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProducts = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProducts;
        }
        public Products GetsByID(int nID)
        {
            Products oProducts = new Products();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Product_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProducts = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProducts;
        }
        public Products GetsByString(string sString)
        {
            Products oProducts = new Products();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Product_GetsByString", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.VarChar)).Value = sString;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProducts = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProducts;
        }
        public DataTable GetsbyDT(string sSQL)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(sSQL, _conn);

                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add(new SqlParameter("@Pro_ID", SqlDbType.VarChar)).Value = sSQL;
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
