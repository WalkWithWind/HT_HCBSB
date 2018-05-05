using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HT.Utility
{
    /// <summary>
    /// 签名
    /// </summary>
    public abstract class Sign
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public static string Secret
        {
            get
            {
                return ConfigurationManager.AppSettings["AppUrlSecret"];
            }
        }

        /// <summary>
        /// 给请求签名
        /// </summary>
        /// <param name="json">请求参数</param>
        /// <returns>签名</returns>
        public static string SignRequest(JObject json)
        {
            try
            {
                //有序字典集合
                IDictionary<string, object> dic = new SortedDictionary<string, object>();
                foreach (JProperty item in json.Properties())
                {
                    if (item.Name.Equals("sign"))
                    {
                        continue;
                    }
                    dic.Add(item.Name, item.Value);
                }

                //迭代键值对
                IEnumerator<KeyValuePair<string, object>> pair = dic.GetEnumerator();

                //生成参数和密钥的指定字符串
                StringBuilder query = new StringBuilder(Secret);
                while (pair.MoveNext())
                {
                    string key = pair.Current.Key;
                    object value = pair.Current.Value;
                    query.Append(key);
                    if (!string.IsNullOrEmpty(value.ToString()))
                    {
                        query.Append(value);
                    }
                }
                query.Append(Secret);
                //进行小写的加密
                string result = EncryptUtil.MD5Encrypt(query.ToString()).ToLower();
                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
