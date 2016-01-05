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
    #region PurchaseOrderDetail
    public class PurchaseOrderDetail : BusinessObject
    {
        #region Constructor
        public PurchaseOrderDetail() { }
        #endregion

        #region Properties

        public string lotNumber { get; set; }

        public string PartyID { get; set; }

        public int Pro_ID { get; set; }

        public string ProdName { get; set; }

        public string prodUnit { get; set; }

        public string PONumber { get; set; }

        public string memoNo { get; set; }

        public double bagQty { get; set; }

        public double unitPrice { get; set; }

        public double unitPerBag { get; set; }

        public double totalQty { get; set; }

        public double saleQtyBag { get; set; }

        public double saleQty { get; set; }

        public double returnQty { get; set; }

        public double wastageQty { get; set; }

        public double truckCost { get; set; }

        public double labourCost { get; set; }

        public double bagCost { get; set; }

        public double otherCost { get; set; }

        public string comments { get; set; }

        public double availQty { get { return this.totalQty - (this.saleQty + this.returnQty + this.wastageQty); } }

        public double purchasePrice { get; set; }
        #endregion

        #region Functions
        public PurchaseOrderDetail GetByID(int nID)
        {
            return PurchaseOrderDetail.Service.GetByID(nID);
        }
        public ID Save()
        {
            return PurchaseOrderDetail.Service.Save(this);
        }
        public bool Delete(int nID)
        {
            return PurchaseOrderDetail.Service.Delete(nID);
        }
        public bool UpDatePurchaseOrderDetail(PurchaseOrderDetail oPurchaseOrderDetail, int nEditID)
        {
            return PurchaseOrderDetail.Service.UpDatePurchaseOrderDetail(oPurchaseOrderDetail, nEditID);
        }
        public string insertPurchaseXml(XmlDocument _XmlBills)
        {
            return PurchaseOrderDetail.Service.insertPurchaseXml(_XmlBills);
        }

        public int UpdatePOD(XmlDocument _XmlBills)
        {
            return PurchaseOrderDetail.Service.UpdatePOD(_XmlBills);
        }
        #endregion

        #region Service Factory
        internal static IPurchaseOrderDetailService Service
        {
            get { return (IPurchaseOrderDetailService)Services.Factory.CreateService(typeof(IPurchaseOrderDetailService)); }
        }

        #endregion

    }

    #endregion

    #region PurchaseOrderDetails
    public class PurchaseOrderDetails : IndexedBusinessObjects
    {

        #region Collection Class Methods
        public void Add(PurchaseOrderDetail oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(PurchaseOrderDetail oItem)
        {
            base.RemoveItem(oItem);
        }
        public PurchaseOrderDetail this[int index]
        {
            get { return (PurchaseOrderDetail)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }

        #endregion

        #region Collection Class Functions
        public static PurchaseOrderDetails Gets()
        {
            return PurchaseOrderDetail.Service.Gets();
        }
        public static PurchaseOrderDetails GetsByID(string nID,string nlotNumber)
        {
            return PurchaseOrderDetail.Service.GetsByID(nID, nlotNumber);
        }
        public static PurchaseOrderDetails GetsByString(string sString)
        {
            return PurchaseOrderDetail.Service.GetsByString(sString);
        }
        public static DataTable GetsbyDT(string sSQL)
        {
            return PurchaseOrderDetail.Service.GetsbyDT(sSQL);
        }

        #endregion
    }

    #endregion

    #region IPurchaseOrderDetail interface
    public interface IPurchaseOrderDetailService
    {
        PurchaseOrderDetail GetByID(int nID);
        PurchaseOrderDetails Gets();
        PurchaseOrderDetails GetsByID(string nID, string nlotNumber);
        PurchaseOrderDetails GetsByString(string sString);
        bool Delete(int oID);
        bool UpDatePurchaseOrderDetail(PurchaseOrderDetail oPurchaseOrderDetail, int nEditID);
        ID Save(PurchaseOrderDetail oPurchaseOrderDetail);
        DataTable GetsbyDT(string sSQL);

        string insertPurchaseXml(XmlDocument _XmlBills);

        int UpdatePOD(XmlDocument _XmlBills);
    }

    #endregion
}
