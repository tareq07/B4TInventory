
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
    #region AddBranch
    public class AddBranch : BusinessObject
    {
        #region Constructor
        public AddBranch() { }

        #endregion

        #region Properties

        #region brn_type
        private EnumBrnchType _ebrn_type = EnumBrnchType.None;
        public EnumBrnchType ebrn_type
        { get; set; }
        #endregion

        #region sbrn_type
        private string _sbrn_type = "";
        public string sbrn_type
        { get { return _sbrn_type= this.ebrn_type.ToString();} }
        #endregion

        #region brn_title
        private string _sbrn_title = "";
        public string brn_title
        {get; set;}
        #endregion
        
        #region brn_location
        private string _sbrn_location = "";
        public string brn_location
        { get; set; }
        #endregion 

        #endregion

        #region Functions
        public AddBranch Get(int nAddBranchID)
        {
            return AddBranch.Service.Get(nAddBranchID);
        }
        public ID Save()
        {
            return AddBranch.Service.Save(this);
        }
        public bool Delete(int nBrnID)
        {
            return AddBranch.Service.Delete(nBrnID);
        }
        public bool UpdateBranch(AddBranch oAddBranch, int nBrnID)
        {
            return AddBranch.Service.UpdateBranch(oAddBranch,nBrnID);
        }
        #endregion

        #region Service Factory
        internal static IAddBranchService Service
        {
            get { return (IAddBranchService)Services.Factory.CreateService(typeof(IAddBranchService)); }
        }
        #endregion
    }
    #endregion

    #region AddBranchs
    public class AddBranchs : IndexedBusinessObjects
    {
        #region Collection Class Methods
        public void Add(AddBranch oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(AddBranch oItem)
        {
            base.RemoveItem(oItem);
        }
        public AddBranch this[int index]
        {
            get { return (AddBranch)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }
        #endregion

        #region Functions
        public static AddBranchs Get()
        {
            return AddBranch.Service.Get();
        }
        public static AddBranchs Gets(int nID)
        {
            return AddBranch.Service.Gets(nID);
        }
        public static AddBranchs GetsByType(int nbrn_type)
        {
            return AddBranch.Service.GetsByType(nbrn_type);
        }
        public static DataSet GetBrnsbyDS()
        {
            return AddBranch.Service.GetBrnsbyDS();
        }
        #endregion
    }
    #endregion

    #region IAddBranch interface
    public interface IAddBranchService
    {
        AddBranch Get(int nAddBranchID);
        AddBranchs Get();
        AddBranchs Gets(int nID);
        AddBranchs GetsByType(int nbrn_type);
        bool Delete(int nBrnID);
        ID Save(AddBranch oAddBranch);
        DataSet GetBrnsbyDS();
        bool UpdateBranch(AddBranch oAddBranch, int nBrnID);
    }
    #endregion
}

