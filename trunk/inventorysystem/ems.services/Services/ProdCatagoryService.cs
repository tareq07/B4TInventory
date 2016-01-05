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
    public class ProdCatagoryService : MarshalByRefObject, IProdCatagoryService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion

        #region Private functions and declaration
        private void MapObject(ProdCatagory oProdCatagory, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oProdCatagory, new ID(oReader.GetInt32("catId")));

            oProdCatagory.catName = oReader.GetString("catName");

            BusinessObject.Factory.SetObjectState(oProdCatagory, ObjectState.Saved);
        }
        private ProdCatagory CreateObject(NullHandler oReader)
        {
            ProdCatagory oProdCatagory = new ProdCatagory();
            MapObject(oProdCatagory, oReader);
            return oProdCatagory;
        }
        private ProdCatagorys CreateObjects(IDataReader oReader)
        {
            ProdCatagorys oProdCatagorys = new ProdCatagorys();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ProdCatagory oItem = CreateObject(oHandler);
                oProdCatagorys.Add(oItem);
            }
            return oProdCatagorys;
        }
        #endregion

        #region Interface implementation
        public ProdCatagoryService() { }
        public ID Save(ProdCatagory oProdCatagory)
        {
            try
            {
                if (oProdCatagory.IsNew)
                {
                    SqlCommand cmd = new SqlCommand("SP_ProdCatagory_Insert", _conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@catName", SqlDbType.VarChar)).Value = oProdCatagory.catName;
                    cmd.Parameters.Add(new SqlParameter("@DBUserID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                    if (_conn.State == ConnectionState.Open) { }
                    else { cmd.Connection.Open(); }
                    int NewID = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                    cmd.Connection.Close();
                    BusinessObject.Factory.SetID(oProdCatagory, new ID(NewID));
                }
                BusinessObject.Factory.SetObjectState(oProdCatagory, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProdCatagory.ID;
        }
        public bool UpDateProdCatagory(ProdCatagory oProdCatagory, int nEditID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProdCatagory_Update", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@catName", SqlDbType.VarChar)).Value = oProdCatagory.catName;
                cmd.Parameters.Add(new SqlParameter("@DBUUID", SqlDbType.Int)).Value = EMSGlobal._nCurrentUserID;
                cmd.Parameters.Add(new SqlParameter("@catId", SqlDbType.Int)).Value = nEditID;
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
                SqlCommand cmd = new SqlCommand("SP_ProdCatagory_Delete", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@catId", SqlDbType.Int)).Value = nID;
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
        public ProdCatagory GetByID(int nID)
        {
            ProdCatagory oProdCatagory = new ProdCatagory();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProdCatagory_GetByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@catId", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oProdCatagory = CreateObject(oReader);
                }
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProdCatagory;
        }
        public ProdCatagorys Gets()
        {
            ProdCatagorys oProdCatagorys = new ProdCatagorys();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProdCatagory_Gets", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProdCatagorys = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProdCatagorys;
        }
        public ProdCatagorys GetsByID(int nID)
        {
            ProdCatagorys oProdCatagorys = new ProdCatagorys();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProdCatagory_GetsByID", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@catId", SqlDbType.Int)).Value = nID;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProdCatagorys = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProdCatagorys;
        }
        public ProdCatagorys GetsByString(string sString)
        {
            ProdCatagorys oProdCatagorys = new ProdCatagorys();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProdCatagory_GetsByString", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@catId", SqlDbType.VarChar)).Value = sString;
                if (_conn.State == ConnectionState.Open) { }
                else { cmd.Connection.Open(); }
                IDataReader reader = cmd.ExecuteReader();

                oProdCatagorys = CreateObjects(reader);
                reader.Close();
                cmd.Dispose();
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
            return oProdCatagorys;
        }
        public DataTable GetsbyDT(string sSQL)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_ProdCatagory_GetsbyDT", _conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@catId", SqlDbType.VarChar)).Value = sSQL;
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
