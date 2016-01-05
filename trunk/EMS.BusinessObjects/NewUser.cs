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
    #region NewUser
    public class NewUser : BusinessObject
    {
        #region Constructor
        public NewUser() { }

        #endregion

        #region Properties

        //#region brn_id
        //private int _nbrn_id = 0;
        //public int brn_id
        //{
        //    get;
        //    set;
        //}
        //#endregion

        //#region brn_title
        //private string _sbrn_title = "";
        //public string BrName
        //{
        //    get;
        //    set;
        //}
        //#endregion

        #region user_code
        private string _suser_code = "";
        public string user_code
        {
            get;
            set;
        }
        #endregion

        #region user_fst_name
        private string _suser_fst_name = "";
        public string user_fst_name
        {
            get;
            set;
        }
        #endregion

        #region user_lst_name
        private string _suser_lst_nameme = "";
        public string user_lst_name
        {
            get;
            set;
        }
        #endregion

        #region user_name_ini

        public string user_name_ini
        {
            get;
            set;
        }
        #endregion

        #region user_name
        private string _suser_name = "";
        public string user_name
        {
            get;
            set;
        }
        #endregion

        #region user_pass
        private string _suser_pass = "";
        public string user_pass
        {
            get;
            set;
        }
        #endregion

        #region user_type
        private EnumUserType _euser_type = EnumUserType.None;
        public EnumUserType user_type
        { get; set; }
        #endregion

        #region user_islogon
        private bool _buser_islogon = false;
        public bool user_islogon
        {
            get;
            set;
        }
        #endregion

        #region user_lock
        private bool _buser_lock = false;
        public bool user_lock
        {
            get;
            set;
        }
        #endregion

        #region user_status
        private EnumUserStatus _euser_status = EnumUserStatus.Active;
        public EnumUserStatus user_status
        { get; set; }
        #endregion

        #region OwnerName
        private string _sOwnerName = "";
        public string OwnerName
        { get; set; }
        #endregion

        #region enum user_type
        private string _suser_type = "";
        public string euser_type
        {
            get
            {
                return _suser_type = this.user_type.ToString();
            }
        }
        #endregion

        #region UserIsLogOn
        private string _sUserIsLogOn = "";
        public string UserIsLogOn
        {
            get
            {
                if (this.user_islogon == true)
                {
                    return _sUserIsLogOn = "true.png";
                }
                else
                {
                    return _sUserIsLogOn = "false.png";
                }
            }
        }
        #endregion

        #region UserList
        private string _sUserList = "";
        public string UserList
        {
            get
            {
                _sUserList = this.user_fst_name + " " + this.user_lst_name + " [" + this.user_code + "]";
                return _sUserList;
            }
        }

        #endregion
        #region UserFullName
        public string _user_full_name = "";
        public string user_full_name
        { get { return _user_full_name = this.user_fst_name + " " + this.user_lst_name; } }
        #endregion
        #endregion

        #region Functions
        public NewUser Get(int nNewUserID)
        {
            return NewUser.Service.Get(nNewUserID);
        }
        public ID Save()
        {
            return NewUser.Service.Save(this);
        }
        public bool UpdateUser(NewUser oNewUser, int nEditID)
        {
            return NewUser.Service.UpdateUser(oNewUser, nEditID);
        }
        public bool ChangePass(string _sNewUN, string sNewUserPass, string sOldUN)
        {
            return NewUser.Service.ChangePass(_sNewUN, sNewUserPass, sOldUN);
        }
        public bool UserDelete(int nUserID)
        {
            return NewUser.Service.UserDelete(nUserID);
        }
        //public static DataTable UserCodeIsExist(string sUserType,string sUserCode)
        //{
        //    return NewUser.Service.UserCodeIsExist(sUserType,sUserCode);
        //}
        public bool UpdateUser_IsLock(int CurrentUserID, bool LockUnlock)
        {
            return NewUser.Service.UpdateUser_IsLock(CurrentUserID, LockUnlock);
        }
        public bool UserStatus(int CurrentUserID, int _nUStatus)
        {
            return NewUser.Service.UserStatus(CurrentUserID, _nUStatus);
        }
        public bool PWReset(int nUserID)
        {
            return NewUser.Service.PWReset(nUserID);
        }
        public bool UpdateUser_IsLogon(int nUserID, bool LogUnlog)
        {
            return NewUser.Service.UpdateUser_IsLogon(nUserID, LogUnlog);
        }
        #endregion

        #region Service Factory
        internal static INewUserService Service
        {
            get { return (INewUserService)Services.Factory.CreateService(typeof(INewUserService)); }
        }
        #endregion
    }
    #endregion

    #region NewUsers
    public class NewUsers : IndexedBusinessObjects
    {
        #region Collection Class Methods
        public void Add(NewUser oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(NewUser oItem)
        {
            base.RemoveItem(oItem);
        }
        public NewUser this[int index]
        {
            get { return (NewUser)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }
        #endregion

        #region Functions
        public static NewUsers Get()
        {
            return NewUser.Service.Get();
        }
        public static NewUsers GetsByeString(string sStr)
        {
            return NewUser.Service.GetsByeString(sStr);
        }
        #endregion
    }
    #endregion

    #region INewUser interface
    public interface INewUserService
    {
        NewUser Get(int nNewUserID);
        NewUsers Get();
        NewUsers GetsByeString(string sStr);
        bool UpdateUser(NewUser oNewUser, int nEditID);
        bool ChangePass(string _sNewUN, string sNewUserPass, string sOldUN);
        bool UserDelete(int nUserID);
        ID Save(NewUser oNewUser);
        //DataTable UserCodeIsExist(string sUserType, string sUserCode);
        bool UpdateUser_IsLock(int CurrentUserID, bool LockUnlock);
        bool UserStatus(int CurrentUserID, int _nUStatus);
        bool PWReset(int nUserID);
        bool UpdateUser_IsLogon(int nUserID, bool LogUnlog);
    }
    #endregion
}
