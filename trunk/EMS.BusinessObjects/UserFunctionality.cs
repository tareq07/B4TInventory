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
    
    #region  User Functionality
    public class UserFunctionality : BusinessObject
    {

        #region Constructor
        public UserFunctionality() { }

        #endregion

        #region Properties

        #region UFID
        private int _nUFID = 0;
        public int UFID
        { get; set; }
        #endregion

        #region User_ID
        private int _nUser_ID = 0;
        public int User_ID
        { get; set; }
        #endregion
       

        #endregion

        #region Functions

        //public UserFunctionality Get(int nManuID)
        //{
        //    return UserFunctionality.Service.Get(nManuID);
        //}
        public ID Save()
        {
            return UserFunctionality.Service.Save(this);
        }
        //public void Delete()
        //{
        //    UserFunctionality.Service.Delete(this.ObjectID);
        //}
        public bool RemoveFunction(int nUFID, int nUserID)
        {
           return UserFunctionality.Service.RemoveFunction(nUFID, nUserID);
        }
        #endregion

        #region Service Factory
        internal static IUserFunctionalityService Service
        {
            get { return (IUserFunctionalityService)Services.Factory.CreateService(typeof(IUserFunctionalityService)); }
        }

        #endregion

    }
    #endregion

    #region UserFunctionalitys
    public class UserFunctionalitys : IndexedBusinessObjects
    {
        #region Collection Class Methods
        public void Add(UserFunctionality oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(UserFunctionality oItem)
        {
            base.RemoveItem(oItem);
        }
        public UserFunctionality this[int index]
        {
            get { return (UserFunctionality)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }
        #endregion

        #region Functions
        //public static UserFunctionalitys Get()
        //{
        //    return UserFunctionality.Service.Get();
        //}

        //public static UserFunctionalitys Gets(int nUserID)
        //{
        //    return UserFunctionality.Service.Gets(nUserID);
        //}
        
        #endregion
    }
    #endregion

    #region IUserFunctionality interface
    public interface IUserFunctionalityService
    {
        //UserFunctionality Get(int id);
        //UserFunctionalitys Get();
        //UserFunctionalitys Gets(int nUserID);
        //void Delete(int oID);
        bool RemoveFunction(int nUFID, int nUserID);
        ID Save(UserFunctionality oUserFunctionality);
        
    }
    #endregion
}
