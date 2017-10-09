using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace AgendaTelefonica.Models
{
    public class algoritmoMD5
    {
        public string GetMD5(string strPlain)
        {
            
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(strPlain);
            MD5 md5 = new MD5CryptoServiceProvider();
            string strHex = "";


            HashValue = md5.ComputeHash(MessageBytes);
            foreach (byte b in HashValue)
            {
                strHex += String.Format("{0:x2}", b);
            }
            return strHex;
        }
    }
}