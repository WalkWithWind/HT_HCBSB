using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace HT.Utility
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public sealed class Utils
    {
        readonly static SystemConfig _config = new SystemConfig();

        /// <summary>
        /// 获取8位随机密钥
        /// </summary>
        public static string GetSalt()
        {
            return GetRandKey(8, ComplexLevel.Middle);
        }

        /// <summary>
        /// 根据复杂度生成指定长度字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="level">复杂度</param>
        /// <returns></returns>
        public static string GetRandKey(int length, ComplexLevel level = ComplexLevel.Lower)
        {
            string rands = string.Empty;
            switch (level)
            {
                case ComplexLevel.Lower:
                    rands = "1234567890";
                    break;
                case ComplexLevel.Middle:
                    rands = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
                case ComplexLevel.High:
                    rands = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!)#$%^*(";
                    break;
            }
            char[] chars = rands.ToCharArray();
            byte[] data = new byte[length];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            StringBuilder sb = new StringBuilder(8);
            foreach (byte item in data)
            {
                sb.Append(chars[item % (chars.Length - 1)]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取全局唯一字符串
        /// </summary>
        public static string GetGuidString()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 生成日期随机码
        /// </summary>
        public static string GetDateString()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        public static object GetVersion()
        {
            return HTKeys.ASSEMBLY_VERSION;
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 将字符串转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns></returns>
        public static int StrToInt(string expression, int defValue)
        {
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(expression, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(expression, defValue));
        }

        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns></returns>
        public static decimal StrToDecimal(string expression, decimal defValue)
        {
            if ((expression == null) || (expression.Length > 10))
                return defValue;

            decimal intValue = defValue;
            {
                bool isDecimal = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (isDecimal)
                    decimal.TryParse(expression, out intValue);
            }
            return intValue;
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns></returns>
        public static float StrToFloat(string expression, float defValue)
        {
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(expression, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(expression, defValue));
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="path">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string path)
        {
            if (path.ToLower().StartsWith("http://"))
            {
                return path;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(path);
            }
            else //非web程序引用
            {
                path = path.Replace("/", "\\");
                if (path.StartsWith("\\"))
                {
                    path = path.Substring(path.IndexOf('\\', 1)).TrimStart('\\');
                }
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }
        }

        /// <summary>
        /// 返回文件扩展名，不含“.”
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        public static string GetFileExt(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                return "";
            }
            if (filepath.LastIndexOf(".", StringComparison.Ordinal) > 0)
            {
                return filepath.Substring(filepath.LastIndexOf(".", StringComparison.Ordinal) + 1); //文件扩展名，不含“.”
            }
            return "";
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="filepath">文件相对路径</param>
        public static bool DeleteFile(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                return false;
            }
            string fullpath = GetMapPath(filepath);
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = HttpUtility.UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(秒)</param>
        public static void WriteCookieSeconds(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = HttpUtility.UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddSeconds(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = HttpUtility.UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = HttpUtility.UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = HttpUtility.UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies[strName] != null)
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strName].Value.ToString());
            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strName][key].ToString());

            return "";
        }

        /// <summary>
        /// 删除上传的文件(及缩略图)
        /// </summary>
        /// <param name="delfile"></param>
        public static void DeleteUpFile(string delfile)
        {
            if (string.IsNullOrEmpty(delfile))
            {
                return;
            }
            string fullpath = GetMapPath(delfile); //原图
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
            if (delfile.LastIndexOf("/", StringComparison.Ordinal) >= 0)
            {
                string thumbnailpath = delfile.Substring(0, delfile.LastIndexOf("/", StringComparison.Ordinal)) + "mall_" + delfile.Substring(delfile.LastIndexOf("/", StringComparison.Ordinal) + 1);
                string path = GetMapPath(thumbnailpath); //宿略图
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        /// <summary>
        /// 获取操作权限
        /// </summary>
        public static Dictionary<string, string> ActionType()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                {"Show", "显示"},
                {"View", "查看"},
                {"Add", "添加"},
                {"Edit", "修改"},
                {"Delete", "删除"},
                {"Audit", "审核"},
                {"Reply", "回复"},
                {"Confirm", "确认"},
                {"Cancel", "取消"},
                {"Invalid", "作废"},
                {"Build", "生成"}
            };
            return dic;
        }

        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {
            string returnStr = "";
            for (int i = 0; i < strLong; i++)
            {
                returnStr += str;
            }

            return returnStr;
        }

        /// <summary>
        /// 返回分页页码
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="linkUrl">链接地址，__id__代表页码</param>
        /// <param name="centSize">中间页码数量</param>
        /// <returns></returns>
        public static string OutPageList(int pageSize, int pageIndex, int totalCount, string linkUrl, int centSize)
        {
            //计算页数
            if (totalCount < 1 || pageSize < 1)
            {
                return "";
            }
            int pageCount = totalCount / pageSize;
            if (pageCount < 1)
            {
                return "";
            }
            if (totalCount % pageSize > 0)
            {
                pageCount += 1;
            }
            if (pageCount <= 1)
            {
                return "";
            }
            StringBuilder pageStr = new StringBuilder();
            string pageId = "__id__";
            string firstBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex - 1).ToString()) + "\">«上一页</a>";
            string lastBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex + 1).ToString()) + "\">下一页»</a>";
            string firstStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, "1") + "\">1</a>";
            string lastStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, pageCount.ToString()) + "\">" + pageCount.ToString() + "</a>";

            if (pageIndex <= 1)
            {
                firstBtn = "<span class=\"disabled\">«上一页</span>";
            }
            if (pageIndex >= pageCount)
            {
                lastBtn = "<span class=\"disabled\">下一页»</span>";
            }
            if (pageIndex == 1)
            {
                firstStr = "<span class=\"current\">1</span>";
            }
            if (pageIndex == pageCount)
            {
                lastStr = "<span class=\"current\">" + pageCount.ToString() + "</span>";
            }
            int firstNum = pageIndex - (centSize / 2); //中间开始的页码
            if (pageIndex < centSize)
                firstNum = 2;
            int lastNum = pageIndex + centSize - ((centSize / 2) + 1); //中间结束的页码
            if (lastNum >= pageCount)
                lastNum = pageCount - 1;
            pageStr.Append("<span>共" + totalCount + "记录</span>");
            pageStr.Append(firstBtn + firstStr);
            if (pageIndex >= centSize)
            {
                pageStr.Append("<span>...</span>\n");
            }
            for (int i = firstNum; i <= lastNum; i++)
            {
                if (i == pageIndex)
                {
                    pageStr.Append("<span class=\"current\">" + i + "</span>");
                }
                else
                {
                    pageStr.Append("<a href=\"" + ReplaceStr(linkUrl, pageId, i.ToString()) + "\">" + i + "</a>");
                }
            }
            if (pageCount - pageIndex > centSize - ((centSize / 2)))
            {
                pageStr.Append("<span>...</span>");
            }
            pageStr.Append(lastStr + lastBtn);
            return pageStr.ToString();
        }

        //会员个人中心分页
        public static string OutPageListUL(int pageSize, int pageIndex, int totalCount, string linkUrl, int centSize)
        {
            //计算页数
            if (totalCount < 1 || pageSize < 1)
            {
                return "";
            }
            int pageCount = totalCount / pageSize;
            if (pageCount < 1)
            {
                return "";
            }
            if (totalCount % pageSize > 0)
            {
                pageCount += 1;
            }
            if (pageCount <= 1)
            {
                return "";
            }
            StringBuilder pageStr = new StringBuilder();
            pageStr.Append("<ul>");
            string pageId = "__id__";
            string firstBtn = "<li ><a  class=\"table_prev\" href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex - 1).ToString()) + "\"></a></li>";
            string lastBtn = "<li ><a  class=\"table_next\" href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex + 1).ToString()) + "\"></a></li>";
            string firstStr = "<li ><a  href=\"" + ReplaceStr(linkUrl, pageId, "1") + "\">1</a>";
            string lastStr = "<li ><a  href=\"" + ReplaceStr(linkUrl, pageId, pageCount.ToString()) + "\">" + pageCount.ToString() + "</a></li>";

            if (pageIndex <= 1)
            {
                firstBtn = "<li ><a   class=\"table_prev\"></a></li>";
            }
            if (pageIndex >= pageCount)
            {
                lastBtn = "<li><a  class=\"table_next\" ></a></li>";
            }
            if (pageIndex == 1)
            {
                firstStr = "<li><a   class=\"current\">1</a></li>";
            }
            if (pageIndex == pageCount)
            {
                lastStr = "<li ><a  class=\"current\">" + pageCount.ToString() + "</a></li>";
            }
            int firstNum = pageIndex - (centSize / 2); //中间开始的页码
            if (pageIndex < centSize)
                firstNum = 2;
            int lastNum = pageIndex + centSize - ((centSize / 2) + 1); //中间结束的页码
            if (lastNum >= pageCount)
                lastNum = pageCount - 1;
            //pageStr.Append("<li class=\"prev\">共" + totalCount + "条记录</li>");
            pageStr.Append(firstBtn + firstStr);
            if (pageIndex >= centSize)
            {
                pageStr.Append("<li><a >...</a></li>");
            }
            for (int i = firstNum; i <= lastNum; i++)
            {
                if (i == pageIndex)
                {
                    pageStr.Append("<li  ><a class=\"current\" >" + i + "</a></li>");
                }
                else
                {
                    pageStr.Append("<li ><a  href=\"" + ReplaceStr(linkUrl, pageId, i.ToString()) + "\">" + i + "</a></li>");
                }
            }
            if (pageCount - pageIndex > centSize - ((centSize / 2)))
            {
                pageStr.Append("<li><a >...</a></li>");
            }
            pageStr.Append(lastStr + lastBtn);
            pageStr.Append("</ul>");
            return pageStr.ToString();
        }



        //论坛分页
        public static string OutPageListforumUL(int pageSize, int pageIndex, int totalCount, string linkUrl, int centSize)
        {
            //计算页数
            if (totalCount < 1 || pageSize < 1)
            {
                return "";
            }
            int pageCount = totalCount / pageSize;
            if (pageCount < 1)
            {
                return "";
            }
            if (totalCount % pageSize > 0)
            {
                pageCount += 1;
            }
            if (pageCount <= 1)
            {
                return "";
            }
            StringBuilder pageStr = new StringBuilder();
            pageStr.Append("<ul>");
            string pageId = "__id__";
            string firstBtn = "<li ><a   href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex - 1).ToString()) + "\">《</a></li>";
            string lastBtn = "<li ><a   href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex + 1).ToString()) + "\">》</a></li>";
            string firstStr = "<li ><a  href=\"" + ReplaceStr(linkUrl, pageId, "1") + "\">1</a>";
            string lastStr = "<li ><a  href=\"" + ReplaceStr(linkUrl, pageId, pageCount.ToString()) + "\">" + pageCount.ToString() + "</a></li>";

            if (pageIndex <= 1)
            {
                firstBtn = "<li ><a  >《</a></li>";
            }
            if (pageIndex >= pageCount)
            {
                lastBtn = "<li><a   >》</a></li>";
            }
            if (pageIndex == 1)
            {
                firstStr = "<li><a   class=\"current\">1</a></li>";
            }
            if (pageIndex == pageCount)
            {
                lastStr = "<li ><a  class=\"current\">" + pageCount.ToString() + "</a></li>";
            }
            int firstNum = pageIndex - (centSize / 2); //中间开始的页码
            if (pageIndex < centSize)
                firstNum = 2;
            int lastNum = pageIndex + centSize - ((centSize / 2) + 1); //中间结束的页码
            if (lastNum >= pageCount)
                lastNum = pageCount - 1;
            //pageStr.Append("<li class=\"prev\">共" + totalCount + "条记录</li>");
            pageStr.Append(firstBtn + firstStr);
            if (pageIndex >= centSize)
            {
                pageStr.Append("<li><a >...</a></li>");
            }
            for (int i = firstNum; i <= lastNum; i++)
            {
                if (i == pageIndex)
                {
                    pageStr.Append("<li  ><a class=\"current\" >" + i + "</a></li>");
                }
                else
                {
                    pageStr.Append("<li ><a  href=\"" + ReplaceStr(linkUrl, pageId, i.ToString()) + "\">" + i + "</a></li>");
                }
            }
            if (pageCount - pageIndex > centSize - ((centSize / 2)))
            {
                pageStr.Append("<li><a >...</a></li>");
            }
            pageStr.Append(lastStr + lastBtn);
            pageStr.Append("</ul>");
            return pageStr.ToString();
        }

        /// <summary>
        /// 替换指定的字符串
        /// </summary>
        /// <param name="originalStr">原字符串</param>
        /// <param name="oldStr">旧字符串</param>
        /// <param name="newStr">新字符串</param>
        /// <returns></returns>
        public static string ReplaceStr(string originalStr, string oldStr, string newStr)
        {
            if (string.IsNullOrEmpty(oldStr))
            {
                return "";
            }
            return originalStr.Replace(oldStr, newStr);
        }

        /// <summary>
        /// 组合URL参数
        /// </summary>
        /// <param name="url">页面地址</param>
        /// <param name="keys">参数名称</param>
        /// <param name="values">参数值</param>
        /// <returns>String</returns>
        public static string CombUrlTxt(string url, string keys, params string[] values)
        {
            StringBuilder urlParams = new StringBuilder();
            try
            {
                string[] keyArr = keys.Split(new char[] { '&' });
                for (int i = 0; i < keyArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(values[i]) && values[i] != "0")
                    {
                        values[i] = HttpUtility.UrlEncode(values[i]);
                        urlParams.Append(string.Format(keyArr[i], values) + "&");
                    }
                }
                if (!string.IsNullOrEmpty(urlParams.ToString()) && url.IndexOf("?") == -1)
                    urlParams.Insert(0, "?");
            }
            catch
            {
                return url;
            }
            return url + DelLastChar(urlParams.ToString(), "&");
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar, StringComparison.Ordinal) >= 0 && str.LastIndexOf(strchar, StringComparison.Ordinal) == str.Length - 1)
            {
                return str.Substring(0, str.LastIndexOf(strchar, StringComparison.Ordinal));
            }
            return str;
        }

        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            try
            {
                if (str.Length < 1)
                {
                    return "";
                }
                return str.Substring(0, str.LastIndexOf(",", StringComparison.Ordinal));
            }
            catch (Exception)
            {
                return str;
            }
        }

        /// <summary>
        /// 生成随机字母字符串(数字字母混和)
        /// </summary>
        /// <param name="codeCount">待生成的位数</param>
        public static string GetCheckCode(int codeCount)
        {
            string str = string.Empty;
            int rep = 0;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch;
            }
            return str;
        }

        /// <summary>
        /// 返回上传目录相对路径
        /// </summary>
        /// <returns></returns>
        public static string GetUpLoadPath()
        {
            string path = _config["webpath"] + _config["filepath"] + "/";//站点目录+上传目录
            int filesave = Convert.ToInt32(_config["filesave"]);
            switch (filesave)
            {
                case 1: //按年月日每天一个文件夹
                    path += DateTime.Now.ToString("yyyyMMdd");
                    break;
                default: //按年月/日存入不同的文件夹
                    path += DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd");
                    break;
            }
            return path + "/";
        }

        /// <summary>
        /// 图片Base64解码
        /// </summary>
        /// <param name="img">Base64字符串</param>
        /// <returns>图片路径</returns>
        public static string DecodeImage(string img)
        {
            try
            {
                if (string.IsNullOrEmpty(img))
                {
                    return "";
                }
                if (img.Length < 100 && img.Length > 10)
                {
                    return "";
                }
                string fpath = GetUpLoadPath();
                //物理路径
                string path = HttpContext.Current.Server.MapPath(fpath);//图片存储文件夹路径
                string picturename = GetGuidString();//文件名
                if (!Directory.Exists(path))//查看存储路径的文件是否存在
                {
                    Directory.CreateDirectory(path);   //创建文件夹，并上传文件
                }
                byte[] arr = Convert.FromBase64String(img);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(path + picturename + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);   //保存为.jpg格式
                ms.Close();
                return fpath + picturename + ".jpg";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 生成指定长度的数字
        /// </summary>
        /// <param name="length">长度</param>
        public static string Number(int length)
        {
            string result = "";
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
    }
}
