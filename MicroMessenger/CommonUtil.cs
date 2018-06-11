using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroMessenger
{

    public class CommonUtil
    {
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static String CreateNoncestr(int length)
        {
            String chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            String res = "";
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                res += chars[rd.Next(chars.Length - 1)];
            }
            return res;
        }
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <returns></returns>
        public static String CreateNoncestr()
        {
            String chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            String res = "";
            Random rd = new Random();
            for (int i = 0; i < 16; i++)
            {
                res += chars[rd.Next(chars.Length - 1)];
            }
            return res;
        }
        /// <summary>
        /// 将参数拼接成key=value&key=value形式
        /// </summary>
        /// <param name="parameters">参数键值对</param>
        /// <returns></returns>
        public static string FormatQueryParaMap(SortedDictionary<string, string> parameters)
        {

            string buff = "";
            try
            {

                var result = from pair in parameters orderby pair.Key select pair;
                foreach (KeyValuePair<string, string> pair in result)
                {
                    if (pair.Key != "")
                    {
                        buff += pair.Key+ "=" + pair.Value+ "&";
                               
                    }
                }
                if (buff.Length == 0 == false)
                {
                    buff = buff.Substring(0, (buff.Length - 1) - (0));
                }
            }
            catch (Exception e)
            {
                //throw new SDKRuntimeException(e.Message);
            }

            return buff;
        }
        /// <summary>
        /// 将参数拼接成key=value&key=value形式
        /// </summary>
        /// <param name="paraMap">参数键值对</param>
        /// <param name="urlEncode">编码</param>
        /// <returns></returns>
        public static string FormatBizQueryParaMap(SortedDictionary<string, string> paraMap,
                bool urlEncode)
        {

            string buff = "";
            try
            {
                var result = from pair in paraMap orderby pair.Key select pair;
                foreach (KeyValuePair<string, string> pair in result)
                {
                    if (pair.Key != "")
                    {

                        string key = pair.Key;
                        string val = pair.Value;
                        if (urlEncode)
                        {
                            val = System.Web.HttpUtility.UrlEncode(val);
                        }
                        buff += key.ToLower() + "=" + val + "&";

                    }
                }

                if (buff.Length == 0 == false)
                {
                    buff = buff.Substring(0, (buff.Length - 1) - (0));
                }
            }
            catch (Exception e)
            {
                //throw new SDKRuntimeException(e.Message);
            }
            return buff;
        }
        /// <summary>
        /// 是否是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(String str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 键值对转成xml
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ArrayToXml(SortedDictionary<string, string> arr)
        {
            String xml = "<xml>";

            foreach (KeyValuePair<string, string> pair in arr)
            {
                String key = pair.Key;
                String val = pair.Value;
                if (IsNumeric(val))
                {
                    xml += "<" + key + ">" + val + "</" + key + ">";

                }
                else
                    xml += "<" + key + "><![CDATA[" + val + "]]></" + key + ">";
            }

            xml += "</xml>";
            return xml;
        }


        /// <summary>
        /// 微信支付验证签名
        /// </summary>
        /// <param name="dic">接收到的所有参数</param>
        /// <param name="key">商户 key</param>
        /// <returns></returns>
        public static bool VerifySign(SortedDictionary<string, string> dic, string key)
        {
            string sign = dic["sign"];
            dic.Remove("sign");
            if (MD5SignUtil.VerifySignature(FormatBizQueryParaMap(dic, false),sign, key))//验证签名
            {
                return true;
            }
            return false;
        }



    }

}
