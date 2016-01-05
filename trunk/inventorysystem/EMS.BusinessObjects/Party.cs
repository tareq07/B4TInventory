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
    #region Party
    public class Party : BusinessObject
    {
        #region Constructor
        public Party() { }
        #endregion

        #region Properties

        public EnumParty PartyType { get; set; }

        public string AccountCode { get; set; }

        public string PartyName { get; set; }

        public string Address { get; set; }

        public string Mobile { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public string PartyWithCode
        {
            get { return "(" + this.AccountCode + ") " + this.PartyName; }
        }

        #endregion

        #region Functions
        public Party GetByID(int nID)
        {
            return Party.Service.GetByID(nID);
        }
        public ID Save()
        {
            return Party.Service.Save(this);
        }
        public bool Delete(string sAcCode)
        {
            return Party.Service.Delete(sAcCode);
        }
        public bool UpDateParty(Party oParty, int nEditID)
        {
            return Party.Service.UpDateParty(oParty, nEditID);
        }

        #endregion

        #region Service Factory
        internal static IPartyService Service
        {
            get { return (IPartyService)Services.Factory.CreateService(typeof(IPartyService)); }
        }

        #endregion
    }

    #endregion

    #region Partys
    public class Partys : IndexedBusinessObjects
    {

        #region Collection Class Methods
        public void Add(Party oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(Party oItem)
        {
            base.RemoveItem(oItem);
        }
        public Party this[int index]
        {
            get { return (Party)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }

        #endregion

        #region Collection Class Functions
        public static Partys Gets()
        {
            return Party.Service.Gets();
        }
        public static Partys GetsByID(int nID)
        {
            return Party.Service.GetsByID(nID);
        }
        public static Partys GetsByString(string sString)
        {
            return Party.Service.GetsByString(sString);
        }
        public static DataTable GetsbyDT(string sSQL)
        {
            return Party.Service.GetsbyDT(sSQL);
        }

        #endregion
    }

    #endregion

    #region IParty interface
    public interface IPartyService
    {
        Party GetByID(int nID);
        Partys Gets();
        Partys GetsByID(int nID);
        Partys GetsByString(string sString);
        bool Delete(string sAcCode);
        bool UpDateParty(Party oParty, int nEditID);
        ID Save(Party oParty);
        DataTable GetsbyDT(string sSQL);
    }

    #endregion
}
