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
 	#region UserSecurity
	public class UserSecurity : BusinessObject
	{ 
	#region Constructor
		public UserSecurity(){}
	#endregion
 
	#region Properties
	public int user_id {get; set;}
	public string MacAddress {get; set;}
	public bool IsAuthorise {get; set;}
	public int AuthoriseBy {get; set;}
	public DateTime RequestDate {get; set;}
	public DateTime AuthoriseDate {get; set;}
 
	#endregion
 
	#region Functions
	public UserSecurity GetByID(int nID)
 	{
 		return UserSecurity.Service.GetByID(nID);
 }
	public ID Save()
 {
 		return UserSecurity.Service.Save(this);
 }
	public bool Delete(int nID)
 {
 		return UserSecurity.Service.Delete(nID);
 }
	public bool UpDateUserSecurity(UserSecurity oUserSecurity, int nEditID)
 {
 		return UserSecurity.Service.UpDateUserSecurity(oUserSecurity,nEditID);
 }
 
	#endregion
 
	#region Service Factory
	internal static IUserSecurityService Service
 {
 		get { return (IUserSecurityService)Services.Factory.CreateService(typeof(IUserSecurityService));} 
 }

	#endregion
 }
 
	#endregion
 
	#region UserSecuritys
	public class UserSecuritys : IndexedBusinessObjects
 {
 
	#region Collection Class Methods
	public void Add(UserSecurity oItem)
 	{
 		base.AddItem(oItem);
 	}
	public void Remove(UserSecurity oItem)
 	{
 	base.RemoveItem(oItem);
 	}
	public UserSecurity this[int index]
 	{
 		get { return (UserSecurity)GetItem(index);}
  	}
	public int GetIndex(int nID)
 	{
 		return base.GetIndex(new ID(nID));
 	}
 
	#endregion
 
	#region Collection Class Functions
	public static UserSecuritys Gets()
 	{
 		return UserSecurity.Service.Gets();
 	}
	public static UserSecuritys GetsByID(int nID)
 	{
 		return UserSecurity.Service.GetsByID(nID);
 	}
	public static UserSecuritys GetsByString(string sString)
 	{
 		return UserSecurity.Service.GetsByString(sString);
 	}
	public static DataTable GetsbyDT(string sSQL)
 	{
 		return UserSecurity.Service.GetsbyDT(sSQL);
 	}
 
	#endregion
 }
 
	#endregion
 
	#region IUserSecurity interface
	public interface IUserSecurityService
 	{
 		UserSecurity GetByID(int nID);
 		UserSecuritys Gets();
 		UserSecuritys GetsByID(int nID);
 		UserSecuritys GetsByString(string sString);
 		bool Delete(int oID);
 		bool UpDateUserSecurity(UserSecurity oUserSecurity, int nEditID);
 		ID Save(UserSecurity oUserSecurity);
 		DataTable GetsbyDT(string sSQL);
 	}
 
	#endregion
 }
