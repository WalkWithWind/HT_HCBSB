using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HT.Utility
{
    public class MyRegex
    {   
        /// <summary>
        /// 是否是手机号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            return Regex.IsMatch(str, "^(13[0-9]|14[5|7]|15[0-9]|17[0-9]|18[0-9])\\d{8}$");
        }
        /// <summary>
        /// 是否是数字
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            if (string.IsNullOrWhiteSpace(inputData)) return false;
            return new Regex("^[0-9]+$").Match(inputData).Success;
        }
    }
}
