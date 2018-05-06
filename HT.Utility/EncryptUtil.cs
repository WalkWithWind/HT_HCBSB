using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HT.Utility
{
    /// <summary>
    /// 加密通用类
    /// </summary>
    public sealed class EncryptUtil
    {

        #region MD5加密

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="content">要加密的内容</param>
        /// <param name="iscapital">是否大写加密</param>
        public static string MD5Encrypt(string content, bool iscapital=false)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
            StringBuilder sb = new StringBuilder();
            foreach (byte item in data)
            {
                sb.AppendFormat("{0:x2}", item);
            }
            var resdata = iscapital ? sb.ToString().ToUpper() : sb.ToString();
            return resdata;
        }

        #endregion

        #region DES加密

        /// <summary> 
        /// DES加密
        /// </summary> 
        /// <param name="content">要加密的内容</param> 
        /// <param name="key">密钥</param> 
        public static string DesEncrypt(string content, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(content);
            des.Key = Encoding.ASCII.GetBytes(MD5Encrypt(key).Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes("19921208");
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>
        /// 使用默认密钥DES加密
        /// </summary>
        /// <param name="content">要加密的内容</param>
        public static string DesEncrypt(string content)
        {
            return DesEncrypt(content, "haitao");
        }

        #endregion

        #region DES解密

        /// <summary>
        /// 使用默认密钥DES解密
        /// </summary>
        /// <param name="content">要解密的内容</param>
        public static string DesDecrypt(string content)
        {
            return DesDecrypt(content, "haitao");
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="content">要解密的内容</param>
        /// <param name="key">密钥</param>
        public static string DesDecrypt(string content, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            var len = content.Length / 2;
            byte[] data = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(content.Substring(x * 2, 2), 16);
                data[x] = (byte)i;
            }
            des.Key = Encoding.ASCII.GetBytes(MD5Encrypt(key).Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes("19921208");
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion



    }
}
