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
 	#region ProductLot
	public class ProductLot : BusinessObject
	{ 
	#region Constructor
		public ProductLot(){}
	#endregion
 
	#region Properties
	public string LotNo {get; set;}
	public int Pro_ID {get; set;}
	public string barCode {get; set;}
	public double Total_Qty {get; set;}
	public double Bag_Qty {get; set;}
	public double Sale_Qty {get; set;}
	public double Ret_Qty {get; set;}
	public double purchesPrice {get; set;}
	public double salePrice {get; set;}
 
	#endregion
 
	#region Functions
	public ProductLot GetByID(int nID)
 	{
 		return ProductLot.Service.GetByID(nID);
 }
	public ID Save()
 {
 		return ProductLot.Service.Save(this);
 }
	public bool Delete(int nID)
 {
 		return ProductLot.Service.Delete(nID);
 }
	public bool UpDateProductLot(ProductLot oProductLot, int nEditID)
 {
 		return ProductLot.Service.UpDateProductLot(oProductLot,nEditID);
 }
 
	#endregion
 
	#region Service Factory
	internal static IProductLotService Service
 {
 		get { return (IProductLotService)Services.Factory.CreateService(typeof(IProductLotService));} 
 }

	#endregion
 }
 
	#endregion
 
	#region ProductLots
	public class ProductLots : IndexedBusinessObjects
 {
 
	#region Collection Class Methods
	public void Add(ProductLot oItem)
 	{
 		base.AddItem(oItem);
 	}
	public void Remove(ProductLot oItem)
 	{
 	base.RemoveItem(oItem);
 	}
	public ProductLot this[int index]
 	{
 		get { return (ProductLot)GetItem(index);}
  	}
	public int GetIndex(int nID)
 	{
 		return base.GetIndex(new ID(nID));
 	}
 
	#endregion
 
	#region Collection Class Functions
	public static ProductLots Gets()
 	{
 		return ProductLot.Service.Gets();
 	}
	public static ProductLots GetsByID(int nID)
 	{
 		return ProductLot.Service.GetsByID(nID);
 	}
	public static ProductLots GetsByString(string sString)
 	{
 		return ProductLot.Service.GetsByString(sString);
 	}
	public static DataTable GetsbyDT(string sSQL)
 	{
 		return ProductLot.Service.GetsbyDT(sSQL);
 	}
 
	#endregion
 }
 
	#endregion
 
	#region IProductLot interface
	public interface IProductLotService
 	{
 		ProductLot GetByID(int nID);
 		ProductLots Gets();
 		ProductLots GetsByID(int nID);
 		ProductLots GetsByString(string sString);
 		bool Delete(int oID);
 		bool UpDateProductLot(ProductLot oProductLot, int nEditID);
 		ID Save(ProductLot oProductLot);
 		DataTable GetsbyDT(string sSQL);
 	}
 
	#endregion
 }
