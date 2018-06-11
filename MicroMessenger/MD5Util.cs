using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace MicroMessenger
{


    public class MD5Util
    {


        /// <summary>
        /// 获取大写的MD5签名结果 
        /// </summary>
        /// <param name="encypStr">要加密的内容</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用UTF-8编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.GetEncoding("UTF-8").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }
        /// <summary>
        /// md5后转16进制字符串
        /// </summary>
        /// <param name="encypStr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string getMd5HexStr(string encypStr, string charset)
        {
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();
            byte[] inputBye;
            byte[] outputBye;
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.Default.GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            string retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToLower();
            return retStr;
        }
    }
}
