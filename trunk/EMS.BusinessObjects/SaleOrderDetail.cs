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
    #region SaleOrderDetail
    public class SaleOrderDetail : BusinessObject
    {
        #region Constructor
        public SaleOrderDetail() { }
        #endregion

        #region Properties

        public string SONumber { get; set; }

        public DateTime saleDate { get; set; }

        public string PONumber { get; set; }

        public string printBillRef { get; set; }

        public string memoNo { get; set; }

        public int Pro_ID { get; set; }

        public double discount { get; set; }

        public string newCus { get; set; }

        public string newCusContact { get; set; }

        public string PartyID { get; set; }

        public string prodUnit { get; set; }

        public double bagQty { get; set; }

        public double unitPrice { get; set; }

        public double unitPerBag { get; set; }

        public double transportCost { get; set; }

        public double returnQty { get; set; }

        public double totalSaleQty { get; set; }

        public double totalAmount { get; set; }

        public string comments { get; set; }

        #endregion

        #region Functions
        public SaleOrderDetail GetByID(int nID)
        {
            return SaleOrderDetail.Service.GetByID(nID);
        }
        public ID Save()
        {
            return SaleOrderDetail.Service.Save(this);
        }
        public bool Delete(int nID)
        {
            return SaleOrderDetail.Service.Delete(nID);
        }
        public bool UpDateSaleOrderDetail(SaleOrderDetail oSaleOrderDetail, int nEditID)
        {
            return SaleOrderDetail.Service.UpDateSaleOrderDetail(oSaleOrderDetail, nEditID);
        }
        public string InsertWholeSaleXml(XmlDocument _XmlBills)
        {
            return SaleOrderDetail.Service.InsertWholeSaleXml(_XmlBills);
        }
        public int UpdateSOD(XmlDocument _XmlBills)
        {
            return SaleOrderDetail.Service.UpdateSOD(_XmlBills);
        }
        #endregion

        #region Service Factory
        internal static ISaleOrderDetailService Service
        {
            get { return (ISaleOrderDetailService)Services.Factory.CreateService(typeof(ISaleOrderDetailService)); }
        }

        #endregion


        
    }

    #endregion

    #region SaleOrderDetails
    public class SaleOrderDetails : IndexedBusinessObjects
    {

        #region Collection Class Methods
        public void Add(SaleOrderDetail oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(SaleOrderDetail oItem)
        {
            base.RemoveItem(oItem);
        }
        public SaleOrderDetail this[int index]
        {
            get { return (SaleOrderDetail)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }

        #endregion

        #region Collection Class Functions
        public static SaleOrderDetails Gets()
        {
            return SaleOrderDetail.Service.Gets();
        }
        public static SaleOrderDetails GetsByID(int nID)
        {
            return SaleOrderDetail.Service.GetsByID(nID);
        }
        public static SaleOrderDetails GetsByString(string sString)
        {
            return SaleOrderDetail.Service.GetsByString(sString);
        }
        public static DataTable GetsbyDT(string sSQL)
        {
            return SaleOrderDetail.Service.GetsbyDT(sSQL);
        }

        #endregion
    }

    #endregion

    #region ISaleOrderDetail interface
    public interface ISaleOrderDetailService
    {
        SaleOrderDetail GetByID(int nID);
        SaleOrderDetails Gets();
        SaleOrderDetails GetsByID(int nID);
        SaleOrderDetails GetsByString(string sString);
        bool Delete(int oID);
        bool UpDateSaleOrderDetail(SaleOrderDetail oSaleOrderDetail, int nEditID);
        ID Save(SaleOrderDetail oSaleOrderDetail);
        DataTable GetsbyDT(string sSQL);
        string InsertWholeSaleXml(XmlDocument _XmlBills);
        int UpdateSOD(XmlDocument _XmlBills);
    }

    #endregion
}
