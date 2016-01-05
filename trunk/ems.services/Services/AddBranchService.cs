
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using EMS.Core.Framework;
using EMS.Core.DataAccess;
using EMS.Core;
using EMS.Core.Utility;

using EMS.BusinessObjects;

using System.Configuration;
using System.Data.SqlClient;

namespace EMS.Services.Services
{
    
    [Serializable]
    public class AddBranchService : MarshalByRefObject, IAddBranchService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion
        #region Private functions and declaration
        
        private void MapObject(AddBranch oAddBranch, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oAddBranch, new ID(oReader.GetInt32("brn_id")));

            oAddBranch.ebrn_type = (EnumBrnchType)oReader.GetInt32("brn_type");
            oAddBranch.brn_title = oReader.GetString("brn_title");
            oAddBranch.brn_location = oReader.GetString("brn_location");

            BusinessObject.Factory.SetObjectState(oAddBranch, ObjectState.Saved);
        }

        private AddBranch CreateObject(NullHandler oReader)
        {
            AddBranch oAddBranch = new AddBranch();
            MapObject(oAddBranch, oReader);
            return oAddBranch;
        }

        private AddBranchs CreateObjects(IDataReader oReader)
        {
            AddBranchs oAddBranchs = new AddBranchs();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                AddBranch oItem = CreateObject(oHandler);
                oAddBranchs.Add(oItem);
            }
            return oAddBranchs;
        }
        #endregion

        #region Interface implementation
        public AddBranchService() { }

        public ID Save(AddBranch oAddBranch)
        {           
            
            bool _isBranchExist = false;
            try
            {
                
                
                if (oAddBranch.IsNew)
                {
                    string QueryString = "SELECT * FROM tbl_Branchs WHERE brn_title='" + oAddBranch.brn_title + "'";
                    _isBranchExist = ExecuteQueryFunctions.ExeIsExist(QueryString);
                    if (!_isBranchExist)
                    {
                        BusinessObject.Factory.SetID(oAddBranch, new ID(ExecuteQueryFunctions.GetNewID(_conn, "SELECT MAX(brn_id) FROM tbl_Branchs")));
                        string QueryString2 = "INSERT INTO tbl_Branchs (brn_id,brn_type,brn_title,brn_location,DBUserID,DBSDT)"
                                            + "VALUES(" +
                                            oAddBranch.ObjectID + "," +
                                            (int)oAddBranch.ebrn_type + ",'" +
                                            oAddBranch.brn_title + "','" +
                                            oAddBranch.brn_location + "'," +
                                            EMSGlobal._nCurrentUserID + ",'" +
                                            DateTime.Now + "')";
                        ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString2);
                    }
                    else
                    {
                        throw new ServiceException("This Title Already Exist.");
                    }
                }               
                
                BusinessObject.Factory.SetObjectState(oAddBranch, ObjectState.Saved);
            }
            catch (Exception e)
            {               
                throw new ServiceException(e.Message, e);                
            }
            return oAddBranch.ID;
        }       
        public bool Delete(int nBrnID)
        {            
            try
            {
                string QueryString = "DELETE FROM tbl_Branchs WHERE brn_id=" + nBrnID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);               
            }
        }
        public bool UpdateBranch(AddBranch oAddBranch, int nBrnID)
        {
            try
            {
                string QueryString = "UPDATE tbl_Branchs SET " +
                                    "brn_type=" + (int)oAddBranch.ebrn_type + "," +
                                    "brn_title='" + oAddBranch.brn_title + "'," +
                                    "brn_location='" + oAddBranch.brn_location + "'," +
                                    "DBUserID=" + EMSGlobal._nCurrentUserID + "," +
                                    "DBSDT='" + DateTime.Now + "' " +
                                    "WHERE brn_id=" + nBrnID + "";
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
        }
        public AddBranch Get(int nID)
        {
            AddBranch oAddBranch = new AddBranch();            
            try
            {
                string QueryString = "SELECT * FROM tbl_Branchs WHERE brn_id=" + nID;
                
                IDataReader reader = ExecuteQueryFunctions.ExeReader(_conn,QueryString);
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oAddBranch = CreateObject(oReader);
                }
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {                
                throw new ServiceException("Failed to Get AddBranch", e);                
            }

            return oAddBranch;
        }
        
        public AddBranchs Get()
        {
            AddBranchs oAddBranchs = null;
            try
            {
                string QueryString = "SELECT * FROM tbl_Branchs ORDER BY brn_id";
                IDataReader reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                oAddBranchs = CreateObjects(reader);
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {                
                throw new ServiceException("Failed to Get AddBranchs", e);
            }

            return oAddBranchs;
        }
        public AddBranchs GetsByType(int nbrn_type)
        {
            AddBranchs oAddBranchs = null;
            try
            {
                string QueryString = "SELECT * FROM tbl_Branchs WHERE brn_type=" + nbrn_type;
                IDataReader reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                oAddBranchs = CreateObjects(reader);
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException("Failed to Get AddBranchs", e);
            }
            return oAddBranchs;
        }
        public AddBranchs Gets(int nID)
        {
            AddBranchs oAddBranchs = null;
            try
            {
                string QueryString = "SELECT * FROM tbl_Branchs WHERE brn_id IN (SELECT brn_id FROM tbl_TeacherAsign WHERE tch_id=" + nID + ")";
                IDataReader reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                oAddBranchs = CreateObjects(reader);
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {               
                throw new ServiceException("Failed to Get AddBranchs", e);
            }
            return oAddBranchs;
        }
        public DataSet GetBrnsbyDS()
        {
            DataSet oDataSet = new DataSet();
            try
            {
                string QueryString = "SELECT * FROM tbl_Branchs ORDER BY brn_id";
                IDataReader reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                oDataSet.Load(reader, LoadOption.OverwriteChanges, new string[1]);
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
            return oDataSet;
        }
        #endregion
    }
}


