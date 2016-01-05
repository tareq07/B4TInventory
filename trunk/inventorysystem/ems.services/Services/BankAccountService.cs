
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
    public class BankAccountService : MarshalByRefObject, IBankAccountService
    {
        #region ConnectionString
        SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
        #endregion
                
        #region Private functions and declaration
        private void MapObject(BankAccount oBankAccount, NullHandler oReader)
        {
            BusinessObject.Factory.SetID(oBankAccount, new ID(oReader.GetInt32("ObjectID")));

            oBankAccount.bankName = oReader.GetString("bankName");
            oBankAccount.branchName = oReader.GetString("branchName");
            oBankAccount.accountNumber = oReader.GetString("accountNumber");
            oBankAccount.GLhead = oReader.GetString("GLhead");
            oBankAccount.currentBalance = oReader.GetDouble("currentBalance");
            oBankAccount.EnumAccountType = (EnumAccountType)oReader.GetInt32("accountType");
            oBankAccount.bankID = oReader.GetInt32("bankID");
            

            BusinessObject.Factory.SetObjectState(oBankAccount, ObjectState.Saved);
        }

        private BankAccount CreateObject(NullHandler oReader)
        {
            BankAccount oBankAccount = new BankAccount();
            MapObject(oBankAccount, oReader);
            return oBankAccount;
        }

        private BankAccounts CreateObjects(IDataReader oReader)
        {
            BankAccounts oBankAccounts = new BankAccounts();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                BankAccount oItem = CreateObject(oHandler);
                oBankAccounts.Add(oItem);
            }
            return oBankAccounts;
        }
        #endregion

        #region Interface implementation
        public BankAccountService() { }

        public ID Save(BankAccount oBankAccount)
        {           
            try
            {                             

                if (oBankAccount.IsNew)
                {
                    BusinessObject.Factory.SetID(oBankAccount, new ID(ExecuteQueryFunctions.GetNewID(_conn, "SELECT MAX(ObjectID) FROM [tbl_bankAccount]")));
                    string QueryString = "INSERT INTO [tbl_bankAccount] (ObjectID,bankID,branchName,bankName,accountNumber,accountType,currentBalance,GLhead,DBUserID,DBSDT)"
                                        + " VALUES ("
                                        + oBankAccount.ObjectID + ","
                                        + oBankAccount.bankID + ",'"
                                        + oBankAccount.branchName + "','"
                                        + oBankAccount.bankName + "','"
                                        + oBankAccount.accountNumber + "',"
                                        + (int)oBankAccount.EnumAccountType + ","
                                        + oBankAccount.currentBalance + ",'"
                                        + oBankAccount.GLhead + "'," 
                                        + EMSGlobal._nCurrentUserID + ",'" 
                                        + DateTime.Now + "')";
                    ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
                }
                
                BusinessObject.Factory.SetObjectState(oBankAccount, ObjectState.Saved);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);                
            }
            return oBankAccount.ID;
        }
        public bool UpDateBankAccount(BankAccount oBankAccount, int nEditID)
        {
            try
            {
                string QueryString = "UPDATE [tbl_bankAccount] SET "

                                    + "branchName = '" + oBankAccount.branchName + "',"
                                    + "accountNumber = '" + oBankAccount.accountNumber + "',"
                                    + "accountType = " + (int)oBankAccount.EnumAccountType + ","
                                    + "UpdateBy = " + EMSGlobal._nCurrentUserID + ""
                                    + " WHERE ObjectID=" + nEditID;
                ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
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

                string sRetStr = ExecuteQueryFunctions.ExeRetStr(_conn, "SELECT CONVERT(VARCHAR,ISNULL(currentBalance,0)) FROM [tbl_bankAccount] WHERE ObjectID=" + nID);
                if (Convert.ToDouble(sRetStr) > 0)
                {
                    throw new ServiceException("Delete not posible, It has balance.!");
                }
                else
                {
                    string QueryString = "DELETE FROM [tbl_bankAccount] WHERE ObjectID=" + nID;
                    ExecuteQueryFunctions.ExeNonQuery(_conn, QueryString);
                    return true;
                }
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }
        }        
        public BankAccount Get(int id)
        {
            BankAccount oBankAccount = new BankAccount();
            
            try
            {
                string QueryString = "SELECT * FROM [tbl_bankAccount]";
                IDataReader reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    oBankAccount = CreateObject(oReader);
                }
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }

            return oBankAccount;
        }
        public BankAccounts Gets()
        {
            BankAccounts oBankAccounts = null;
            
            try
            {
                string QueryString = "SELECT * FROM [tbl_bankAccount]";
                IDataReader reader = null;
                reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                oBankAccounts = CreateObjects(reader);
                reader.Close();
                _conn.Close();
                
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }

            return oBankAccounts;
        }
        public BankAccounts Gets(int nID)
        {
            BankAccounts oBankAccounts = null;
            
            try
            {
                string QueryString = "SELECT * FROM [tbl_bankAccount]";
                IDataReader reader = null;
                reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                oBankAccounts = CreateObjects(reader);
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }

            return oBankAccounts;
        }
        public BankAccounts GetsAccounts(int nBrnID)
        {
            BankAccounts oBankAccounts = null;

            try
            {
                string QueryString = "SELECT * FROM [tbl_bankAccount] WHERE bankID=" + nBrnID;
                IDataReader reader = null;
                reader = ExecuteQueryFunctions.ExeReader(_conn, QueryString);
                oBankAccounts = CreateObjects(reader);
                reader.Close();
                _conn.Close();
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message, e);
            }

            return oBankAccounts;
        }
        //public DataTable GetsbyDT()
        //{
        //    DataSet oDataSet = new DataSet();
        //    SqlConnection conn = new SqlConnection(_connectionString); 
        //    try
        //    {
        //        //string QueryString = "SELECT MAX(id) FROM Table";

        //        IDataReader reader = ExecuteQueryFunctions.ExeReader(conn, QueryString);
        //        oDataSet.Load(reader, LoadOption.OverwriteChanges, new string[1]);
        //        reader.Close();
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ServiceException(e.Message);
        //    }
        //    return oDataSet.Tables[0];
        //}
        #endregion
    }
}


