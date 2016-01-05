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
    #region ProductConfigure
    public class ProductConfigure : BusinessObject
    {
        #region Constructor
        public ProductConfigure() { }
        #endregion

        #region Properties

        public int prodId { get; set; }

        public string Pro_Name { get; set; }

        public double unitQty { get; set; }

        public EnumUnitType unitType { get; set; }

        #endregion

        #region Functions
        public ProductConfigure GetByID(int nID)
        {
            return ProductConfigure.Service.GetByID(nID);
        }
        public ID Save()
        {
            return ProductConfigure.Service.Save(this);
        }
        public bool Delete(int nID)
        {
            return ProductConfigure.Service.Delete(nID);
        }
        public bool UpDateProductConfigure(ProductConfigure oProductConfigure, int nEditID)
        {
            return ProductConfigure.Service.UpDateProductConfigure(oProductConfigure, nEditID);
        }

        #endregion

        #region Service Factory
        internal static IProductConfigureService Service
        {
            get { return (IProductConfigureService)Services.Factory.CreateService(typeof(IProductConfigureService)); }
        }

        #endregion
    }

    #endregion

    #region ProductConfigures
    public class ProductConfigures : IndexedBusinessObjects
    {

        #region Collection Class Methods
        public void Add(ProductConfigure oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(ProductConfigure oItem)
        {
            base.RemoveItem(oItem);
        }
        public ProductConfigure this[int index]
        {
            get { return (ProductConfigure)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }

        #endregion

        #region Collection Class Functions
        public static ProductConfigures Gets()
        {
            return ProductConfigure.Service.Gets();
        }
        public static ProductConfigures GetsByID(int nID)
        {
            return ProductConfigure.Service.GetsByID(nID);
        }
        public static ProductConfigures GetsByString(string sString)
        {
            return ProductConfigure.Service.GetsByString(sString);
        }
        public static DataTable GetsbyDT(string sSQL)
        {
            return ProductConfigure.Service.GetsbyDT(sSQL);
        }

        #endregion
    }

    #endregion

    #region IProductConfigure interface
    public interface IProductConfigureService
    {
        ProductConfigure GetByID(int nID);
        ProductConfigures Gets();
        ProductConfigures GetsByID(int nID);
        ProductConfigures GetsByString(string sString);
        bool Delete(int oID);
        bool UpDateProductConfigure(ProductConfigure oProductConfigure, int nEditID);
        ID Save(ProductConfigure oProductConfigure);
        DataTable GetsbyDT(string sSQL);
    }

    #endregion
}
