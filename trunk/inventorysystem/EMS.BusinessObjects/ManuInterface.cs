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
    #region  ManuInterface
    public class ManuInterface : BusinessObject
    {

        #region Constructor
        public ManuInterface() { }

        #endregion

        #region Properties

        public int Parent_id { get; set; }
        public bool Is_parent { get; set; }
        public bool Sub_parent { get; set; }
        public bool IsCheck { get; set; }
        public string Menu_title { get; set; }
        public string NevigetUrl { get; set; }

        #endregion

        #region Functions

        public ManuInterface Get(int nManuID)
        {
            return ManuInterface.Service.Get(nManuID);
        }
        public ID Save()
        {
            return ManuInterface.Service.Save(this);
        }
        public void Delete()
        {
            ManuInterface.Service.Delete(this.ObjectID);
        }
        public void UpdateUser_IsLock(int CurrentUserID, bool LockUnlock)
        {
            ManuInterface.Service.UpdateUser_IsLock(CurrentUserID, LockUnlock);
        }
        public void UserStatus(int CurrentUserID, int _nUStatus)
        {
            ManuInterface.Service.UserStatus(CurrentUserID, _nUStatus);
        }
        public void LogOut(int CurrentUserID, bool bLogOut)
        {
            ManuInterface.Service.LogOut(CurrentUserID, bLogOut);
        }

        #endregion

        #region Service Factory
        internal static IManuInterfaceService Service
        {
            get { return (IManuInterfaceService)Services.Factory.CreateService(typeof(IManuInterfaceService)); }
        }

        #endregion

    }
    #endregion

    #region ManuInterfaces
    public class ManuInterfaces : IndexedBusinessObjects
    {
        #region Collection Class Methods
        public void Add(ManuInterface oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(ManuInterface oItem)
        {
            base.RemoveItem(oItem);
        }
        public ManuInterface this[int index]
        {
            get { return (ManuInterface)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }
        #endregion

        #region Functions


        public static ManuInterfaces Gets(int nUserID)
        {
            return ManuInterface.Service.Gets(nUserID);
        }


        #endregion
    }
    #endregion

    #region IManuInterface interface
    public interface IManuInterfaceService
    {
        ManuInterface Get(int id);
        ManuInterfaces Gets(int nUserID);
        void Delete(int oID);
        void LogOut(int CurrentUserID, bool bLogOut);
        ID Save(ManuInterface oManuInterface);
        void UpdateUser_IsLock(int CurrentUserID, bool LockUnlock);
        void UserStatus(int CurrentUserID, int _nUStatus);
    }
    #endregion
    
}
