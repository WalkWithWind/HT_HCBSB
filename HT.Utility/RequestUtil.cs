using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HT.Utility
{
    /// <summary>
    /// 请求通用类
    /// </summary>
    public sealed class RequestUtil
    {
        #region POST请求

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">参数字符串</param>
        public static string Post(string url, string param)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.22 Safari/537.36 SE 2.X MetaSr 1.0";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "*/*";
                request.Timeout = 15000;
                request.AllowAutoRedirect = false;
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.UTF8);
                writer.Write(param);
                writer.Close();
                WebResponse response = request.GetResponse();
                {
                    Stream stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        string result = reader.ReadToEnd();
                        reader.Close();
                        return result;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">参数字符串</param>
        /// <param name="contenttype">请求内容类型</param>
        public static string Post(string url, string param, string contenttype)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.22 Safari/537.36 SE 2.X MetaSr 1.0";
                request.ContentType = contenttype;
                request.Accept = "*/*";
                request.Timeout = 15000;
                request.AllowAutoRedirect = false;
                StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.UTF8);
                writer.Write(param);
                writer.Close();
                WebResponse response = request.GetResponse();
                {
                    Stream stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        string result = reader.ReadToEnd();
                        reader.Close();
                        return result;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 以JSON格式发起Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="json">json数据</param>
        /// <returns></returns>
        public static string HttpPost(string url, JObject json)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(json.ToString());
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = "POST";
                request.Accept = "application/json; charset=utf-8";
                request.ContentType = "application/json";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.75 Safari/537.36";
                request.Timeout = 15000;
                request.ContentLength = data.Length;
                Stream requeststream = request.GetRequestStream();
                requeststream.Write(data, 0, data.Length);
                requeststream.Flush();
                requeststream.Close();
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream responsestream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responsestream, Encoding.UTF8);
                string result = reader.ReadToEnd();
                reader.Close();
                responsestream.Close();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 模拟表单方式发起Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="dic">请求参数</param>
        /// <returns></returns>
        public static string HttpPost(string url, Dictionary<string, string> dic)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (dic != null && dic.Count > 0)
                {
                    foreach (KeyValuePair<string, string> item in dic)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append("&");
                        }
                        sb.AppendFormat("{0}={1}", item.Key, item.Value);
                    }
                }
                byte[] data = Encoding.UTF8.GetBytes(sb.ToString());
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                request.Referer = url;
                request.Accept = "*";
                request.Timeout = 15000;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.75 Safari/537.36";
                request.Method = "POST";
                request.ContentLength = data.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Flush();
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream backstream = response.GetResponseStream();
                StreamReader reader = new StreamReader(backstream, Encoding.UTF8);
                string result = reader.ReadToEnd();
                reader.Close();
                backstream.Close();
                response.Close();
                request.Abort();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region GET请求

        public static string Get(string url)
        {
            try
            {
                try
                {
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    request.Method = "GET";
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.22 Safari/537.36 SE 2.X MetaSr 1.0";
                    request.Accept = "*/*";
                    request.Timeout = 15000;
                    request.AllowAutoRedirect = false;
                    WebResponse response = request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream,Encoding.UTF8);
                    string result = reader.ReadToEnd();
                    reader.Close();
                    return result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}
