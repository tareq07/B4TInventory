using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EMS.Core.Framework;
using EMS.Core;
using EMS.Core.Utility;
using System.Data;


namespace EMS.BusinessObjects
{
    #region Transaction
    public class Transaction : BusinessObject
    {
        #region Constructor
        public Transaction() { }
        #endregion

        #region Properties

        public string TranRef { get; set; }

        public string LegacyRef { get; set; }

        public string DebitAcc { get; set; }

        public string CreditAcc { get; set; }

        public double DrAmount { get; set; }

        public double CrAmount { get; set; }

        public DateTime TranDate { get; set; }

        public string Perticular { get; set; }

        public string TranHead { get; set; }

        public double CurrBalance { get; set; }

        #endregion

        #region Functions
        public Transaction GetByID(int nID)
        {
            return Transaction.Service.GetByID(nID);
        }
        public ID Save()
        {
            return Transaction.Service.Save(this);
        }
        public string InsertTransaction(Transaction oTransaction)
        {
            return Transaction.Service.InsertTransaction(oTransaction);

        }
        public bool Delete(int nID)
        {
            return Transaction.Service.Delete(nID);
        }
        public bool ReverseTransaction(string sTranReff)
        {
            return Transaction.Service.ReverseTransaction(sTranReff);
        }
        public bool UpDateTransaction(Transaction oTransaction, int nEditID)
        {
            return Transaction.Service.UpDateTransaction(oTransaction, nEditID);
        }
        public string GetTrnRef()
        {
            return Transaction.Service.GetTrnRef();
        }
        #endregion

        #region Service Factory
        internal static ITransactionService Service
        {
            get { return (ITransactionService)Services.Factory.CreateService(typeof(ITransactionService)); }
        }

        #endregion
    }

    #endregion

    #region Transactions
    public class Transactions : IndexedBusinessObjects
    {

        #region Collection Class Methods
        public void Add(Transaction oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(Transaction oItem)
        {
            base.RemoveItem(oItem);
        }
        public Transaction this[int index]
        {
            get { return (Transaction)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }

        #endregion

        #region Collection Class Functions
        public static Transactions Gets()
        {
            return Transaction.Service.Gets();
        }
        public static Transactions GetsByID(int nID)
        {
            return Transaction.Service.GetsByID(nID);
        }
        public static Transactions GetsByString(string sString)
        {
            return Transaction.Service.GetsByString(sString);
        }
        public static DataTable GetsbyDT(string sDateFrom, string sDateTo)
        {
            return Transaction.Service.GetsbyDT(sDateFrom, sDateTo);
        }
        public static DataTable GetsbyDT2(string sAccountNo, string sDateFrom, string sDateTo)
        {
            return Transaction.Service.GetsbyDT2(sAccountNo,sDateFrom, sDateTo);
        }
        #endregion
    }

    #endregion

    #region ITransaction interface
    public interface ITransactionService
    {
        Transaction GetByID(int nID);
        Transactions Gets();
        Transactions GetsByID(int nID);
        Transactions GetsByString(string sString);
        bool Delete(int oID);
        bool UpDateTransaction(Transaction oTransaction, int nEditID);
        ID Save(Transaction oTransaction);
        DataTable GetsbyDT(string sDateFrom, string sDateTo);
        DataTable GetsbyDT2(string sAccountNo, string sDateFrom, string sDateTo);
        string GetTrnRef();
        bool ReverseTransaction(string sTranReff);
        string InsertTransaction(Transaction oTransaction);
    }

    #endregion
}
