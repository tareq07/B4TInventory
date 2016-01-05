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
    #region Product
    public class Product : BusinessObject
    {
        #region Constructor
        public Product() { }
        #endregion

        #region Properties

        public string Pro_Name { get; set; }

        public string catName { get; set; }

        public int Pro_Cat_ID { get; set; }

        public string modelNumber { get; set; }

        #endregion

        #region Functions
        public Product GetByID(int nID)
        {
            return Product.Service.GetByID(nID);
        }
        public ID Save()
        {
            return Product.Service.Save(this);
        }
        public bool Delete(int nID)
        {
            return Product.Service.Delete(nID);
        }
        public bool UpDateProduct(Product oProduct, int nEditID)
        {
            return Product.Service.UpDateProduct(oProduct, nEditID);
        }

        #endregion

        #region Service Factory
        internal static IProductService Service
        {
            get { return (IProductService)Services.Factory.CreateService(typeof(IProductService)); }
        }

        #endregion
    }

    #endregion

    #region Products
    public class Products : IndexedBusinessObjects
    {

        #region Collection Class Methods
        public void Add(Product oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(Product oItem)
        {
            base.RemoveItem(oItem);
        }
        public Product this[int index]
        {
            get { return (Product)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }

        #endregion

        #region Collection Class Functions
        public static Products Gets()
        {
            return Product.Service.Gets();
        }
        public static Products GetsByID(int nID)
        {
            return Product.Service.GetsByID(nID);
        }
        public static Products GetsByString(string sString)
        {
            return Product.Service.GetsByString(sString);
        }
        public static DataTable GetsbyDT(string sSQL)
        {
            return Product.Service.GetsbyDT(sSQL);
        }

        #endregion
    }

    #endregion

    #region IProduct interface
    public interface IProductService
    {
        Product GetByID(int nID);
        Products Gets();
        Products GetsByID(int nID);
        Products GetsByString(string sString);
        bool Delete(int oID);
        bool UpDateProduct(Product oProduct, int nEditID);
        ID Save(Product oProduct);
        DataTable GetsbyDT(string sSQL);
    }

    #endregion
}
