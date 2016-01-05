using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using EMS.Core.Framework;
using EMS.Core;
using EMS.Core.Utility;
using System.Data;
using System.Xml;

namespace EMS.BusinessObjects
{
    #region DueCollection
    public class DueCollection : BusinessObject
    {
        #region Constructor
       public DueCollection() { }
        #endregion

        #region Properties

        public string TrnRef { get; set; }

        public string TranRefPrint { get; set; }

        public string LegacyRef { get; set; }

        public EnumParty PartyType { get; set; }

        public string AccountCode { get; set; }

        public string PartyName { get; set; }

        public DateTime TranLastDate { get; set; }

        public double OutStdBalance { get; set; }

        public double ReceivedBalance { get; set; }

        public double TotalBill { get; set; }

        public double paidType { get; set; }

        public double bankAcId { get; set; }

        #endregion

        #region Functions
        public DueCollection GetByID(int nID)
        {
            return DueCollection.Service.GetByID(nID);
        }
        public ID Save()
        {
            return DueCollection.Service.Save(this);
        }
        public bool Delete(int nID)
        {
            return DueCollection.Service.Delete(nID);
        }
        public bool UpDateDueCollection(DueCollection oDueCollection, int nEditID)
        {
            return DueCollection.Service.UpDateDueCollection(oDueCollection, nEditID);
        }
        public bool DueBillCollection(DueCollection oDueCollection)
        {
            return DueCollection.Service.DueBillCollection(oDueCollection);
        }
        public bool ReverseDueBillCollection(string sBillRef)
        {
            return DueCollection.Service.ReverseDueBillCollection(sBillRef);
        }
        public bool InsertFeeCollectinXml(XmlDocument _XmlDoc)
        {
            return DueCollection.Service.InsertFeeCollectinXml(_XmlDoc);
        }
        #endregion

        #region Service Factory
        internal static IDueCollectionService Service
        {
            get { return (IDueCollectionService)Services.Factory.CreateService(typeof(IDueCollectionService)); }
        }

        #endregion
    }
     #endregion

    #region DueCollections
    public class DueCollections : IndexedBusinessObjects
    {

        #region Collection Class Methods
        public void Add(DueCollection oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(DueCollection oItem)
        {
            base.RemoveItem(oItem);
        }
        public DueCollection this[int index]
        {
            get { return (DueCollection)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }

        #endregion

        #region Collection Class Functions
        public static DueCollections Gets()
        {
            return DueCollection.Service.Gets();
        }
        public static DueCollections GetsByID(int nID)
        {
            return DueCollection.Service.GetsByID(nID);
        }
        public static DueCollections GetsByString(string sString)
        {
            return DueCollection.Service.GetsByString(sString);
        }
        public static DataTable GetsbyDT(string sSQL)
        {
            return DueCollection.Service.GetsbyDT(sSQL);
        }
        public static DataTable GetsbyDT2(int nPartyType)
        {
            return DueCollection.Service.GetsbyDT2(nPartyType);
        }
        public static DataTable GetsDueDetails(string sStuRollNo)
        {
            return DueCollection.Service.GetsDueDetails(sStuRollNo);
        }

        #endregion
    }

    #endregion

    #region IDueCollection interface
    public interface IDueCollectionService
    {
        DueCollection GetByID(int nID);
        DueCollections Gets();
        DueCollections GetsByID(int nID);
        DueCollections GetsByString(string sString);
        bool Delete(int oID);
        bool UpDateDueCollection(DueCollection oDueCollection, int nEditID);
        ID Save(DueCollection oDueCollection);
        DataTable GetsbyDT(string sSQL);
        DataTable GetsbyDT2(int nPartyType);
        DataTable GetsDueDetails(string sStuRollNo);
        bool DueBillCollection(DueCollection oDueCollection);
        bool ReverseDueBillCollection(string sBillRef);
        bool InsertFeeCollectinXml(XmlDocument _XmlDoc);
    }

    #endregion
}
