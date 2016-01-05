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
    #region ProdCatagory
    public class ProdCatagory : BusinessObject
    {
        #region Constructor
        public ProdCatagory() { }
        #endregion

        #region Properties

        public string catName { get; set; }

        #endregion

        #region Functions
        public ProdCatagory GetByID(int nID)
        {
            return ProdCatagory.Service.GetByID(nID);
        }
        public ID Save()
        {
            return ProdCatagory.Service.Save(this);
        }
        public bool Delete(int nID)
        {
            return ProdCatagory.Service.Delete(nID);
        }
        public bool UpDateProdCatagory(ProdCatagory oProdCatagory, int nEditID)
        {
            return ProdCatagory.Service.UpDateProdCatagory(oProdCatagory, nEditID);
        }

        #endregion

        #region Service Factory
        internal static IProdCatagoryService Service
        {
            get { return (IProdCatagoryService)Services.Factory.CreateService(typeof(IProdCatagoryService)); }
        }

        #endregion
    }

    #endregion

    #region ProdCatagorys
    public class ProdCatagorys : IndexedBusinessObjects
    {

        #region Collection Class Methods
        public void Add(ProdCatagory oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(ProdCatagory oItem)
        {
            base.RemoveItem(oItem);
        }
        public ProdCatagory this[int index]
        {
            get { return (ProdCatagory)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }

        #endregion

        #region Collection Class Functions
        public static ProdCatagorys Gets()
        {
            return ProdCatagory.Service.Gets();
        }
        public static ProdCatagorys GetsByID(int nID)
        {
            return ProdCatagory.Service.GetsByID(nID);
        }
        public static ProdCatagorys GetsByString(string sString)
        {
            return ProdCatagory.Service.GetsByString(sString);
        }
        public static DataTable GetsbyDT(string sSQL)
        {
            return ProdCatagory.Service.GetsbyDT(sSQL);
        }

        #endregion
    }

    #endregion

    #region IProdCatagory interface
    public interface IProdCatagoryService
    {
        ProdCatagory GetByID(int nID);
        ProdCatagorys Gets();
        ProdCatagorys GetsByID(int nID);
        ProdCatagorys GetsByString(string sString);
        bool Delete(int oID);
        bool UpDateProdCatagory(ProdCatagory oProdCatagory, int nEditID);
        ID Save(ProdCatagory oProdCatagory);
        DataTable GetsbyDT(string sSQL);
    }

    #endregion
}
