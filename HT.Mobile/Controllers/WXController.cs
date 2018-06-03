using HT.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HT.Model.Model;
using HT.BLL;
using HT.Model;
using System.Web.Security;
using System.Xml;
using System.Text;

namespace HT.Mobile.Controllers
{
    public class WXController : BaseController
    {
        /// <summary>
        /// 用户授权回调
        /// </summary>
        /// <returns></returns>
        public ActionResult OAuthCallback()
        {

            string code = Request["code"];
            string appId = Request["appid"];//授权给开放平台时返回此参数 默认授权无此参数
            string state = Request["state"];//state 原样返回

            string pageUrl = HttpUtility.UrlDecode(state);

            string appSecret = "";
            string getAccessTokenUrl = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                appId,
                appSecret,
                code);
            string accessTokenSource = RequestUtil.Get(getAccessTokenUrl);
            WXOAuthAccessToken accessTokenModel = JsonConvert.DeserializeObject<WXOAuthAccessToken>(accessTokenSource);

            ht_user user = BLLUser.GetUserByOpenid(accessTokenModel.openid);
            if (user != null)
            {
                BLLAuthentication.LoginAuthenticationTicket(user);
                return Redirect(pageUrl);
            }
            string wxUserInfoSourceJson = RequestUtil.Get(string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}",
                       accessTokenModel.access_token,
                       accessTokenModel.openid
                   ));
            WeixinUserInfo wxUserInfo = JsonConvert.DeserializeObject<WeixinUserInfo>(wxUserInfoSourceJson);

            user = new ht_user();
            user.nickname = wxUserInfo.nickname;
            user.avatar = wxUserInfo.headimgurl;
            user.username = accessTokenModel.openid;
            user.openid = accessTokenModel.openid;
            string prms = pageUrl.Substring(pageUrl.IndexOf("?") + 1);
            var qList = HttpUtility.ParseQueryString(prms);
            var pid = qList.Get("pid");
            if (pid != null) user.parent_id = Convert.ToInt32(pid);
            BLLAuthentication.LoginAuthenticationTicket(user);
            return Redirect(pageUrl);
        }


        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="id">订单号</param>
        /// <returns></returns>
        public ActionResult Pay(string id)
        {
            var order = BLLNews.GetNewsDetailsByOrderNo(id);
            ViewBag.OrderNo = id;
            if (Request.IsAjaxRequest())
            {
                if (order == null || order.pay_status == 1)
                {
                    return JsonResult(Model.Enum.APIErrCode.OperateFail, "订单无效或已经支付");
                }
                string Ip = Request.UserHostAddress;
                string openId = BLLUser.GetLoginUserInfo().openid;
                string notiUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/WX/PayNotify";//通知地址
               
                bool isRequestSuccess = false;
                var payRequest = BLLWeixin.WXPay(order.order_no, order.total.Value, openId, Ip, notiUrl, out isRequestSuccess);
                if (isRequestSuccess)
                {
                    return JsonResult(Model.Enum.APIErrCode.Success, "OK", payRequest);
                }
                else
                {
                    return JsonResult(Model.Enum.APIErrCode.OperateFail, "订单无效或已经支付");

                }
            }
            else
            {
                return View();
            }
            

        }
        /// <summary>
        /// 微信支付通知
        /// </summary>
        /// <returns></returns>
        public ActionResult PayNotify()
        {
            string successXml = "<xml><return_code><![CDATA[SUCCESS]]></return_code></xml>";
            string failXml = "<xml><return_code><![CDATA[FAIL]]></return_code></xml>";
            try
            {

                //Tolog("进入支付回调");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Request.InputStream);
                xmlDoc.Save(string.Format("E:\\WXPay\\Notify{0}.xml", DateTime.Now.ToString("yyyyMMddHHmmssfff")));//写入日志
                //全部参数
                Dictionary<string, string> parametersAll = new Dictionary<string, string>();
                foreach (XmlElement item in xmlDoc.DocumentElement.ChildNodes)
                {
                    string key = item.Name;
                    string value = item.InnerText;
                    if ((!string.IsNullOrEmpty(key)) && (!string.IsNullOrEmpty(value)))
                    {
                        parametersAll.Add(key, value);

                    }

                }
                parametersAll = (from entry in parametersAll
                                 orderby entry.Key ascending
                                 select entry).ToDictionary(pair => pair.Key, pair => pair.Value);//全部参数排序
                if (MicroMessenger.CommonUtil.VerifySign(parametersAll, BLLConfig.Get("wx_mchsecret")))
                {
                    if (BLLNews.WXPaySuccess(parametersAll["out_trade_no"], parametersAll["out_trade_no"]))
                    {
                        return Content(successXml);
                    }

                }

            }
            catch (Exception ex)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"E:\WXPay\Error.txt", true, Encoding.GetEncoding("UTF-8")))
                {
                    sw.WriteLine(ex.ToString());
                }

            }
            return Content(failXml);

        }
        /// <summary>
        /// 支付成功
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PaySuccess(string id)
        {
            ViewBag.OrderNo = id;
            return View();

        }
        /// <summary>
        /// 支付失败
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PayFail(string id)
        {
            ViewBag.OrderNo = id;
            return View();

        }

    }
}