using HT.Model;
using HT.Model.Model;
using HT.Utility;
using MicroMessenger;
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
            if ((!string.IsNullOrEmpty(accessTokenExpire)) && (!string.IsNullOrEmpty(accessToken)) && DateTime.Now < DateTime.Parse(accessTokenExpire))
            {
                return accessToken;
            }
            string parm = string.Format("grant_type=client_credential&appid={0}&secret={1}", appId, appSecret);
            string result = RequestUtil.Get("https://api.weixin.qq.com/cgi-bin/token?" + parm);
            WXAccessToken wxAccessToken = JSONHelper.JsonToObject<WXAccessToken>(result);
            if (wxAccessToken.errcode != 0)
            {
                return string.Empty;
            }
            using (Entities db = new Entities())
            {
                db.ht_sys_config.FirstOrDefault(p => p.xkey == "wx_access_token").xvalue = wxAccessToken.access_token;
                db.ht_sys_config.FirstOrDefault(p => p.xkey == "wx_access_token_expire").xvalue = DateTime.Now.AddSeconds(wxAccessToken.expires_in - 60).ToString();
                if (db.SaveChanges() > 0)
                {

                }
                else
                {

                }

            }
            return wxAccessToken.access_token;
        }
        #endregion

        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="totalAmount">订单金额</param>
        /// <param name="openId">用户OpenId</param>
        /// <param name="ip">用户IP</param>
        /// <param name="notiUrl">通知Url</param>
        /// <param name="body">订单内容</param>
        /// <param name="tradeType">交易类型</param>
        /// <returns></returns>
        public static WXPayRequest WXPay(string orderId, decimal totalAmount, string openId, string ip, string notiUrl, out bool isSuccess, string body = "", string tradeType = "")
        {
            string appId = BLLConfig.Get("wx_appid");
            string mchId = BLLConfig.Get("wx_mchid");
            string mchKey = BLLConfig.Get("wx_mchsecret");
            return WXApi.GetBrandWcPayRequest(orderId, totalAmount, appId, mchId, mchKey, openId, ip, notiUrl,out isSuccess, body, tradeType);

        }



        /// <summary>
        /// 发送模板消息(余额提醒)
        /// </summary>
        /// <param name="openId">openId</param>
        /// <param name="mobile">手机号</param>
        /// <param name="blanceMsg">消息</param>
        /// <param name="url">链接</param>
        /// <returns></returns>
        public static bool SendTemplateMessageBlance(string openId, string mobile, string blanceMsg, string url = "")
        {
            ///data 示例
            ///{{first.DATA}}
            ///账号：{{keyword1.DATA}}
            ///当前余额：{{keyword2.DATA}}
            ///{{remark.DATA}}
            TemplateMessage msg = new TemplateMessage();
            msg.template_id = "";
            msg.touser = openId;
            msg.url = url;
            msg.data = new
            {
                first = new { value = string.Format("尊敬的{0},您当前账户余额不足，希望您尽快充值!", mobile) },
                keyword1 = new { value = mobile },
                keyword2 = new { value = blanceMsg },
                remark = new { value = "感谢你的使用。" }
            };
            return WXApi.SendTemplateMessage(AccessToken,msg);
        }




    }
}
