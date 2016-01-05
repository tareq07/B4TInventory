
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
    #region BankAccount
    public class BankAccount : BusinessObject
    {
        #region Constructor
        public BankAccount() { }

        #endregion

        #region Properties

        #region bankName
        public string bankName
        {get; set;}
        #endregion

        #region branchName
        public string branchName
        { get; set; }
        #endregion
        
        #region accountNumber
        public string accountNumber
        { get; set; }
        #endregion

        #region GLhead
        public string GLhead
        { get; set; }
        #endregion

        #region bankID
        public int bankID
        { get; set; }
        #endregion

        #region currentBalance
        public double currentBalance
        { get; set; }
        #endregion

        #region AccWithBrn
        public string _AccWithBrn = "";
        public string AccWithBrn
        { get { return _AccWithBrn = this.accountNumber + " (" + this.branchName + ")"; } }
        #endregion

        //#region DateTime
        //public DateTime Datetime
        //{ get; set; }
        //#endregion

        //#region bool
        //public bool bbool
        //{ get; set; }
        //#endregion

        #region Enum
        public EnumAccountType EnumAccountType
        { get; set; }
        #endregion       

        #endregion

        #region Functions
        public BankAccount Get(int nBankAccountID)
        {
            return BankAccount.Service.Get(nBankAccountID);
        }
        public ID Save()
        {
            return BankAccount.Service.Save(this);
        }
        public bool Delete(int nID)
        {
            return BankAccount.Service.Delete(nID);
        }
        public bool UpDateBankAccount(BankAccount oBankAccount, int nEditID)
        {
            return BankAccount.Service.UpDateBankAccount(oBankAccount, nEditID);
        }
        #endregion

        #region Service Factory
        internal static IBankAccountService Service
        {
            get { return (IBankAccountService)Services.Factory.CreateService(typeof(IBankAccountService)); }
        }
        #endregion
    }
    #endregion

    #region BankAccounts
    public class BankAccounts : IndexedBusinessObjects
    {
        #region Collection Class Methods
        public void Add(BankAccount oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(BankAccount oItem)
        {
            base.RemoveItem(oItem);
        }
        public BankAccount this[int index]
        {
            get { return (BankAccount)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }
        #endregion

        #region Functions
        public static BankAccounts Gets()
        {
            return BankAccount.Service.Gets();
        }
        public static BankAccounts Gets(int nID)
        {
            return BankAccount.Service.Gets(nID);
        }
        public static BankAccounts GetsAccounts(int nBrnID)
        {
            return BankAccount.Service.GetsAccounts(nBrnID);
        }
        //public static DataTable GetsbyDT()
        //{
        //    return Grading.Service.GetsbyDT();
        //}
        #endregion
    }
    #endregion

    #region IBankAccount interface
    public interface IBankAccountService
    {
        BankAccount Get(int nBankAccountID);
        BankAccounts Gets();
        BankAccounts Gets(int nID);
        BankAccounts GetsAccounts(int nBrnID);
        bool Delete(int oID);
        ID Save(BankAccount oBankAccount);
        bool UpDateBankAccount(BankAccount oBankAccount, int nEditID);
        //DataTable GetsbyDT();
    }
    #endregion
}

