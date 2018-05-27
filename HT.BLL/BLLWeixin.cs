using HT.Model;
using HT.Model.Model;
using HT.Utility;
using System;
using System.Linq;
namespace HT.BLL
{
    /// <summary>
    /// 微信业务逻辑
    /// </summary>
    public class BLLWeixin
    {

        #region AccessToken 获取
        /// <summary>
        /// AccessToken 调用Api接口凭据
        /// </summary>
        public static string AccessToken { get { return GetAccessToken(); } }
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        private static string GetAccessToken()
        {
            var appId = BLLConfig.Get("wx_appid");
            var appSecret = BLLConfig.Get("wx_appsecret");
            var accessToken = BLLConfig.Get("wx_access_token");
            var accessTokenExpire = BLLConfig.Get("wx_access_token_expire");
            if ((!string.IsNullOrEmpty(accessTokenExpire))&& (!string.IsNullOrEmpty(accessToken)) && DateTime.Now < DateTime.Parse(accessTokenExpire))
            {
                return accessToken;
            }
            string parm = string.Format("grant_type=client_credential&appid={0}&secret={1}", appId, appSecret);
            string result = RequestUtil.Get("https://api.weixin.qq.com/cgi-bin/token?"+parm);
            WXAccessToken wxAccessToken = JSONHelper.JsonToObject<WXAccessToken>(result);
            if (wxAccessToken.errcode != 0)
            {
                return string.Empty;
            }
            using (Entities db = new Entities())
            {
                db.ht_sys_config.FirstOrDefault(p => p.xkey == "wx_access_token").xvalue = wxAccessToken.access_token;
                db.ht_sys_config.FirstOrDefault(p => p.xkey == "wx_access_token_expire").xvalue = DateTime.Now.AddSeconds(wxAccessToken.expires_in-60).ToString();
                if ( db.SaveChanges()>0)
                {

                }
                else
                {

                }

            }
            return wxAccessToken.access_token;
          

        }
        #endregion

        #region 模板消息发送



        ///// <summary>
        ///// 发送模板消息(余额提醒)
        ///// </summary>
        ///// <param name="openId">openId</param>
        ///// <param name="mobile">手机号</param>
        ///// <param name="blanceMsg">余额链接</param>
        ///// <param name="url">链接</param>
        ///// <returns></returns>
        //public static bool SendTemplateMessageBlance(string openId, string mobile, string blanceMsg, string url = "")
        //{
        //    ///data 示例
        //    ///{{first.DATA}}
        //    ///账号：{{keyword1.DATA}}
        //    ///当前余额：{{keyword2.DATA}}
        //    ///{{remark.DATA}}
        //    TemplateMessage msg = new TemplateMessage();
        //    msg.template_id = Config.TemplateMsgIdBlance;
        //    msg.touser = openId;
        //    msg.url = url;
        //    msg.data = new
        //    {
        //        first = new { value = string.Format("尊敬的{0},您当前账户余额不足，希望您尽快充值!", mobile) },
        //        keyword1 = new { value = mobile },
        //        keyword2 = new { value = blanceMsg },
        //        remark = new { value = "感谢你的使用。" }
        //    };
        //    return SendTemplateMessage(Ms.Common.JsonHelper.ObjectToJson(msg));
        //}

        ///// <summary>
        ///// 发送模板消息(审核通知)
        ///// </summary>
        ///// <param name="openId">openId</param>
        ///// <param name="mobile">手机号</param>
        ///// <param name="detail">详情</param>
        ///// <param name="url">链接</param>
        ///// <returns></returns>
        //public static bool SendTemplateMessageVery(string openId, string mobile, string detail, string url = "")
        //{
        //    /// data 示例
        //    ///{{first.DATA}}
        //    ///信息ID：{{keyword1.DATA}}
        //    ///详情：{{keyword2.DATA}}
        //    ///{{remark.DATA}}
        //    TemplateMessage msg = new TemplateMessage();
        //    msg.template_id = Config.TemplateMsgIdVerify;
        //    msg.touser = openId;
        //    msg.url = url;
        //    msg.data = new
        //    {
        //        first = new { value = string.Format("尊敬的{0}", mobile) },
        //        keyword1 = new { value = DateTime.Now.ToString("yyyyMMddHHmmss") },
        //        keyword2 = new { value = detail },
        //        remark = new { value = "感谢您的使用。" }
        //    };
        //    return SendTemplateMessage(Ms.Common.JsonHelper.ObjectToJson(msg));
        //}

        ///// <summary>
        ///// 发送模板消息
        ///// </summary>
        ///// <param name="templateMsg">模板消息</param>
        ///// <returns></returns>
        //public static bool SendTemplateMessage(TemplateMessage templateMsg)
        //{
        //    //官方文档 https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1433751277
        //    return SendTemplateMessage(Ms.Common.JsonHelper.ObjectToJson(templateMsg));
        //}
        ///// <summary>
        ///// 发送模板消息
        ///// </summary>
        ///// <param name="json">提交的json</param>
        ///// <returns></returns>
        //private static bool SendTemplateMessage(string json)
        //{
        //    string resultJson = Ms.Common.HttpUtil.HttpPost(string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", Access_Token), json);
        //    WXApiResult result = Ms.Common.JsonHelper.JsonToObject<WXApiResult>(resultJson);
        //    return result.errcode == 0;
        //}
        #endregion


    }
}
