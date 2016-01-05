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
    #region ChartOfAcc
    public class ChartOfAcc : BusinessObject
    {
        #region Constructor
        public ChartOfAcc() { }
        #endregion

        #region Properties

        public string Coa_type { get; set; }

        public int Coa_parent_id { get; set; }

        public bool Coa_is_perent { get; set; }

        public string Coa_title { get; set; }

        public string Coa_level_code { get; set; }

        public string Coa_account_code { get; set; }

        public string Coa_legacy_code { get; set; }

        public bool Coa_is_ledgerhead { get; set; }

        public string CoatitleWithCode 
        { get { return " ["+this.Coa_type.Trim()+"]"+"(" + this.Coa_level_code + ") " + this.Coa_title; } }

        #endregion

        #region Functions
        public ChartOfAcc GetByID(int nID)
        {
            return ChartOfAcc.Service.GetByID(nID);
        }
        public ID Save()
        {
            return ChartOfAcc.Service.Save(this);
        }
        public bool Delete(int nID)
        {
            return ChartOfAcc.Service.Delete(nID);
        }
        public bool UpDateChartOfAcc(ChartOfAcc oChartOfAcc, int nEditID)
        {
            return ChartOfAcc.Service.UpDateChartOfAcc(oChartOfAcc, nEditID);
        }

        #endregion

        #region Service Factory
        internal static IChartOfAccService Service
        {
            get { return (IChartOfAccService)Services.Factory.CreateService(typeof(IChartOfAccService)); }
        }

        #endregion
    }

    #endregion

    #region ChartOfAccs
    public class ChartOfAccs : IndexedBusinessObjects
    {

        #region Collection Class Methods
        public void Add(ChartOfAcc oItem)
        {
            base.AddItem(oItem);
        }
        public void Remove(ChartOfAcc oItem)
        {
            base.RemoveItem(oItem);
        }
        public ChartOfAcc this[int index]
        {
            get { return (ChartOfAcc)GetItem(index); }
        }
        public int GetIndex(int nID)
        {
            return base.GetIndex(new ID(nID));
        }

        #endregion

        #region Collection Class Functions
        public static ChartOfAccs Gets()
        {
            return ChartOfAcc.Service.Gets();
        }
        public static ChartOfAccs GetsByID(int nID)
        {
            return ChartOfAcc.Service.GetsByID(nID);
        }
        public static ChartOfAccs GetsByString(string sString)
        {
            return ChartOfAcc.Service.GetsByString(sString);
        }
        public static DataTable GetsbyDT(string sSQL)
        {
            return ChartOfAcc.Service.GetsbyDT(sSQL);
        }
        public static DataTable GetGLDetailbyDT()
        {
            return ChartOfAcc.Service.GetGLDetailbyDT();
        }
        public static DataTable GetPeriodicGLbyDT(string sFrom, string sTO)
        {
            return ChartOfAcc.Service.GetPeriodicGLbyDT(sFrom, sTO);
        }
        public static DataTable GetPLDetailbyDT(string p, string p_2)
        {
            return ChartOfAcc.Service.GetPLDetailbyDT(p, p_2);
        }
        #endregion

    }

    #endregion

    #region IChartOfAcc interface
    public interface IChartOfAccService
    {
        ChartOfAcc GetByID(int nID);
        ChartOfAccs Gets();
        ChartOfAccs GetsByID(int nID);
        ChartOfAccs GetsByString(string sString);
        bool Delete(int oID);
        bool UpDateChartOfAcc(ChartOfAcc oChartOfAcc, int nEditID);
        ID Save(ChartOfAcc oChartOfAcc);
        DataTable GetsbyDT(string sSQL);
        DataTable GetGLDetailbyDT();
        DataTable GetPeriodicGLbyDT(string sFrom, string sTO);
        DataTable GetPLDetailbyDT(string p, string p_2);
    }

    #endregion
}
