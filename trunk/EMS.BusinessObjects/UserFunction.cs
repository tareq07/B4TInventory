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
    #region  UserFunction
    public class UserFunction : BusinessObject
    {

        #region Constructor
        public UserFunction() { }

        #endregion

        #region Properties

        #region Is_Parent
        private bool _bIs_Parent = false;
        public bool Is_Parent
        { get; set; }
        #endregion

        #region ParentID
        private int _nParentID = 0;
        public int ParentID
        { get; set; }
        #endregion

        #region Function_Name
        private string _sFunction_Name = "";
        public string Function_Name
        { get; set; }
        #endregion
        #region IsCheck
        private bool _bIsCheck = false;
        public bool IsCheck
        { get; set; }
        #endregion
        #endregion

        #region Functions

        //public UserFunction Get(int nID)
        //{
        //    return UserFunction.Service.Get(nID);
        //}
        //public ID Save()
        //{
        //    return UserFunction.Service.Save(this);
        //}
        //public void Delete()
        //{
        //    UserFunction.Service.Delete(this.ObjectID);
        //}        
        #endregion

        #region Service Factory
        internal static IUserFunctionService Service
        {
            get { return (IUserFunctionService)Services.Factory.CreateService(typeof(IUserFunctionService)); }
        }

        #endregion

    }
    #endregion

    #region UserFunctions
    public class UserFunctions : IndexedBusinessObjects
    {
        #region Collection Class Methods
        public void Add(UserFunction oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(UserFunction oItem)
        {
            base.RemoveItem(oItem);
        }
        public UserFunction this[int index]
        {
            get { return (UserFunction)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }
        #endregion

        #region Functions
        public static UserFunctions Get()
        {
            return UserFunction.Service.Get();
        }
        
        public static UserFunctions Gets(int nID)
        {
            return UserFunction.Service.Gets(nID);
        }
        //public static UserFunctions GetsChildFunction(int nUserID, int nFunctionID)
        //{
        //    return UserFunction.Service.GetsChildFunction(nUserID, nFunctionID);
        //}
        #endregion
    }
    #endregion

    #region IUserFunction interface
    public interface IUserFunctionService
    {
        //UserFunction Get(int id);
        UserFunctions Get();
        UserFunctions Gets(int nID);
        //UserFunctions GetsChildFunction(int nUserID, int nFunctionID);
        //void Delete(int oID);       
        //ID Save(UserFunction oUserFunction);
        
    }
    #endregion
    
}


