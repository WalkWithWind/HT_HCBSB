using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HT.Utility
{

    public class MessageService
    {
        public MessageService()
        {
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="user">短信接口用户名</param>
        /// <param name="pwd">短信接口密码</param>
        /// <param name="url">短信接口地址</param>
        /// <param name="mobile">手机号</param>
        /// <param name="content">短信内容</param>
        /// <param name="msg">返回消息</param>
        public bool Send(string user, string pwd, string url, string mobile, string content, out string msg)
        {
            try
            {
                string poststr = "account={0}&pswd={1}&mobile={2}&msg={3}&needstatus=true";
                string temp = string.Format(poststr, user, EncryptUtil.DesDecrypt(pwd, "haitao"), mobile, content);
                byte[] data = Encoding.UTF8.GetBytes(temp);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Flush();
                stream.Close();
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string result = reader.ReadToEnd();
                    if (result.Split(',')[1].Substring(0, 1).Equals("0"))
                    {
                        msg = "发送成功";
                        return true;
                    }
                    msg = "发送失败";
                    return false;
                }
                msg = "发送失败";
                return false;
            }
            catch (Exception)
            {
                msg = "发送失败";
                return false;
            }
        }
    }
}
