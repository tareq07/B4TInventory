using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections;
using System.Reflection;

namespace EMS.Core.Utility
{
    #region Global Class
    public class EMSGlobal
    {
        #region Current User
        public static int _nCurrentUserID = 0;
        public static int CurrentUser(int ID)
        {
            _nCurrentUserID = ID;
            return _nCurrentUserID;
        }
        #endregion

        #region Current UserName
        public static string _sCurrenUserName = "";
        public static string CurrentUserName(string sCurrenUserName)
        {
            _sCurrenUserName = sCurrenUserName;
            return _sCurrenUserName;
        }
        #endregion

        #region Password related function
        public static string Encrypt(string sText)
        {
            int i = 0;
            string sEncrypt = "", sKey = "cel.abracadabra";
            char cTextChar, cKeyChar;
            char[] cTextData, cKey;

            //Save Length of Pass
            sText = (char)(sText.Length) + sText;

            //Pad Password with space upto 10 Characters
            if (sText.Length < 10)
            {
                sText = sText + sText.PadRight((10 - sText.Length), ' ');
            }
            cTextData = sText.ToCharArray();

            //Make the key big enough
            while (sKey.Length < sText.Length)
            {
                sKey = sKey + sKey;
            }
            sKey = sKey.Substring(0, sText.Length);
            cKey = sKey.ToCharArray();

            //Encrypting Data
            for (i = 0; i < sText.Length; i++)
            {
                cTextChar = (char)cTextData.GetValue(i);
                cKeyChar = (char)cKey.GetValue(i);
                sEncrypt = sEncrypt + IntToHex((int)(cTextChar) ^ (int)(cKeyChar));
            }

            return sEncrypt;
        }

        public static string Decrypt(string sText)
        {
            int j = 0, i = 0, nLen = 0;
            string sTextByte = "", sDecrypt = "", sKey = "cel.abracadabra";
            char[] cTextData, cKey;
            char cTextChar, cKeyChar;

            //Taking Lenght, half of Encrypting data  
            nLen = sText.Length / 2;

            //Making key is big Enough
            while (sKey.Length < nLen)
            {
                sKey = sKey + sKey;
            }
            sKey = sKey.Substring(0, nLen);
            cKey = sKey.ToCharArray();
            cTextData = sText.ToCharArray();

            //Decripting data
            for (i = 0; i < nLen; i++)
            {
                sTextByte = "";
                for (j = i * 2; j < (i * 2 + 2); j++)
                {
                    sTextByte = sTextByte + cTextData.GetValue(j).ToString();
                }
                cTextChar = (char)HexToInt(sTextByte);
                cKeyChar = (char)cKey.GetValue(i);
                sDecrypt = sDecrypt + (char)((int)(cKeyChar) ^ (int)(cTextChar));
            }

            //Taking real password
            cTextData = sDecrypt.ToCharArray();
            sDecrypt = "";
            i = (int)(char)cTextData.GetValue(0);
            for (j = 1; j <= i; j++)
            {
                sDecrypt = sDecrypt + cTextData.GetValue(j).ToString();
            }
            return sDecrypt;
        }

        private static string IntToHex(int nIntData)
        {
            return Convert.ToString(nIntData, 16).PadLeft(2, '0');
        }

        private static int HexToInt(string sHexData)
        {
            return Convert.ToInt32(sHexData, 16);
        }
        #endregion
    }
    #endregion
}

