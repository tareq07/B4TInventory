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
    #region  MenuPermission
    public class MenuPermission : BusinessObject
    {

        #region Constructor
        public MenuPermission() { }

        #endregion

        #region Properties
        #region MenuID
        private int _nMenuID = 0;
        public int MenuID
        { get; set; }
        #endregion

        #region UserID
        private int _nUserID = 0;
        public int UserID
        { get; set; }
        #endregion         

        #endregion

        #region Functions

        //public MenuPermission Get(int nManuID)
        //{
        //    return MenuPermission.Service.Get(nManuID);
        //}
        public ID Save()
        {
            return MenuPermission.Service.Save(this);
        }
        //public void Delete()
        //{
        //    MenuPermission.Service.Delete(this.ObjectID);
        //}
        public void DeletePermission(int nMenuID, int CurrentUserID)
        {
            MenuPermission.Service.DeletePermission(nMenuID,CurrentUserID);
        }
        public void DeletePermissions(int CurrentUserID)
        {
            MenuPermission.Service.DeletePermissions(CurrentUserID);
        }
        #endregion

        #region Service Factory
        internal static IMenuPermissionService Service
        {
            get { return (IMenuPermissionService)Services.Factory.CreateService(typeof(IMenuPermissionService)); }
        }

        #endregion

    }
    #endregion

    #region MenuPermissions
    public class MenuPermissions : IndexedBusinessObjects
    {
        #region Collection Class Methods
        public void Add(MenuPermission oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(MenuPermission oItem)
        {
            base.RemoveItem(oItem);
        }
        public MenuPermission this[int index]
        {
            get { return (MenuPermission)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }
        #endregion

        #region Functions
        //public static MenuPermissions Get()
        //{
        //    return MenuPermission.Service.Get();
        //}

        //public static MenuPermissions Gets(int nUserID)
        //{
        //    return MenuPermission.Service.Gets(nUserID);
        //}

        //public static MenuPermissions GetsChildManu(int nManuID, int CurrentUserId)
        //{
        //    return MenuPermission.Service.GetsChildManu(nManuID, CurrentUserId);
        //}
        //public static MenuPermissions GetsSubChildManu(int nManuID)
        //{
        //    return MenuPermission.Service.GetsSubChildManu(nManuID);
        //}
        //public static DataSet GetsTree(int nUserID)
        //{
        //    return MenuPermission.Service.GetsTree(nUserID);
        //}
        #endregion
    }
    #endregion

    #region IMenuPermission interface
    public interface IMenuPermissionService
    {
        //MenuPermission Get(int id);
        //MenuPermissions Get();
        //MenuPermissions Gets(int nUserID);
        //MenuPermissions GetsChildManu(int nManuID, int CurrentUserId);
        //MenuPermissions GetsSubChildManu(int nManuID);
        //void Delete(int oID);
        void DeletePermission(int nMenuID, int CurrentUserID);
        void DeletePermissions(int CurrentUserID);
        ID Save(MenuPermission oMenuPermission);
        //DataSet GetsTree(int nUserID);
    }
    #endregion
    
}

