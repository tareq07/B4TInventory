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
    #region Challan
    public class Challan : BusinessObject
    {
        #region Constructor
        public Challan() { }
        #endregion

        #region Properties

        public string ChallanType { get; set; }

        public string ChallanNo { get; set; }

        public string Description { get; set; }

        #endregion

        #region Functions
        public Challan GetByID(int nID)
        {
            return Challan.Service.GetByID(nID);
        }
        public ID Save()
        {
            return Challan.Service.Save(this);
        }
        public bool Delete(int nID)
        {
            return Challan.Service.Delete(nID);
        }
        public bool UpDateChallan(Challan oChallan, int nEditID)
        {
            return Challan.Service.UpDateChallan(oChallan, nEditID);
        }

        #endregion

        #region Service Factory
        internal static IChallanService Service
        {
            get { return (IChallanService)Services.Factory.CreateService(typeof(IChallanService)); }
        }

        #endregion
    }

    #endregion

    #region Challans
    public class Challans : IndexedBusinessObjects
    {

        #region Collection Class Methods
        public void Add(Challan oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(Challan oItem)
        {
            base.RemoveItem(oItem);
        }
        public Challan this[int index]
        {
            get { return (Challan)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }

        #endregion

        #region Collection Class Functions
        public static Challans Gets()
        {
            return Challan.Service.Gets();
        }
        public static Challans GetsByID(int nID)
        {
            return Challan.Service.GetsByID(nID);
        }
        public static Challans GetsByString(string sString)
        {
            return Challan.Service.GetsByString(sString);
        }
        public static DataTable GetsbyDT(string sSQL)
        {
            return Challan.Service.GetsbyDT(sSQL);
        }

        #endregion
    }

    #endregion

    #region IChallan interface
    public interface IChallanService
    {
        Challan GetByID(int nID);
        Challans Gets();
        Challans GetsByID(int nID);
        Challans GetsByString(string sString);
        bool Delete(int oID);
        bool UpDateChallan(Challan oChallan, int nEditID);
        ID Save(Challan oChallan);
        DataTable GetsbyDT(string sSQL);
    }

    #endregion
}
