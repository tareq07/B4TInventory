using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace EMS.Core.DataAccess
{
    public class ExecuteQueryFunctions
    {
                
        #region ExeNonQuery
        public static void ExeNonQuery(SqlConnection _con, string QueryString)
        {            

            SqlCommand cmd = new SqlCommand(QueryString, _con);
            if (_con.State == ConnectionState.Open)
            { }
            else
            {
                cmd.Connection.Open();
            }
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.Connection.Close();
        }
        #endregion

        #region Generation Function
        public static int GetNewID(SqlConnection _con, string QueryString)
        {            
            SqlCommand cmd = new SqlCommand(QueryString,_con);
            if (_con.State == ConnectionState.Open)
            { }
            else
            {
                cmd.Connection.Open();
            }            
            
            object nMaxID = cmd.ExecuteScalar();
           
            cmd.Connection.Close();
            if (nMaxID == DBNull.Value)
            {
                nMaxID = 1;
            }
            else
            {
                nMaxID = Convert.ToInt32(nMaxID) + 1;
                if ((int)nMaxID <= 0)
                {
                    nMaxID = 1;
                }
            }

            return (int)nMaxID;
        }
        #endregion

        #region IDataReader
        public static IDataReader ExeReader(SqlConnection _con, string QueryString)
        {
            
            SqlCommand cmd = new SqlCommand(QueryString, _con);
            if (_con.State == ConnectionState.Open)
            { }
            else
            {
                cmd.Connection.Open();
            }
            SqlDataReader reader = cmd.ExecuteReader();
            
            return reader;
        }
        #endregion

        #region ExecuteScalar
        public static bool ExeSclr(SqlConnection _con, string QueryString)
        {
            int nHas = 0;            
            SqlCommand cmd = new SqlCommand(QueryString, _con);
            if (_con.State == ConnectionState.Open)
            { }
            else
            {
                cmd.Connection.Open();
            }
            object bIsExist = cmd.ExecuteScalar();
            cmd.Connection.Close();
            nHas = Convert.ToInt32(bIsExist);
            if (nHas > 0)
            { return true; }
            else
            { return false; }
        }
        #endregion        

        #region ExecuteScalar for string
        public static string ExeRetStr(SqlConnection _con, string QueryString)
        {
            SqlCommand cmd = new SqlCommand(QueryString, _con);
            if (_con.State == ConnectionState.Open)
            { }
            else
            {
                cmd.Connection.Open();
            }
            string sRetStr = (string)cmd.ExecuteScalar();
            return sRetStr;
        }
        #endregion

        #region ExecuteScalar IsExist

        public static bool ExeIsExist(string QueryString)
        {
            SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
            int nHas = 0;
            SqlCommand cmd = new SqlCommand(QueryString, _conn);
            if (_conn.State == ConnectionState.Open)
            { }
            else
            {
                cmd.Connection.Open();
            }
            object bIsExist = cmd.ExecuteScalar();
            cmd.Connection.Close();
            nHas = Convert.ToInt32(bIsExist);
            if (nHas > 0)
            { return true; }
            else
            { return false; }
        }
        #endregion

        #region HasChild
        public static bool HasChild(string QueryString)
        {
            SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
            int nHas = 0;
            SqlCommand cmd = new SqlCommand(QueryString, _conn);
            if (_conn.State == ConnectionState.Open)
            { }
            else
            {
                cmd.Connection.Open();
            }
            object bIsExist = cmd.ExecuteScalar();
            cmd.Connection.Close();
            nHas = Convert.ToInt32(bIsExist);
            if (nHas > 0)
            { return true; }
            else
            { return false; }
        }
        #endregion 

        #region ExecuteScalar for intiger
        public static int ExeRetInt(string QueryString)
        {
            int nHas = 0;
            SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
            SqlCommand cmd = new SqlCommand(QueryString, _conn);
            if (_conn.State == ConnectionState.Open)
            { }
            else
            {
                cmd.Connection.Open();
            }
            object bIsExist = cmd.ExecuteScalar();
            cmd.Connection.Close();
            nHas = Convert.ToInt32(bIsExist);
            return nHas;
        }
        #endregion
        #region ExeNonQuery
        public static void ExeNonQuery(string QueryString)
        {
            SqlConnection _conn = new SqlConnection(EMSConFunc.ConString());
            SqlCommand cmd = new SqlCommand(QueryString, _conn);
            if (_conn.State == ConnectionState.Open)
            { }
            else
            {
                cmd.Connection.Open();
            }
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.Connection.Close();
        }
        #endregion
        
    }
}
